using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform objToFollow;
    void Start()
    {
        objToFollow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = new Vector3(objToFollow.transform.position.x, objToFollow.transform.position.y, transform.position.z);
    }
}
