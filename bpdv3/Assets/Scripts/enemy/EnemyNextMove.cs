using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNextMove : MonoBehaviour
{
    public GameObject tileObj;
    public int occupiedNum;
    bool canMove;

    public bool nextMoveTrigger;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            nextMoveTrigger = true;
            tileObj = other.gameObject;
            occupiedNum = other.GetComponent<TileProperties>().occupiedNum;
        }
    }

    public int CheckNumber()
    {
        occupiedNum = tileObj.GetComponent<TileProperties>().occupiedNum;        
        return occupiedNum;
    }

    public bool CanMove()
    {
        canMove = occupiedNum == 1 ? true : false;        
        return canMove;
    }
}
