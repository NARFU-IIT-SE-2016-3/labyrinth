using UnityEngine;
using System.Collections;

public class TrapSlow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public void OnCollisionEnter2D(Collision2D other)
	{

		if (other.gameObject.tag == "Player") 
		{
			Destroy (gameObject);
			Application.LoadLevel (Application.loadedLevel);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
