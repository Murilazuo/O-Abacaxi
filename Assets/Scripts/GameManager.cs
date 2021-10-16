using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool active;

    public static GameManager gameManager;

    public Vector2 checkPooint;
    private void Awake()
    {
        if (gameManager == null) gameManager = this;
        else Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);
    }
    public static void ChangeState(bool xAxis)
    {
        active = !active;

        Platform[] platforms = FindObjectsOfType<Platform>();
        foreach(Platform p in platforms)
        {
            
            p.Revert();
        }
        TrapDoor[] traps = FindObjectsOfType<TrapDoor>();
        foreach (TrapDoor trap in traps)
        {
            trap.ChangerTrapState();
        }
    }
}
