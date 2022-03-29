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
    public static int countCopperCoin = 20;
    public static int countSilverCoin = 20;
    public static int countGoldCoin = 20;
    public Text copperCoinText;
    public Text silverCoinText;
    public Text goldCoinText;
    public void Spawn(int type)
    {
        if (coin != null)
        {
            if (coin.name == "CopperCoinDrag(Clone)") countCopperCoin++;
            if (coin.name == "SilverCoinDrag(Clone)") countSilverCoin++;
            if (coin.name == "GoldCoinDrag(Clone)") countGoldCoin++;
            Destroy(coin);
        }
        switch(type)
        {        
            case 1:
                if (countCopperCoin > 0)
                { 
                    coin = Instantiate(copperCoin, new Vector3(0f, 0.2f, -8f), Quaternion.identity);
                    countCopperCoin--; 
                }
                break;
            case 2:
                if (countSilverCoin > 0) 
                { 
                    coin = Instantiate(silverCoin, new Vector3(0f, 0.2f, -8f), Quaternion.identity); 
                    countSilverCoin--;
                }
                break;
            case 3:
                if (countGoldCoin > 0) 
                { 
                    coin = Instantiate(goldCoin, new Vector3(0f, 0.2f, -8f), Quaternion.identity); 
                    countGoldCoin--;
                }
                break;
        }
    }
    private void Update()
    {
        copperCoinText.text = Convert.ToString(countCopperCoin);
        silverCoinText.text = Convert.ToString(countSilverCoin);
        goldCoinText.text = Convert.ToString(countGoldCoin);
    }
}
