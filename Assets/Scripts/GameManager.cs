using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool active;
    public static void ChangeState(bool xAxis)
    {
        active = !active;

        Platform[] platforms = FindObjectsOfType<Platform>();
        foreach(Platform p in platforms)
        {
            p.Revert();
        }
    }
    
}
