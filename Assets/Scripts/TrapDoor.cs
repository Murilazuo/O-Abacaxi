using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    Color openColor, closeColor;

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
        DoorState();
    }
    void DoorState()
    {
        if (GameManager.active)
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
