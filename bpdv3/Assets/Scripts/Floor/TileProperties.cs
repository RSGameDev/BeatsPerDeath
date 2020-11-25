using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// A majority of code in this script was to help with the development process for the enemy AI, seeing the numbers for each tile and to see visually with different colour changes also.
// There is a core purpose for this script to, that takes the value of occupancy 'occupiedNum'. Which another script retrieves the value for it's own purposes.
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
        tmp.text = occupiedNum.ToString();                          // Dev purpose only - to see the value for each tile, occupancy status.
        TileColour();
    }

    // Dev purpose only - Visual recognition for tile occupancy
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

    // Once a row has passed the last row hazard point (flames/laser). It's values can reset to zero. Ready for when it scrolls back to the top of the level to be used again.
    public void ResetValue()
    {
        occupiedNum = 0;
    }
}
