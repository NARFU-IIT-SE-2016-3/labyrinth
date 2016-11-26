using UnityEngine;

/// <summary>
/// Enemy generic behavior
/// </summary>
public class EnemyScript : MonoBehaviour
{
	private WeaponScript weapon;

	void Awake()
	{
		// Получить оружие только один раз
		weapon = GetComponent<WeaponScript>();
	}

	void Update()
	{
		// автоматическая стрельба
		if (weapon != null && weapon.CanAttack)
		{
			weapon.Attack(true);
		}
	}
}