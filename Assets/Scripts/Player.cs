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
    [SerializeField] private CheckWallCollision[] wallColision;


    //evento
    public delegate void ChangeStateAction();
    public static event ChangeStateAction OnChangedState;




    [SerializeField] private bool xAxis;

    bool canChangeState = true;

    Door[] doors;

    Rigidbody2D rig;
    GameManager gameManager;
    FollowPlatform followPlatform;
    Animator anim;
    KeyCode inputUp, inputDown, inputRight, inputLeft;

    void SetDirFalse()
    {
        up = false;
        down = false;
        right = false;
        left = false;
    }
    public static bool canMove;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        followPlatform = GetComponent<FollowPlatform>();
        gameManager = GameManager.gameManager;
        anim = GetComponentInChildren<Animator>();

        inputUp = KeyCode.W;
        inputDown = KeyCode.S;
        inputLeft= KeyCode.A;
        inputRight = KeyCode.D;

        Spawn();



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
        if (canMove)
        {
            Stop();
            return;
        }

        Move();


    }
    private void FixedUpdate()
    {
        Vector2 extraSpeed  = Vector2.zero;
        if (followPlatform.platform != null)
            extraSpeed = followPlatform.rig.velocity;

        rig.velocity = speed + extraSpeed;
    }
    private void Move()
    {
        if (Input.GetKeyDown(inputUp) && !checkUp.inColision && !up)
        {
            SetDirFalse();
            up = true;
            SetMove(false, Vector2.up);
        }
        else if (Input.GetKeyDown(inputDown) && !checkDown.inColision && !down)
        {
            SetDirFalse();
            down = true;
            SetMove(false, Vector2.down);
        }
        if (Input.GetKeyDown(inputLeft) && !checkLeft.inColision && !left)
        {
            SetDirFalse();
            left = true;
            SetMove(true, Vector2.left);
        }
        if (Input.GetKeyDown(inputRight) && !checkRight.inColision && !right)
        {
            SetDirFalse();
            right = true;
            SetMove(true, Vector2.right);
        }
        

    }

    void Reverse()
    {
        anim.SetBool("xAxis", xAxis);
        GameManager.ChangeState();

        if (OnChangedState != null) 
        {
            OnChangedState();
        }

    }

    void SetMove(bool axis, Vector2 direction)
    {
        xAxis = axis;
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
                canChangeState = false;
                Stop();
                break;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Wall":
                canChangeState = true;
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
            case "Hole":
                if (followPlatform.platform == null) Hit();
                break;

        }
    }
  /*  private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Hole":
                if (followPlatform.platform == null) Hit();
                break;
        }
    }*/

    private void Hit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
}
