using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private Vector2 speed;
    private bool xAxis;

    bool hole = false;

    Rigidbody2D rig;
    GameManager gameManager;
    FollowPlatform followPlatform;
    KeyCode inputX, inputY, inputStop;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        followPlatform = GetComponent<FollowPlatform>();
        gameManager = GameManager.gameManager;

        inputX = KeyCode.D;
        inputY = KeyCode.W;
        inputStop = KeyCode.S;

        Spawn();

    }
    void Spawn()
    {
        if(gameManager.checkPooint != null)
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
        Vector2 extraSpeed  =Vector2.zero;
        if (followPlatform.platform != null)
            extraSpeed = followPlatform.rig.velocity;
        rig.velocity = speed + extraSpeed;
    }
    void RevertX()
    {

        xAxis = true;
        speed.y = 0;
        if (speed.x == 0) speed.x = baseSpeed;
        else speed.x = -speed.x;

        GameManager.ChangeState(xAxis);
    }
    void Stop()
    {
        speed = Vector2.zero;
    }
    internal void RevertY()
    {
        xAxis = false;
        speed.x = 0;
        if (speed.y == 0) speed.y = baseSpeed;
        else speed.y = -speed.y;
        GameManager.ChangeState(xAxis);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Wall":
                if (xAxis) RevertX();
                else RevertY();
                break;
            case "Bullet":
                Hit();
                Destroy(collision.gameObject);
                break;
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Nest":
                gameManager.checkPooint = collision.gameObject.transform.position;
                speed = Vector2.zero;
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
