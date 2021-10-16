using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private Vector2 speed;

    [SerializeField] private bool xAxis;

    bool canChangeState = true;

    Rigidbody2D rig;
    GameManager gameManager;
    FollowPlatform followPlatform;
    Animator anim;
    KeyCode inputUp, inputDown, inputRight, inputLeft, inputStop;

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
        if (Input.GetKeyDown(inputUp))
        {
            SetMove(false, Vector2.up);
        }
        else if (Input.GetKeyDown(inputDown))
        {
            SetMove(false, Vector2.down);
        }
        if (Input.GetKeyDown(inputLeft))
        {
            SetMove(true, Vector2.left);
        }
        if (Input.GetKeyDown(inputRight))
        {
            SetMove(true, Vector2.right);
        }
        

    }

    void Reverse()
    {
        anim.SetBool("xAxis", xAxis);
        if (canChangeState)
        {
            GameManager.ChangeState(xAxis);
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
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        switch (collision.tag)
        {
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
