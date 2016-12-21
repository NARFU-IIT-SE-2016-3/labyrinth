using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnCollisionEnter2D(Collision2D other)
	{		
		//Damage (10);
		if (other.gameObject.tag == "Player")
		{			
			//Application.LoadLevel (Application.loadedLevel);
			Health.Destroy (gameObject);
		}

	}
}
