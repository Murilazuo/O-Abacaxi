using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool active;

    public static GameManager gameManager;

    public static int coin;

    public static int idLevel = 0;

    public static Vector2 checkPooint;
    public Vector2 startCheckPoint;
    public Vector2 checkPointDebug;
    private void Awake()
    {
        if (idLevel != SceneManager.GetActiveScene().buildIndex)
        {
            idLevel = SceneManager.GetActiveScene().buildIndex;
            checkPooint = startCheckPoint;
        }
        gameManager = this;
        checkPointDebug = checkPooint;

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
