using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private Vector2 direction;

    private BoxCollider2D thisCollider;
    private Collider2D playerCollider;

    private Rigidbody2D rig;

    public bool hasPlayer;

    private bool stop = false;
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        direction = direction.normalized;
        playerCollider = FindObjectOfType<Player>().GetComponent<Collider2D>();
        thisCollider = GetComponent<BoxCollider2D>();
    }
    
    private void FixedUpdate()
    {
        rig.velocity = direction * speed;
    }
    
    public void Revert()
    {
        
            direction *= -1;
        
        /*
        if (thisCollider.IsTouching(playerCollider) == true)
        {
            StartCoroutine("waitPlayerLeave");
        }
        else 
        {
            direction *= -1;
        }*/

    }

    public IEnumerator WaitPlayerLeave()
    {
        stop = true;
        yield return new WaitForSeconds(.5f);
        Revert();
        stop = false;
    }

    public void SetDireection(Vector2 newDir)
    {
        direction = newDir;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Wall":
                if (hasPlayer) break;
                direction = Vector2.zero;
                StartCoroutine(nameof(WaitPlayerLeave));
                break;
        }
    }

  /*  private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag) 
        {
            case "Player":
                print("UEEEPA");
                direction *= -1;
                break;
        }

    }*/

    //private void OnEnable()
    //{
    //    Player.OnChangedState += Revert;
    //}
    //private void OnDisable()
    //{

    //    Player.OnChangedState -= Revert;
    //}
}
