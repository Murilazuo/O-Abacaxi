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


    //evento
    public delegate void ChangeStateAction();
    public static event ChangeStateAction OnChangedState;

    [SerializeField]private Vector2 currentDirection;


    [SerializeField] private bool xAxis;

    bool canChangeState = true;

    Door[] doors;

    Rigidbody2D rig;
    GameManager gameManager;
    FollowPlatform followPlatform;
    Animator anim;
    KeyCode inputUp, inputDown, inputRight, inputLeft;

    
    public  bool canMove = true;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        followPlatform = GetComponent<FollowPlatform>();
        gameManager = GameManager.gameManager;
        anim = GetComponentInChildren<Animator>();

        followPlatform.hasPlayer = true;

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
        print(rig.velocity);

        if (rig.velocity != Vector2.zero)
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
        
        rig.velocity = speed;// + extraSpeed;
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
        SetDirWallCheck();
        canMove = false;
        currentDirection = direction;
        speed = direction * baseSpeed;
        Reverse();
    }
    
    public void Stop()
    {
        speed = Vector2.zero;
    }
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Bullet":
                Hit();
                Destroy(collision.gameObject);
                break;
            case "Reverse":
                speed = speed.normalized * -1 * baseSpeed;
                Reverse();
                break;
            case "Wall":
                canMove = true;
                Stop();
                break;
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
            case "Nest":
                gameManager.checkPooint = collision.gameObject.transform.position;
                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Ground":
                return;
            case "Hole":
                if (followPlatform.platform == null) Hit();
                break;
        }
    }

    private void Hit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
