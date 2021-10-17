using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] Sprite[] coins;
    [SerializeField] public int idCoin;


    private void Start()
    {
        if (CoinsManager.coinCollect[idCoin])
        {
            Destroy(gameObject);
        }

        int idSpr = Random.Range(0, 4);
        GetComponent<SpriteRenderer>().sprite = coins[idSpr];
    }
    public void Collect()
    {
        GameManager.coin++;
        CoinsManager.coinCollect[idCoin] = true;
        Destroy(gameObject);
    }
}
