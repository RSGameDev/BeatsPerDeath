using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
