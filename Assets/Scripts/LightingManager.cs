using UnityEngine;
using System.Collections;

public class LightingManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(LevelManager.Width / 2, LevelManager.Height / 2, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
