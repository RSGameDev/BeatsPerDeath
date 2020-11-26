using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is attached to an object in front of each of the spawning locations. To detect if a player is in front of the spawner, so that the coin object will spawn on a different column.
public class SpawnDetectPlayer : MonoBehaviour
{
    public bool playerInFront;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInFront = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInFront = false;
        }
    }
}
