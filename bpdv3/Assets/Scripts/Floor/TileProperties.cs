using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TileProperties : MonoBehaviour
{
    public bool hasEnemy;
    public int occupiedNum = 0;

    public GameObject text;
    TextMeshPro tmp;
    public Material material0;
    public Material material1;
    public Material material2;
    public Material material3;

    // Start is called before the first frame update
    void Start()
    {
        tmp = text.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        tmp.text = occupiedNum.ToString();
        TileColour();
    }

    private void TileColour()
    {
        if (occupiedNum == 0)
        {
            GetComponent<Renderer>().material = material0;
        }

        if (occupiedNum == 1)
        {
            GetComponent<Renderer>().material = material1;
        }

        if (occupiedNum == 2)
        {
            GetComponent<Renderer>().material = material2;
        }

        if (occupiedNum == 3)
        {
            GetComponent<Renderer>().material = material3;
        }
    }

    public void OccupiedIncreased()
    {
        occupiedNum++;
    }

    public void OccupiedDecreased()
    {
        occupiedNum--;
        if (occupiedNum < 0)
        {
            occupiedNum = 0;
        }
    }    

    public void ResetValue()
    {
        occupiedNum = 0;
    }
}
