using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SpawnCoins : MonoBehaviour
{
    public GameObject copperCoin;
    public GameObject silverCoin;
    public GameObject goldCoin;
    static GameObject coin;

    public static Action TextEvent;

    public static int countCopperCoin = 20;
    public static int CountCopperCoin
    {
        get { return countCopperCoin; }
        set { 
            countCopperCoin = value;
            TextEvent.Invoke();
            }
    }
    public static int countSilverCoin = 20;
    public static int CountSilverCoin
    {
        get { return countSilverCoin; }
        set
        {
            countSilverCoin = value;
            TextEvent.Invoke();
        }
    }
    public static int countGoldCoin = 20;
    public static int CountGoldCoin
    {
        get { return countGoldCoin; }
        set
        {
            countGoldCoin = value;
            TextEvent.Invoke();
        }
    }
    public Text copperCoinText;
    public Text silverCoinText;
    public Text goldCoinText;

    private void Start()
    {
        TextEvent = TextUpdate;
        TextEvent.Invoke();
    }

    public void Spawn(int type)
    {
        if (coin != null)
        {
            if (coin.name == "CopperCoinDrag(Clone)") CountCopperCoin++;
            if (coin.name == "SilverCoinDrag(Clone)") CountSilverCoin++;
            if (coin.name == "GoldCoinDrag(Clone)") CountGoldCoin++;
            Destroy(coin);
        }
        switch(type)
        {        
            case 1:
                if (CountCopperCoin > 0)
                { 
                    coin = Instantiate(copperCoin, new Vector3(0f, 0.2f, -8f), Quaternion.identity);
                    CountCopperCoin--; 
                }
                break;
            case 2:
                if (CountSilverCoin > 0) 
                { 
                    coin = Instantiate(silverCoin, new Vector3(0f, 0.2f, -8f), Quaternion.identity); 
                    CountSilverCoin--;
                }
                break;
            case 3:
                if (CountGoldCoin > 0) 
                { 
                    coin = Instantiate(goldCoin, new Vector3(0f, 0.2f, -8f), Quaternion.identity); 
                    CountGoldCoin--;
                }
                break;
        }
    }
    private void TextUpdate()
    {
        copperCoinText.text = Convert.ToString(countCopperCoin);
        silverCoinText.text = Convert.ToString(countSilverCoin);
        goldCoinText.text = Convert.ToString(countGoldCoin);
    }
}
