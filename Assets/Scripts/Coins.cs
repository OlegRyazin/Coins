using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
    public static Vector3[,,] coinsPosition;
    public static int[,,] coinsType; // -1 - blocked, 0 - empty, 1 - copper, 2 - silver, 3 - gold

    void Start()
    {
        coinsPosition = new Vector3[10, 9, 9];
        coinsType = new int[10, 9, 9];
        for (int y = 0; y < coinsPosition.GetLength(0); y++)
        {
            for (int z = 0; z < coinsPosition.GetLength(1); z++)
            {
                for (int x = 0; x < coinsPosition.GetLength(2); x++)
                {
                    coinsPosition[y, z, x] = new Vector3(-0.6f + x * 0.15f, -1.1f + y * 0.035f, -7.5f + z * 0.2f);
                    coinsType[y, z, x] = 0;
                }
            }
        }
    }
}
