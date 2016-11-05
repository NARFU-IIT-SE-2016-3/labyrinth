using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(Instance);
    }

	// Use this for initialization
	public void Start()
    {
        
    }
	
	// Update is called once per frame
	public void Update()
    {
	
	}
}
