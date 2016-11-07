using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float DampTime = 5;
    public GameObject Target;

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
    
    public void Update()
    {
        if (Target == null)
        {
            return;
        }

        var newPos = new Vector3(Target.transform.position.x, Target.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, DampTime);
    }
}
