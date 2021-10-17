using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public static bool[] coinCollect;
    static bool firt = true;
    private void Awake()
    {
        if (firt)
        {
            firt = false;
            coinCollect = new bool[transform.childCount];
        }

        int idCoins = 0;
        foreach(Transform coin in transform)
        {
            coin.GetComponent<Coin>().idCoin = idCoins;
            idCoins++;
        }
    }
}
