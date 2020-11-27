using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This detects when an object has moved on a tile and if it is an enemy.
public class OnTile : MonoBehaviour
{
    const string s_Enemy = "enemy";
    const string s_NextMove = "NextMove";
    const string s_Coin = "coin";

    private void OnTriggerEnter(Collider other)
    {
        var tag = other.gameObject.tag;

        if (tag == s_Enemy || tag == s_NextMove || tag == s_Coin)
        {
            GetComponentInParent<TileProperties>().OccupiedIncreased();
        }        

        if (tag == s_Enemy)
        {
            GetComponentInParent<TileProperties>().hasEnemy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var tag = other.gameObject.tag;

        if (tag == s_Enemy || tag == s_NextMove)
        {
            GetComponentInParent<TileProperties>().OccupiedDecreased();
        }        

        if (tag == s_Enemy)
        {
            GetComponentInParent<TileProperties>().hasEnemy = false;
        }
    }
}
