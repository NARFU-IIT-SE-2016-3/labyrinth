using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float Speed;
    public int Health;
    public GameObject Torch;

    private Rigidbody2D rb2d;
    private static bool isInstantiated;
    private Collider2D lastCollision;

    private int maxLives = 3;
    private int lives = 1;

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
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "LifeItem")
        {
            if (lives < maxLives)
            {
                lives++;
                Destroy(other.gameObject);
            }
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
