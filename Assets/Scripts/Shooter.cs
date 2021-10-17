using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float timeToDestroyBullet;
    [SerializeField] private float[] bulletSpeed;
    [SerializeField] private float[] bulletTime;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Vector2Int direction;
    [SerializeField] private bool startActive;
    private bool active;

    
    private int idState = 0;
    private float timer = 0;

    SpriteRenderer spr;
    Animator anim;
    private void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (GameManager.active) active = startActive;
        else active = !startActive;

        if (active) idState = 1;
        else idState = 0;

        anim.SetBool("Active", active);
        if(timer > bulletTime[idState])
        {
            anim.SetTrigger("Shoote");
            var bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bulletObject.GetComponent<Bullet>().SetSpeed(bulletSpeed[idState], direction.x, direction.y,timeToDestroyBullet);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
    }

    
}
