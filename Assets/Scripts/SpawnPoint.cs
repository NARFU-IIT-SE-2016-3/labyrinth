using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public int X;
    public int Y;

	public void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(X, Y, player.transform.position.z);
	}
}
