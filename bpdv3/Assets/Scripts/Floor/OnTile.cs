using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy") || other.gameObject.CompareTag("NextMove") || other.gameObject.CompareTag("coin"))
        {
            print("ontile " + other.name);
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
