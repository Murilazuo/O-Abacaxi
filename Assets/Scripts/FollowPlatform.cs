using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlatform : MonoBehaviour
{
    internal Platform platform;
    internal Rigidbody2D rig;


    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            collision.transform.SetParent(transform);
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
            collision.transform.SetParent(null);
            rig = null;
            platform = null;
        }
    }

}
