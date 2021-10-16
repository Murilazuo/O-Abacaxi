using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Color openColor, closeColor;
    bool isOpen;

    SpriteRenderer spr;
    Collider2D col;
    void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        openColor = Color.green;
        closeColor = Color.red;

    }
    private void Update()
    {
        isOpen = GameManager.state;

        DoorState();
    }
    void DoorState()
    {
        if (isOpen)
        {
            spr.color = openColor;
            col.enabled = false;
        }
        else
        {
            spr.color = closeColor;
            col.enabled = true;

        }
    }
    
}
