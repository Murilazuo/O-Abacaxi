using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]Sprite open, close;

    SpriteRenderer spr;
    Collider2D col;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

    }
    private void Update()
    {
        DoorState();
    }
    void DoorState()
    {
        if (GameManager.active)
        {
            spr.sprite = open;
            col.enabled = false;
        }
        else
        {
            spr.sprite = close;
            col.enabled = true;
        }
    }
    
}
