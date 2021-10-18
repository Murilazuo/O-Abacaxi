using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private Vector2 speed;
    [SerializeField] private bool up, down, right, left;
    [SerializeField] private WallCheck[] wallColision;

    [SerializeField] private GameObject trail;

    bool fall = false;
    //evento
    public delegate void ChangeStateAction();
    public static event ChangeStateAction OnChangedState;

    [SerializeField]internal Vector2 currentDirection;


    [SerializeField] private bool xAxis;

    bool canChangeState = true;
    public static bool dead = false;
    Door[] doors;

    [SerializeField] GameObject deathEfect;

    Rigidbody2D rig;
    GameManager gameManager;
    FollowPlatform followPlatform;
    Animator anim;
    AudioSource audioSource;
    [SerializeField] AudioClip canMoveAudio;
    [SerializeField] AudioClip checkPoint;
    KeyCode inputUp, inputDown, inputRight, inputLeft;

    
    public  bool canMove = true;
    private void Awake()
    {
        followPlatform = GetComponent<FollowPlatform>();
        followPlatform.hasPlayer = true;
    }
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        gameManager = GameManager.gameManager;
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        inputUp = KeyCode.W;
        inputDown = KeyCode.S;
        inputLeft= KeyCode.A;
        inputRight = KeyCode.D;

        dead = false;

        trail.SetActive(false);
        Spawn();
        trail.SetActive(true);


    }
    private void SetDirWallCheck()
    {
        foreach (WallCheck check in wallColision)
        {
            check.playerDirection = currentDirection;
        }
    }
    void Spawn()
    {
        transform.position = GameManager.checkPooint;   
    }
    void Update()
    {
        if (rig.velocity == Vector2.zero)
        {
            anim.SetBool("Move", false);
        }
        else anim.SetBool("Move", true);

        if (rig.velocity != Vector2.zero || !canMove)
        {
            return;
        }

        Move();


    }
    private void FixedUpdate()
    {
       // Vector2 extraSpeed  = Vector2.zero;
        //if (followPlatform.platform != null)
        //    extraSpeed = followPlatform.rig.velocity;
        if(followPlatform.platform == null)
        {
            rig.velocity = speed;// + extraSpeed;
        }
    }
    private void Move()
    {
        if (Input.GetKeyDown(inputUp))
        {
            if (currentDirection != Vector2.up && !wallColision[0].inColision)
            {
                SetMove(Vector2.up);
                if (wallColision[0].hasDoor)
                {
                     audioSource.PlayOneShot(canMoveAudio);
                        wallColision[0].hasDoor = false;

                }
            }
            else audioSource.PlayOneShot(canMoveAudio);
        }
        else if (Input.GetKeyDown(inputDown))
        {
            if (currentDirection != Vector2.down && !wallColision[1].inColision)
            {
                SetMove(Vector2.down);
                if (wallColision[1].hasDoor)
                {
                    audioSource.PlayOneShot(canMoveAudio);
                    wallColision[1].hasDoor = false;
                }
            }
            else audioSource.PlayOneShot(canMoveAudio);
        }
        if (Input.GetKeyDown(inputLeft))
        {
            if(currentDirection != Vector2.left && !wallColision[2].inColision)
            {
                SetMove(Vector2.left);
                if (wallColision[2].hasDoor)
                {
                    audioSource.PlayOneShot(canMoveAudio);
                    wallColision[2].hasDoor = false;
                }
            }
            else audioSource.PlayOneShot(canMoveAudio);

        }
        if (Input.GetKeyDown(inputRight))
        {
            if(currentDirection != Vector2.right && !wallColision[3].inColision)
            {
                SetMove(Vector2.right);
                if (wallColision[3].hasDoor)
                {
                    audioSource.PlayOneShot(canMoveAudio);
                    wallColision[3].hasDoor = false;
                }
            }
            else audioSource.PlayOneShot(canMoveAudio);

        }
    }

    void Reverse()
    {
        GameManager.ChangeState();
    }

    void SetMove(Vector2 direction)
    {
        if (OnChangedState != null)
        {
            OnChangedState();
        }

        currentDirection = direction;
        SetDirWallCheck();
        speed = direction * baseSpeed;
        Reverse();
    }
    
    public void Stop()
    {
        if (fall) { 
            anim.SetTrigger("Fall"); fall = false;
            ShowNewText.showNewText.NewText("Você Morreu");
        }
        speed = Vector2.zero;
        transform.position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
    }
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            
            case "Reverse":
                speed = speed.normalized * -1 * baseSpeed;
                Reverse();
                break;
            case "Wall":
                Stop();
                break;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Coin":
                collision.GetComponent<Coin>().Collect();
                break;
            case "Nest":
                print(GameManager.checkPooint);
                GameManager.checkPooint = collision.gameObject.transform.position;
                print(GameManager.checkPooint);

                audioSource.PlayOneShot(checkPoint);
                break;
            case "Ground":
                return;
            case "Hole":
                canMove = false;
                fall = true;
                Invoke(nameof(Stop), 0.1f );
                Invoke(nameof(Restart), 2);
                break;
            case "Bullet":
                if(dead == false)
                {
                dead = true;
                Instantiate(deathEfect, transform.position,Quaternion.identity);
                GetComponentInChildren<SpriteRenderer>().enabled = false;
                Stop();
                Invoke(nameof(Restart), 2);
                canMove = false;
                ShowNewText.showNewText.NewText("Você Morreu");
                Destroy(collision.gameObject);
                }
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            collision.GetComponent<Door>().hasPlayer = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door")){
            collision.GetComponent<Door>().hasPlayer = false;
        }
    }


    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
