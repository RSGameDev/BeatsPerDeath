using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is a gameobject placed in front of the enemy. It checks the tile in front of it to work out if another object is destined to go there.
public class EnemyNextMove : MonoBehaviour
{
    public GameObject EnemyNextMoveTileObject;
    public int OccupiedNumber;
    public bool CanEnemyMove;
    public bool NextMoveTrigger; // <- i may remove this, not sure this is needed fyi. 4/12/20 pm

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            NextMoveTrigger = true;
            EnemyNextMoveTileObject = other.gameObject;
            OccupiedNumber = other.GetComponent<TileProperties>().occupiedNum;
        }
    }

    public int CheckNumber()
    {
        OccupiedNumber = EnemyNextMoveTileObject.GetComponent<TileProperties>().occupiedNum;        
        return OccupiedNumber;
    }

    public bool CanMove()
    {
        CanEnemyMove = OccupiedNumber == 1 ? true : false;        
        return CanEnemyMove;
    }
}
