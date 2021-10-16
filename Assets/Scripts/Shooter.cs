using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float[] bulletSpeed;
    [SerializeField] private float[] bulletTime;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Vector2Int direction;

    
    private int idState = 0;
    private float timer = 0;
   

    void Update()
    {

        if (GameManager.active) idState = 1;
        else idState = 0;

        if(timer > bulletTime[idState])
        {
            var bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bulletObject.GetComponent<Bullet>().SetSpeed(bulletSpeed[idState], direction.x, direction.y);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }
        


    }

    
}
