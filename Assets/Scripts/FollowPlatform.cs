using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlatform : MonoBehaviour
{
    internal Vector2 extraSpeed;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            extraSpeed = collision.gameObject.GetComponent<Platform>().platformSpeed;
        }
        else extraSpeed = Vector2.zero;
    }
}
