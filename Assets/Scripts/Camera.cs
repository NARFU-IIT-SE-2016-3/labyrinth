using UnityEngine;

public class Camera : MonoBehaviour
{
    public float DampTime = 5;
    public GameObject Target;

    // Use this for initialization
    void Start()
    {
	
	}
    
    // Update is called once per frame
    void Update()
    {
        if (Target == null)
        {
            return;
        }

        var newPos = new Vector3(Target.transform.position.x, Target.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, DampTime);
    }
}
