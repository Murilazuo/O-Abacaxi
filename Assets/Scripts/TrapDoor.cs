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

    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
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
                anim.SetBool("Blue", false);
                gameObject.tag = "Hole";
            }

        }
        else
        {
            anim.SetBool("Blue",true);
            gameObject.tag = "Untagged";
        }
    }

    void TriggerOpenTrap()
    {
        anim.SetTrigger("Open");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !anim.GetBool("Blue"))
        {
            TriggerOpenTrap();
        }
    }

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
