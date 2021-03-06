using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck : MonoBehaviour
{
    [SerializeField] internal bool inColision = false;
    [SerializeField] internal Vector2 playerDirection;
    [SerializeField] private Vector2 direction;
    [SerializeField] internal bool hasDoor = false;
    Player player;
    private void Start()
    {
        player = GetComponentInParent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.CompareTag("Wall"))
            {
                inColision = true;
                hasDoor = false;
            }
            if (collision.CompareTag("Door") && collision.GetComponent<Collider2D>().isTrigger)
             {
                hasDoor = true;
             }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            inColision = false;
            
        }
        if (collision.CompareTag("Door"))
        {
            hasDoor = false;
            inColision = false;
        }

    }
    
}
