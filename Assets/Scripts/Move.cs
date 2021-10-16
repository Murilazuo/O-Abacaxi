using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float speed;
    private float speedX, speedY;
    private bool xAxis;


    Rigidbody2D rig;
    KeyCode inputX, inputY;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();

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
        rig.velocity = new Vector2(speedX, speedY);
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
        print("test");
        switch (collision.gameObject.tag)
        {
            case "Wall":
                if (xAxis) RevertX();
                else RevertY();
                break;
            case "Bullet":
                break;
        }
    }
}
