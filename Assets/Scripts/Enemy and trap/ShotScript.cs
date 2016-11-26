using UnityEngine;

/// <summary>
/// Поведение снаряда
/// </summary>
public class ShotScript : MonoBehaviour
{
	// 1 – Переменная дизайнера

	/// <summary>
	/// Причиненный вред
	/// </summary>
	public int damage = 1;

	/// <summary>
	/// Снаряд наносит повреждения игроку или врагам?
	/// </summary>
	public bool isEnemyShot = false;

	void Start()
	{
		// Ограниченное время жизни, чтобы избежать утечек
		Destroy(gameObject, 5); // 20 секунд
	}
	public void OnCollisionEnter2D(Collision2D other)
	{
		
		if (other.gameObject.tag == "Player")
		{
			Destroy(gameObject);
		}
		if (other.gameObject.tag == "Wall")
		{
			Destroy(gameObject);
		}
	}
}
