using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Sprite open, close;

    [SerializeField] private bool isDoorOpen;

    public bool hasPlayer;


    SpriteRenderer spr;
    Collider2D col;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        //StartCoroutine("Checker");
    }
    public void ChangeDoorState()
    {
        isDoorOpen = !isDoorOpen;
        if (hasPlayer)
        {
            Invoke(nameof(DoorState), 0.15f);
        }else DoorState();
    }
    /* public IEnumerator Checker() 
     {
         yield return new WaitForSeconds(.2f);


     }*/

    private void Update()
    {
    }

    void DoorState()
    {
        if (isDoorOpen)
        {
            spr.sprite = open;
            //gameObject.tag = "Door";
            gameObject.tag = "Door";
            col.isTrigger = true;
        }
        else
        {
            col.isTrigger = false;
            spr.sprite = close;
            gameObject.tag = "Wall";
        }

        /*
            if (GameManager.active)
            {
                spr.sprite = open;
                gameObject.tag = "Door";
                col.enabled = false;
            }
            else if(isDoorOpen)
            {
                spr.sprite = close;
                gameObject.tag = "Wall";
                isDoorOpen = false;
                col.enabled = true;
            }*/
    }


    private void OnEnable()
    {
        Player.OnChangedState += ChangeDoorState;
    }
    private void OnDisable()
    {

        Player.OnChangedState -= ChangeDoorState;
    }
}
