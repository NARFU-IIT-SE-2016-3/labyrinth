using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float Speed;
    public int Health = 100;
    public GameObject Torch;

    private Rigidbody2D rb2d;
    private static bool isInstantiated;
    private Collider2D lastCollision;

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

	// Use this for initialization
	public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
	}

    public void FixedUpdate()
    {
        var hor = Input.GetAxisRaw("Horizontal");
        var vert = Input.GetAxisRaw("Vertical");
        var x = hor * Time.fixedDeltaTime * Speed;
        var y = vert * Time.fixedDeltaTime * Speed;

        rb2d.velocity = new Vector2(x, y);

        HandleInput();
        CheckDistance();
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "npc")
        {
            lastCollision = other.collider;
        }
		if (other.gameObject.tag == "Enemy")
		{
			Health -= 10;
		}
		if (other.gameObject.tag == "Wall")
		{
			Health -= 50;
		}
    }

    private void HandleInput()
    {
        // torch placement
        if (Input.GetKeyDown(KeyCode.E))
        {
            var pos = new Vector3(transform.position.x, transform.position.y, Torch.transform.position.z);
            Instantiate(Torch, pos, Quaternion.identity);
        }

        // restart level
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        // start dialog
        if (Input.GetKeyDown(KeyCode.F) && lastCollision != null)
        {
            var dialog = lastCollision.GetComponent<DialogLoader>();
            dialog.NextLine();
        }
    }

    private void CheckDistance()
    {
        if (lastCollision == null)
        {
            return;
        }

        var dist = Vector3.Distance(lastCollision.transform.position, transform.position);

        if (dist > 1)
        {
            lastCollision.GetComponent<DialogLoader>().ResetDialog();
            lastCollision = null;
        }
    }


}
