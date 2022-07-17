using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragCoins : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public LineRenderer line;
    private Vector3 pointScreen;
    private Vector3 offset;
    private GameObject temporaryCoin;
    SpriteRenderer coin_SpriteRenderer;
    public GameObject Coin;
    public int typeCoin;
    int[] pos = new int[3] { -1, -1, -1 };
    bool createCoin = false;
    bool coinHavePlace = false;
    bool coinHaveBase = false;

    public void OnBeginDrag(PointerEventData eventData)
    {
        pointScreen = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, pointScreen.z));
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pointScreen.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint);
        transform.position = new Vector3(curPosition.x, 0.2f, curPosition.y * 5f - 7f);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if(createCoin)
        {          
            if (pos[1] != 0 && pos[2] != 0) Coins.coinsType[pos[0], pos[1] - 1, pos[2] - 1] = -1;
            if (pos[1] != Coins.coinsType.GetLength(1) - 1 && pos[2] != 0) Coins.coinsType[pos[0], pos[1] + 1, pos[2] - 1] = -1;
            if (pos[1] != 0 && pos[2] != Coins.coinsType.GetLength(2) - 1) Coins.coinsType[pos[0], pos[1] - 1, pos[2] + 1] = -1;
            if (pos[1] != Coins.coinsType.GetLength(1) - 1 && pos[2] != Coins.coinsType.GetLength(2) - 1) Coins.coinsType[pos[0], pos[1] + 1, pos[2] + 1] = -1;
            if (pos[1] != 0) Coins.coinsType[pos[0], pos[1] - 1, pos[2]] = -1;
            if (pos[1] != Coins.coinsType.GetLength(1) - 1) Coins.coinsType[pos[0], pos[1] + 1, pos[2]] = -1;
            if (pos[2] != 0) Coins.coinsType[pos[0], pos[1], pos[2] - 1] = -1;
            if (pos[2] != Coins.coinsType.GetLength(2) - 1) Coins.coinsType[pos[0], pos[1], pos[2] + 1] = -1;

            Coins.coinsType[pos[0], pos[1], pos[2]] = typeCoin;
            GameObject newCoin = Instantiate(Coin, new Vector3(Coins.coinsPosition[pos[0], pos[1], pos[2]].x, transform.position.y, Coins.coinsPosition[pos[0], pos[1], pos[2]].z), Quaternion.identity);
            newCoin.GetComponent<CoinsDelete>().pos = new int[3] {pos[0], pos[1], pos[2]};
            newCoin.GetComponent<CoinsDelete>().ghostCoin = false;
            coin_SpriteRenderer = newCoin.GetComponent<SpriteRenderer>();
            coin_SpriteRenderer.sortingLayerName = "Coin" + (pos[0] + 1);
            Destroy(temporaryCoin);
            Destroy(gameObject);
        }
        else
        {
            transform.position = new Vector3(0f, 0.2f, -8f);
        }   
    }

    private void Start()
    {
        line.startWidth = 0.01f;
        line.endWidth = 0.01f;     
    }

    private void LateUpdate()
    {
        createCoin = false;
        Destroy(temporaryCoin);
        line.startColor = Color.red;
        line.endColor = Color.red;
        int[] newPos = new int[3] { -1, -1, -1};
        for (int z = 0; z < Coins.coinsPosition.GetLength(1); z++)
        {
            for (int x = 0; x < Coins.coinsPosition.GetLength(2); x++)
            {
                if(Coins.coinsPosition[0, z, x].x - 0.15f < transform.position.x && Coins.coinsPosition[0, z, x].x + 0.15f > transform.position.x && Coins.coinsPosition[0, z, x].z - 0.15f < transform.position.z && Coins.coinsPosition[0, z, x].z + 0.15f > transform.position.z)
                {
                    newPos = new int[3] { 0, z, x };
                }
            }
        }
        line.SetPosition(0, transform.position);
        if (newPos[0] == 0)
        {
            for (int y = 0; y < Coins.coinsType.GetLength(0); y++)
            {
                if (Coins.coinsType[y, newPos[1], newPos[2]] != 0) newPos[0] = y + 1;
            }
            if (newPos[0] != 9)
            {
                coinHavePlace = true;
                if (newPos[1] != 0 && newPos[2] != 0) if (Coins.coinsType[newPos[0], newPos[1] - 1, newPos[2] - 1] == 0 || Coins.coinsType[newPos[0], newPos[1] - 1, newPos[2] - 1] == -1);
                else coinHavePlace = false;
                if (newPos[1] != Coins.coinsType.GetLength(1) - 1 && newPos[2] != 0) if (Coins.coinsType[newPos[0], newPos[1] + 1, newPos[2] - 1] == 0 || Coins.coinsType[newPos[0], newPos[1] + 1, newPos[2] - 1] == -1);
                    else coinHavePlace = false;
                if (newPos[1] != 0 && newPos[2] != Coins.coinsType.GetLength(2) - 1) if (Coins.coinsType[newPos[0], newPos[1] - 1, newPos[2] + 1] == 0 || Coins.coinsType[newPos[0], newPos[1] - 1, newPos[2] + 1] == -1);
                    else coinHavePlace = false;
                if (newPos[1] != Coins.coinsType.GetLength(1) - 1 && newPos[2] != Coins.coinsType.GetLength(2) - 1) if (Coins.coinsType[newPos[0], newPos[1] + 1, newPos[2] + 1] == 0 || Coins.coinsType[newPos[0], newPos[1] + 1, newPos[2] + 1] == -1);
                    else coinHavePlace = false;
                if (newPos[1] != Coins.coinsType.GetLength(1) - 1) if (Coins.coinsType[newPos[0], newPos[1] + 1, newPos[2]] == 0 || Coins.coinsType[newPos[0], newPos[1] + 1, newPos[2]] == -1);
                    else coinHavePlace = false;
                if (newPos[1] != 0) if (Coins.coinsType[newPos[0], newPos[1] - 1, newPos[2]] == 0 || Coins.coinsType[newPos[0], newPos[1] - 1, newPos[2]] == -1);
                    else coinHavePlace = false;
                if (newPos[2] != Coins.coinsType.GetLength(2) - 1) if (Coins.coinsType[newPos[0], newPos[1], newPos[2] + 1] == 0 || Coins.coinsType[newPos[0], newPos[1], newPos[2] + 1] == -1);
                    else coinHavePlace = false;
                if (newPos[2] != 0) if (Coins.coinsType[newPos[0], newPos[1], newPos[2] - 1] == 0 || Coins.coinsType[newPos[0], newPos[1], newPos[2] - 1] == -1);
                    else coinHavePlace = false;
                if (newPos[0] == 0)
                {
                    coinHaveBase = true;
                }
                else
                {               
                    newPos[0] -= 1;
                    coinHaveBase = true;
                    if (newPos[1] != 0 && newPos[2] != 0) if (Coins.coinsType[newPos[0], newPos[1] - 1, newPos[2] - 1] == 0) coinHaveBase = false;
                    if (newPos[1] != Coins.coinsType.GetLength(1) - 1 && newPos[2] != 0) if (Coins.coinsType[newPos[0], newPos[1] + 1, newPos[2] - 1] == 0) coinHaveBase = false;
                    if (newPos[1] != 0 && newPos[2] != Coins.coinsType.GetLength(2) - 1) if (Coins.coinsType[newPos[0], newPos[1] - 1, newPos[2] + 1] == 0) coinHaveBase = false;
                    if (newPos[1] != Coins.coinsType.GetLength(1) - 1 && newPos[2] != Coins.coinsType.GetLength(2) - 1) if (Coins.coinsType[newPos[0], newPos[1] + 1, newPos[2] + 1] == 0) coinHaveBase = false;
                    if (newPos[1] != Coins.coinsType.GetLength(1) - 1) if (Coins.coinsType[newPos[0], newPos[1] + 1, newPos[2]] == 0) coinHaveBase = false;
                    if (newPos[1] != 0) if (Coins.coinsType[newPos[0], newPos[1] - 1, newPos[2]] == 0) coinHaveBase = false;
                    if (newPos[2] != Coins.coinsType.GetLength(2) - 1) if (Coins.coinsType[newPos[0], newPos[1], newPos[2] + 1] == 0) coinHaveBase = false;
                    if (newPos[2] != 0) if (Coins.coinsType[newPos[0], newPos[1], newPos[2] - 1] == 0) coinHaveBase = false;

                    int countBase = 0;
                    if (Coins.coinsType[newPos[0], newPos[1], newPos[2]] != 0 && Coins.coinsType[newPos[0], newPos[1], newPos[2]] != -1) countBase += 4;
                    if (newPos[1] != 0 && newPos[2] != 0) if (Coins.coinsType[newPos[0], newPos[1] - 1, newPos[2] - 1] != 0) countBase++;
                    if (newPos[1] != Coins.coinsType.GetLength(1) - 1 && newPos[2] != 0) if (Coins.coinsType[newPos[0], newPos[1] + 1, newPos[2] - 1] != 0 && Coins.coinsType[newPos[0], newPos[1] + 1, newPos[2] - 1] != -1) countBase++;
                    if (newPos[1] != 0 && newPos[2] != Coins.coinsType.GetLength(2) - 1) if (Coins.coinsType[newPos[0], newPos[1] - 1, newPos[2] + 1] != 0 && Coins.coinsType[newPos[0], newPos[1] - 1, newPos[2] + 1] != -1) countBase++;
                    if (newPos[1] != Coins.coinsType.GetLength(1) - 1 && newPos[2] != Coins.coinsType.GetLength(2) - 1) if (Coins.coinsType[newPos[0], newPos[1] + 1, newPos[2] + 1] != 0 && Coins.coinsType[newPos[0], newPos[1] + 1, newPos[2] + 1] != -1) countBase++;
                    if (newPos[1] != Coins.coinsType.GetLength(1) - 1) if (Coins.coinsType[newPos[0], newPos[1] + 1, newPos[2]] != 0 && Coins.coinsType[newPos[0], newPos[1] + 1, newPos[2]] != -1) countBase += 2;
                    if (newPos[1] != 0) if (Coins.coinsType[newPos[0], newPos[1] - 1, newPos[2]] != 0 && Coins.coinsType[newPos[0], newPos[1] - 1, newPos[2]] != -1) countBase += 2;
                    if (newPos[2] != Coins.coinsType.GetLength(2) - 1) if (Coins.coinsType[newPos[0], newPos[1], newPos[2] + 1] != 0 && Coins.coinsType[newPos[0], newPos[1], newPos[2] + 1] != -1) countBase += 2;
                    if (newPos[2] != 0) if (Coins.coinsType[newPos[0], newPos[1], newPos[2] - 1] != 0 && Coins.coinsType[newPos[0], newPos[1], newPos[2] - 1] != -1) countBase += 2;
                    if(countBase <= 3) coinHaveBase = false;
                    newPos[0] += 1;
                }
                if (coinHavePlace && coinHaveBase)
                {
                    line.startColor = Color.green;
                    line.endColor = Color.green;
                    line.SetPosition(1, new Vector3(Coins.coinsPosition[newPos[0], newPos[1], newPos[2]].x, Coins.coinsPosition[newPos[0], newPos[1], newPos[2]].y, Coins.coinsPosition[newPos[0], newPos[1], newPos[2]].z));
                    temporaryCoin = Instantiate(Coin, Coins.coinsPosition[newPos[0], newPos[1], newPos[2]], Quaternion.identity);
                    coin_SpriteRenderer = temporaryCoin.GetComponent<SpriteRenderer>();
                    coin_SpriteRenderer.sortingLayerName = "Coin" + (newPos[0] + 1);
                    coin_SpriteRenderer.color = new Color(0, 0, 0, 0.5f);
                    pos = newPos;
                    createCoin = true;
                }
                else line.SetPosition(1, new Vector3(transform.position.x, -2f, transform.position.z));
            }
            else line.SetPosition(1, new Vector3(transform.position.x, -2f, transform.position.z));
        }    
        else line.SetPosition(1, new Vector3(transform.position.x, -2f, transform.position.z));
    }
}
