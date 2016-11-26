using UnityEngine;
using System.Collections;

public class TrapSaw : MonoBehaviour
{
	public int HealthSaw = 50;
	public float speedRotate = 10.0f;
	// Use this for initialization
	public Vector2 speed = new Vector2(10,10);
	public Vector2 direction = new Vector2 (-1, 0);
	private Vector2 movement;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		movement = new Vector2 (speed.x * direction.x,
			speed.y * direction.y);
		//Вращение
		transform.Rotate (new Vector3 (0f, 0f, speedRotate));
	}

	void FixedUpdate(){
		GetComponent<Rigidbody2D>().velocity = movement;
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
		HealthSaw -= damageCount;

		if (HealthSaw <= 0) {
			Destroy (gameObject);
		}
	
	}

}
