using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public float Speed;
    public int Health;
    public int Satiety = 100;

    public GameObject Torch;

	public UnityEvent OnCollect;
	public UnityEvent OnTorchPlace;

    private Rigidbody2D rb2d;
    private static bool isInstantiated;
    private Collider2D lastCollision;

    private int maxLives = 3;
    private int lives = 1;
    private int maxSatiety = 100;
    private float lastTimeUpdate;

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

		OnCollect = new UnityEvent ();
		OnTorchPlace = new UnityEvent ();
    }

	public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        lastTimeUpdate = Time.fixedTime;
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

        // Timer (hunger)
        if (Time.fixedTime - lastTimeUpdate > 100f)
        {
            lastTimeUpdate = Time.fixedTime;
            Satiety -= 1;

            if (Satiety <= 0)
            {
                Debug.Log("You starved to death");
                //SceneManager.LoadScene(1);
            }
        }
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
        if (other.tag == "LifeItem")
        {
            if (lives < maxLives)
            {
                lives++;
                Destroy(other.gameObject);
				OnCollect.Invoke();
            }
        }
        else if (other.tag == "Food")
        {
            if (Satiety < maxSatiety)
            {
                Satiety++;
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
			OnTorchPlace.Invoke ();
        }
		// restart
		else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
		// start dialog
		else if (Input.GetKeyDown(KeyCode.F) && lastCollision != null)
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
