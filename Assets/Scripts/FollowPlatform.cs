using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlatform : MonoBehaviour
{
    internal Platform platform;
    internal Rigidbody2D rig;

    Vector2 extraSpeed;


    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            platform = collision.GetComponent<Platform>();
            rig = collision.GetComponent<Rigidbody2D>();
            
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            rig = null;
            platform = null;
        }
    }

}
