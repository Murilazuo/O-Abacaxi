using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private Vector2 direction;

    private Rigidbody2D rig;
    private FollowPlatform followPlatform;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        direction = direction.normalized;
    }
    
    private void FixedUpdate()
    {
        rig.velocity = direction * speed;
    }

    public void Revert()
    {
        direction *= -1;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Wall":
                Revert();
                break;
        }
    }
    
}
