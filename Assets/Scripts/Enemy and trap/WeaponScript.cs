using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class WeaponScript : MonoBehaviour
{
	//--------------------------------
	// 1 – Переменные дизайнера
	//--------------------------------
	public int HealthWeapon = 50;
	/// <summary>
	/// Префаб снаряда для стрельбы
	/// </summary>
	public Transform shotPrefab;

	/// <summary>
	/// Время перезарядки в секундах
	/// </summary>
	public float shootingRate = 0.25f;

	//--------------------------------
	// 2 - Перезарядка
	//--------------------------------

	private float shootCooldown;

	void Start()
	{
		shootCooldown = 0f;
	}

	void Update()
	{
		if (shootCooldown > 0)
		{
			shootCooldown -= Time.deltaTime;
		}
	}

	//--------------------------------
	// 3 - Стрельба из другого скрипта
	//--------------------------------

	/// <summary>
	/// Создайте новый снаряд, если это возможно
	/// </summary>
	public void Attack(bool isEnemy)
	{
		if (CanAttack)
		{
			shootCooldown = shootingRate;

			// Создайте новый выстрел
			var shotTransform = Instantiate(shotPrefab) as Transform;

			// Определите положение
			shotTransform.position = transform.position;

			// Свойство врага
			ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
			if (shot != null)
			{
				shot.isEnemyShot = isEnemy;
			}

			// Сделайте так, чтобы выстрел всегда был направлен на него
			MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
			//if (move != null)
			//{
			//	move.direction = this.transform.right; // в двухмерном пространстве это будет справа от спрайта
			//}
		}
	}

	/// <summary>
	/// Готово ли оружие выпустить новый снаряд?
	/// </summary>
	public bool CanAttack
	{
		get
		{
			return shootCooldown <= 0f;
		}
	}
	public void OnCollisionEnter2D(Collision2D other)
	{		
		Damage (10);
		if (other.gameObject.tag == "Player")
		{
			Application.LoadLevel (Application.loadedLevel);
			//Player.Destroy(Player);		
		}
		if (other.gameObject.tag == "Wall")
		{
			Damage (100);
			//Player.Destroy(Player);
		}
	}
	public void Damage(int damageCount){
		HealthWeapon -= damageCount;

		if (HealthWeapon <= 0) {
			Destroy (gameObject);
		}

	}
}