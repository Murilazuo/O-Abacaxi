using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private Vector2 speed;
    [SerializeField] private WallCheck checkUp, checkDown, checkRight, checkLeft;
    [SerializeField] private bool up, down, right, left;
    [SerializeField] private WallCheck[] wallColision;
    [SerializeField] private GameObject[] wallCollisionOBJ;

    bool fall = false;
    //evento
    public delegate void ChangeStateAction();
    public static event ChangeStateAction OnChangedState;

    [SerializeField]internal Vector2 currentDirection;


    [SerializeField] private bool xAxis;

    bool canChangeState = true;

    Door[] doors;

    Rigidbody2D rig;
    GameManager gameManager;
    FollowPlatform followPlatform;
    Animator anim;
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


        inputUp = KeyCode.W;
        inputDown = KeyCode.S;
        inputLeft= KeyCode.A;
        inputRight = KeyCode.D;

        Spawn();



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
        if(gameManager.checkPooint != Vector2.zero)
        {
            transform.position = gameManager.checkPooint;
        }
    }
    void Update()
    {

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
        if (Input.GetKeyDown(inputUp) && currentDirection != Vector2.up && !wallColision[0].inColision)
        {
            SetMove(Vector2.up);

        }
        else if (Input.GetKeyDown(inputDown) && currentDirection != Vector2.down && !wallColision[1].inColision)
        {
            SetMove(Vector2.down);

        }
        if (Input.GetKeyDown(inputLeft) && currentDirection != Vector2.left && !wallColision[2].inColision)
        {
            SetMove(Vector2.left);

        }
        if (Input.GetKeyDown(inputRight) && currentDirection != Vector2.right && !wallColision[3].inColision)
        {
            SetMove(Vector2.right);

        }
    }

    void Reverse()
    {
        anim.SetBool("xAxis", xAxis);
        GameManager.ChangeState();
    }

    void SetMove(Vector2 direction)
    {
        if (OnChangedState != null)
        {
            OnChangedState();
        }

        //canMove = false;
        currentDirection = direction;
        SetDirWallCheck();
        speed = direction * baseSpeed;
        Reverse();
    }
    
    public void Stop()
    {
        if (fall) { anim.SetTrigger("Fall"); fall = false; }

        speed = Vector2.zero;
    }
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Bullet":
                Restart();
                Destroy(collision.gameObject);
                break;
            case "Reverse":
                speed = speed.normalized * -1 * baseSpeed;
                Reverse();
                break;
            /*case "Wall":
                canMove = true;
                break;*/
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Wall":
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
                gameManager.checkPooint = collision.gameObject.transform.position;
                break;
            case "Ground":
                return;
            case "Hole":
                canMove = false;
                fall = true;
                Invoke(nameof(Stop), 0.1f * baseSpeed);
                Invoke(nameof(Restart), 2);
                break;
        }
    }
    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
