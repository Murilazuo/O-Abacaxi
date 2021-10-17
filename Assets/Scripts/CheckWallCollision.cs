using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWallCollision : MonoBehaviour
{
    public bool inWall;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        inWall = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inWall = false;
    }
}
