using UnityEngine;

public class LightingManager : MonoBehaviour
{
    private static bool isInstantiated;

    public void Awake()
    {
        if (!isInstantiated)
        {
            isInstantiated = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

	public void Start()
    {
        transform.position = new Vector3(LevelManager.Width / 2, LevelManager.Height / 2, transform.position.z);
	}
}
