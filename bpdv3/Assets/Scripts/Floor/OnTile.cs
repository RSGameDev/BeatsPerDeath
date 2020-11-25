using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This detects when an object has moved on a tile and if it is an enemy.
public class OnTile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("NextMove") || other.gameObject.CompareTag("coin"))
        {
            GetComponentInParent<TileProperties>().OccupiedIncreased();
        }

        if (other.gameObject.CompareTag("enemy"))
        {
            GetComponentInParent<TileProperties>().hasEnemy = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("NextMove") || other.gameObject.CompareTag("enemy"))
        {
            GetComponentInParent<TileProperties>().OccupiedDecreased();
        }

        if (other.gameObject.CompareTag("enemy"))
        {
            GetComponentInParent<TileProperties>().hasEnemy = false;
        }
    }
}
