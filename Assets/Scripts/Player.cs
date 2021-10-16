using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private Vector2 speed;

    [SerializeField]private Vector2 lastDir;
    private bool xAxis;

    bool hole = false;
    bool canChangeState = true;

    Rigidbody2D rig;
    GameManager gameManager;
    FollowPlatform followPlatform;
    Animator anim;
    KeyCode inputX, inputY, inputStop;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        followPlatform = GetComponent<FollowPlatform>();
        gameManager = GameManager.gameManager;
        anim = GetComponentInChildren<Animator>();

        inputX = KeyCode.D;
        inputY = KeyCode.W;
        inputStop = KeyCode.S;

        lastDir = Vector2.one;

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
        if (Input.GetKeyDown(inputX))
        {
            RevertX();
        } 
        else if (Input.GetKeyDown(inputY))
        {
            RevertY();
        } 
        else if (Input.GetKeyDown(inputStop))
        {
            Stop();
        }

        

    }
    private void FixedUpdate()
    {
        Vector2 extraSpeed  = Vector2.zero;
        if (followPlatform.platform != null)
            extraSpeed = followPlatform.rig.velocity;
        rig.velocity = speed + extraSpeed;
    }
    
    void RevertX()
    {
        
        if (lastDir.y == 0)
        {
            lastDir.y = 1;
        }
        else
        {
            lastDir.y *= -1 ;
        }

        xAxis = true;
        speed.y = 0;
        if (speed.x == 0) speed.x = baseSpeed * lastDir.x;
        else speed.x = -speed.x;

        anim.SetBool("xAxis", xAxis);
        if (canChangeState)
        {
            GameManager.ChangeState(xAxis);
        }
    }
    void Stop()
    {
        speed = Vector2.zero;
    }
    internal void RevertY()
    {
        if (lastDir.x == 0)
        {
            lastDir.x = 1;
        }
        else
        {
            lastDir.x *= -1;
        }

        xAxis = false;
        speed.x = 0;
        if (speed.y == 0) speed.y = baseSpeed * lastDir.y;
        else speed.y = -speed.y;

        anim.SetBool("xAxis", xAxis);
        if (canChangeState) { 
        GameManager.ChangeState(xAxis);
        }
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
                if (xAxis) RevertX();
                else RevertY();
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
