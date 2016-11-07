using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {
    public int X;
    public int Y;

	// Use this for initialization
	void Start () {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(X, Y, player.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
