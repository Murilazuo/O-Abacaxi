using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    private float speedX, speedY;
    private bool xAxis;


    Rigidbody2D rig;
    FollowPlatform followPlatform;
    KeyCode inputX, inputY;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        followPlatform = GetComponent<FollowPlatform>();

        inputX = KeyCode.E;
        inputY = KeyCode.R;

    }

    void Update()
    {
        if (Input.GetKeyDown(inputX))
        {
            RevertX();
        }
        if (Input.GetKeyDown(inputY))
        {
            RevertY();
        }
    }
    private void FixedUpdate()
    {
        rig.velocity = new Vector2(speedX, speedY) + followPlatform.extraSpeed;
    }
    void RevertX()
    {
        xAxis = true;
        speedY = 0;
        if (speedX == 0) speedX = speed;
        else speedX = -speedX;

        GameManager.ChangeState();
    }
    internal void RevertY()
    {
        xAxis = false;
        speedX = 0;
        if (speedY == 0) speedY = speed;
        else speedY = -speedY;
        GameManager.ChangeState();
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
                Destroy(collision.gameObject);
                break;
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Nest":
                speedX = 0;
                speedY = 0;

                break;
        }
    }
    
}
