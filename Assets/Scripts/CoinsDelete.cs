using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CoinsDelete : MonoBehaviour, IPointerClickHandler
{
    public int[] pos = new int[3];
    public bool rightCoin = false;
    float speed = 0.4f;
    bool rightPlace = false;
    bool animGo = false;
    bool animEnd = false;
    bool canDel = true;
    public bool ghostCoin = true;
    SpriteRenderer coin_SpriteRenderer;
    string oldLayerName;
    public ParticleSystem sparks;
    private ParticleSystem sparkObj;

    private void Start()
    {
        coin_SpriteRenderer = GetComponent<SpriteRenderer>();
        oldLayerName = coin_SpriteRenderer.sortingLayerName;
        coin_SpriteRenderer.sortingLayerName = "Coin11";
    }
    private void Update()
    {
        if (!ghostCoin)
        {
            if (!rightPlace)
            {
                if (Coins.coinsPosition[pos[0], pos[1], pos[2]].y < gameObject.transform.position.y)
                {
                    gameObject.transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
                    speed *= 1.05f;
                }
                else
                {
                    gameObject.transform.position = Coins.coinsPosition[pos[0], pos[1], pos[2]];
                    rightPlace = true;
                    animGo = true;
                    sparkObj = Instantiate(sparks, transform.position, Quaternion.identity);
                }
            }
            if (animGo)
            {
                if (gameObject.transform.position.y < Coins.coinsPosition[pos[0], pos[1], pos[2]].y + 0.2f)
                {
                    gameObject.transform.Translate(new Vector3(0, 4f * Time.deltaTime, 0));
                    gameObject.transform.Rotate(0.0f, 0.0f, 270.0f * Time.deltaTime, Space.Self);
                }
                else
                {
                    animGo = false;
                    animEnd = true;
                }
            }
            if (animEnd)
            {
                if (gameObject.transform.position.y > Coins.coinsPosition[pos[0], pos[1], pos[2]].y)
                {
                    gameObject.transform.Translate(new Vector3(0, -2f * Time.deltaTime, 0));
                    gameObject.transform.Rotate(0.0f, 0.0f, -135.0f * Time.deltaTime, Space.Self);
                }
                else
                {
                    animEnd = false;
                    coin_SpriteRenderer.sortingLayerName = oldLayerName;
                    gameObject.transform.position = Coins.coinsPosition[pos[0], pos[1], pos[2]];
                    Destroy(sparkObj);
                }
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        for (int y = 0; y < Coins.coinsPosition.GetLength(0); y++)
        {
            for (int z = 0; z < Coins.coinsPosition.GetLength(1); z++)
            {
                for (int x = 0; x < Coins.coinsPosition.GetLength(2); x++)
                {
                    if(gameObject.transform.position == Coins.coinsPosition[y, z, x]) pos = new int[3] {y, z, x};
                }
            }
        }
        canDel = true;
        for (int i = pos[0] + 1; i < Coins.coinsType.GetLength(0); i++)
        {
            if (!(Coins.coinsType[i, pos[1], pos[2]] == 0)) canDel = false;
        }
        if (canDel)
        {
            Coins.coinsType[pos[0], pos[1], pos[2]] = 0;
            for (int z = 0; z < Coins.coinsPosition.GetLength(1); z++)
            {
                for (int x = 0; x < Coins.coinsPosition.GetLength(2); x++)
                {
                    if (Coins.coinsType[pos[0], z, x] == -1) Coins.coinsType[pos[0], z, x] = 0;
                }
            }
            for (int z = 0; z < Coins.coinsPosition.GetLength(1); z++)
            {
                for (int x = 0; x < Coins.coinsPosition.GetLength(2); x++)
                {
                    if (Coins.coinsType[pos[0], z, x] != -1 && Coins.coinsType[pos[0], z, x] != 0)
                    {
                        if (pos[1] != 0 && pos[2] != 0) Coins.coinsType[pos[0], pos[1] - 1, pos[2] - 1] = -1;
                        if (pos[1] != Coins.coinsType.GetLength(1) - 1 && pos[2] != 0) Coins.coinsType[pos[0], pos[1] + 1, pos[2] - 1] = -1;
                        if (pos[1] != 0 && pos[2] != Coins.coinsType.GetLength(2) - 1) Coins.coinsType[pos[0], pos[1] - 1, pos[2] + 1] = -1;
                        if (pos[1] != Coins.coinsType.GetLength(1) - 1 && pos[2] != Coins.coinsType.GetLength(2) - 1) Coins.coinsType[pos[0], pos[1] + 1, pos[2] + 1] = -1;
                        if (pos[1] != 0) Coins.coinsType[pos[0], pos[1] - 1, pos[2]] = -1;
                        if (pos[1] != Coins.coinsType.GetLength(1) - 1) Coins.coinsType[pos[0], pos[1] + 1, pos[2]] = -1;
                        if (pos[2] != 0) Coins.coinsType[pos[0], pos[1], pos[2] - 1] = -1;
                        if (pos[2] != Coins.coinsType.GetLength(2) - 1) Coins.coinsType[pos[0], pos[1], pos[2] + 1] = -1;
                    }
                }
            }
            if (gameObject.name == "CopperCoinOnTable(Clone)")
            {
                SpawnCoins.CountCopperCoin++;
            }
            if (gameObject.name == "SilverCoinOnTable(Clone)")
            {
                SpawnCoins.CountSilverCoin++;
            }
            if (gameObject.name == "GoldCoinOnTable(Clone)")
            {
                SpawnCoins.CountGoldCoin++;
            }
            Destroy(gameObject);
        }
    }
}
