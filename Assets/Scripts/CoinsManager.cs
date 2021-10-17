using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public static bool[] coinCollect;
    static bool firt = true;
    bool inCombo = false;
    public int comboCount = 0;
    public float time, endTime;

    [SerializeField] private float comboForce;

    AudioSource audioSourse;
    [SerializeField]AudioClip collectCoin;
    private void Awake()
    {
        audioSourse = GetComponent<AudioSource>();
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
    private void Update()
    {
        if (inCombo)
        {
           if(time > endTime)
           {
                inCombo = false;
                comboCount = 0;
           }
          else
          {
            time += Time.deltaTime;
          }
        }
    }
    public void PickupCoin()
    {
        comboCount = Mathf.Clamp(comboCount, 0, 5);
        audioSourse.pitch = .9f + (comboCount / comboForce);
        audioSourse.PlayOneShot(collectCoin);
        time = 0;
        comboCount += 1;
        inCombo = true;
    }
}
