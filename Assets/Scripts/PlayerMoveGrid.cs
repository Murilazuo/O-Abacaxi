using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveGrid : MonoBehaviour
{
    Vector3 move;
    [SerializeField] private float time;
    [SerializeField] private float speed;
    [SerializeField] private bool inWall;
    bool moving;
    bool stop;



    KeyCode inputUp, inputDown, inputRight, inputLeft;

    [SerializeField] private CheckWallCollision[] wallColl;
    void Start()
    {

        inputUp = KeyCode.W;
        inputDown = KeyCode.S;
        inputLeft = KeyCode.A;
        inputRight = KeyCode.D;

        StartCoroutine(nameof(Move));

    }

    void Update()
    {

        MoveInput();
    }
    void MoveInput()
    {
        if (CanY() && Input.GetKeyDown(inputUp))
        {
            if (move == Vector3.up * speed) return;

            move = Vector3.up * speed;
            GameManager.ChangeState();

        }
        else if (CanY() && Input.GetKeyDown(inputDown))
        {
            if (move == Vector3.down * speed) return;
            move = Vector3.down * speed;

            GameManager.ChangeState();

        }
        if (CanX() && Input.GetKeyDown(inputLeft))
        {
            if(move == Vector3.left * speed) return;
            move = Vector3.left * speed;

            GameManager.ChangeState();

        }
        if (CanX() && Input.GetKeyDown(inputRight))
        {
            if (move == Vector3.right * speed) return;
            move = Vector3.right * speed;
            GameManager.ChangeState();
        }
    }
    IEnumerator Move()
    {
        while (true)
        {
            
            
                transform.Translate(move);
            
            yield return new WaitForFixedUpdate();
        }
    } 
    bool CanY()
    {
        if(wallColl[0].inWall && wallColl[1].inWall)
        {
            return false;
        }
        return true;
    }
    bool CanX()
    {
        if (wallColl[2].inWall && wallColl[3].inWall)
        {
            return false;
        }
        return true;
    }


}
