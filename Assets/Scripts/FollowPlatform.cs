using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlatform : MonoBehaviour
{
    internal Platform platform;
    internal Rigidbody2D rig;
    internal bool hasPlayer = false;
    internal Player player;


    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        if (hasPlayer)
        {
            player = GetComponent<Player>();
        }
        
    }

    private void Update()
    {
        if (hasPlayer && platform != null)
        {
            platform.SetDireection(player.currentDirection);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Platform"))
        {
            
            transform.SetParent(collision.transform);
            platform = collision.GetComponent<Platform>();
            platform.hasPlayer = hasPlayer;
            rig = collision.GetComponent<Rigidbody2D>();
            transform.localPosition = Vector2.zero;
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
