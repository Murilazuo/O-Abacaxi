using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rig;

    public void SetSpeed(float speed, int dirX, int dirY)
    {
        rig = GetComponent<Rigidbody2D>();
        float angle = 0;
        if (dirY != 0) angle = 90;

        transform.eulerAngles = new Vector3(0,0,angle);
        rig.velocity = new Vector2(speed * dirX, speed * dirY);
    }

}
