using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Sprite open, close;

    [SerializeField] private bool isDoorOpen;


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
    }
    /* public IEnumerator Checker() 
     {
         yield return new WaitForSeconds(.2f);


     }*/

    private void Update()
    {
        DoorState();
    }

    void DoorState()
    {
        if (isDoorOpen)
        {
            spr.sprite = open;
            //gameObject.tag = "Door";
            col.enabled = false;
        }
        else
        {
            spr.sprite = close;
            gameObject.tag = "Wall";
            col.enabled = true;
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
