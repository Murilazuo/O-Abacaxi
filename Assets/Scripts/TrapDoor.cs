using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    [SerializeField] Sprite open, close;

    public SpriteRenderer spr;
    public float timeTrapDoor;
    [SerializeField]private bool canOpenTrap = true;
    [SerializeField] bool openTrapDoor;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();

        TrapState();
    }
    public void ChangerTrapState()
    {
        if (canOpenTrap) 
        {
            openTrapDoor = !openTrapDoor;
        }

        TrapState();
    }

    void TrapState()
    {
        if (openTrapDoor)
        {
            spr.sprite = open;
            gameObject.tag = "Hole";
        }
        else
        {
            spr.sprite = close;
            gameObject.tag = "Untagged";
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
                canOpenTrap = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            canOpenTrap = true;
        ChangerTrapState();
        
    }

}
