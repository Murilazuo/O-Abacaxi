using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool active;
    public static void ChangeState()
    {
        active = !active;
    }
    
}
