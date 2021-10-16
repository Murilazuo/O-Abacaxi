using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    [SerializeField] Sprite open, close;

    public SpriteRenderer spr;
    Collider2D col;
    public float timeTrapDoor;
    private bool canOpenTrap = true;
    bool openTrapDoor;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();


    }
    public void Teste()
    {
        if (canOpenTrap) 
        {
            StartCoroutine("DoorState");
        }
        //DoorState();
    }
    IEnumerator DoorState()
    {
        openTrapDoor = !openTrapDoor;
        yield return new WaitForSeconds(timeTrapDoor);
        if (openTrapDoor)
        {
            spr.sprite = open;
            //col.enabled = false;
        }
        else
        {
            spr.sprite = close;
            //col.enabled = true;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            if (spr.sprite == open)
            {
                canOpenTrap = false;
            }
            else 
            {
                canOpenTrap = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

                canOpenTrap = true;
          

            
        }
    }

}
