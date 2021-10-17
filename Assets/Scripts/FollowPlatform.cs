using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlatform : MonoBehaviour
{
    internal Platform platform;
    internal Rigidbody2D rig;
    internal bool hasPlayer = false;


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
            platform.hasPlayer = hasPlayer;
            rig = collision.GetComponent<Rigidbody2D>();
            
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            platform.hasPlayer = false;

            collision.transform.SetParent(null);
            rig = null;
            platform = null;
        }
    }

}
