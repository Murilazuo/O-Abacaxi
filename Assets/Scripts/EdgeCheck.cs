using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeCheck : MonoBehaviour
{
    [SerializeField] private playerMovement PlayerMov;
    [SerializeField] private int myEdgeID;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            switch (myEdgeID)
            {
                case 0:
                    PlayerMov.isNorthBlocked = true;
                    break;
               case 1:
                    PlayerMov.isSouthBlocked = true;
                    break;
                case 2:
                    PlayerMov.isEastBlocked = true;
                    break;
                case 3:
                    PlayerMov.isWestBlocked = true;
                    break;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            switch (myEdgeID)
            {
                case 0:
                    PlayerMov.isNorthBlocked = false;
                    break;
                case 1:
                    PlayerMov.isSouthBlocked = false;
                    break;
                case 2:
                    PlayerMov.isEastBlocked = false;
                    break;
                case 3:
                    PlayerMov.isWestBlocked = false;
                    break;
            }
        }
    }
}
