using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool active;

    public static GameManager gameManager;

    public static int coin;

    public Vector2 checkPooint;
    private void Awake()
    {
        gameManager = this;
    }
    public static void ChangeState()
    {
        active = !active;

       /* Platform[] platforms = FindObjectsOfType<Platform>();
        foreach(Platform p in platforms)
        {
            
            p.Revert();
        }*/
        TrapDoor[] traps = FindObjectsOfType<TrapDoor>();
        foreach (TrapDoor trap in traps)
        {
            trap.ChangerTrapState();
        }
    }
}
