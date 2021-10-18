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
    private BoxCollider2D thisCollider;
    private Collider2D playerCollider;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        thisCollider = GetComponent<BoxCollider2D>();
        playerCollider = FindObjectOfType<Player>().GetComponent<Collider2D>();
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
            if (thisCollider.IsTouching(playerCollider))
            {
                StartCoroutine("waitPlayerLeave");
            }
            else 
            {
                spr.sprite = open;
                gameObject.tag = "Hole";
            }

        }
        else
        {
            spr.sprite = close;
            gameObject.tag = "Untagged";
        }
    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
                canOpenTrap = false;
        }
    }*/
    /*private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            canOpenTrap = true;
        ChangerTrapState();
        
    }*/
    public IEnumerator waitPlayerLeave() 
    {
        yield return new WaitForSeconds(.15f);
        TrapState();
    }
}
