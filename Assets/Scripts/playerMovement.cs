using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private Collider2D[] colliders;
    private float recordedCoordinateX;
    private float recordedCoordinateY;
    private float coordinateToCheckX;
    private float coordinateToCheckY;

    private Vector3 move;
    private Vector3 stop = new Vector3(0, 0, 0);

    [SerializeField] private float playerSpeed;

    public bool isNorthBlocked = false;
    public bool isSouthBlocked = false;
    public bool isEastBlocked = false;
    public bool isWestBlocked = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isNorthBlocked == false)
        {
            recordedCoordinateY = transform.position.y;
            coordinateToCheckY = recordedCoordinateY += 1;
            isSouthBlocked = false;
            move = (Vector3.up * playerSpeed);

        }
        if (Input.GetKeyDown(KeyCode.S) && isSouthBlocked == false)
        {
            recordedCoordinateY = transform.position.y;
            coordinateToCheckY = recordedCoordinateY -= 1;
            isNorthBlocked = false;
            move = (Vector3.down * playerSpeed);
        }
        if (Input.GetKeyDown(KeyCode.D) && isEastBlocked == false)
        {
            recordedCoordinateY = transform.position.x;
            coordinateToCheckY = recordedCoordinateY += 1;
            isWestBlocked = false;
            move = (Vector3.right * playerSpeed);
        }
        if (Input.GetKeyDown(KeyCode.A) && isWestBlocked == false)
        {
            recordedCoordinateY = transform.position.x;
            coordinateToCheckY = recordedCoordinateY -= 1;
            isEastBlocked = false;
            move = (Vector3.left * playerSpeed);
        }
        Move();

        if (isNorthBlocked)
        {
            move = stop;
        }

        if (isSouthBlocked)
        {
            move = stop;
        }

        if (isEastBlocked)
        {
            move = stop;
        }

        if (isWestBlocked)
        {
            move = stop;
        }


    }


    private void Move()
    {
        transform.Translate(move * Time.deltaTime);
    }



}
