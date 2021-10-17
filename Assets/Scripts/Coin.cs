using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] Sprite[] coins;



    private void Start()
    {
        int idSpr = Random.Range(0, 4);
        GetComponent<SpriteRenderer>().sprite = coins[idSpr];
    }
    public void Collect()
    {
        GameManager.coin++;
        Destroy(gameObject);
    }
}
