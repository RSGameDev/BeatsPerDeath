using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to an object beneath the player, enemy, coin. So it can attach itself to the tile. As when the tile moves across the object needs to be moving along with it.
public class Anchor : MonoBehaviour
{
    public GameObject tileObj;

    private void OnTriggerEnter(Collider other)
    {
        if ((gameObject.transform.parent.CompareTag("enemy") || gameObject.transform.parent.CompareTag("coin")) && other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            tileObj = other.gameObject;
            Vector3 newPos = other.GetComponent<Renderer>().bounds.center;
            transform.parent.position = new Vector3(newPos.x, newPos.y+1f, newPos.z);
            transform.parent.SetParent(other.transform);
        }        

        if (gameObject.transform.parent.CompareTag("Player") && other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            tileObj = other.gameObject;
            Vector3 newPos = other.GetComponent<Renderer>().bounds.center;
            transform.parent.position = new Vector3(newPos.x, newPos.y + 1f, newPos.z);
            transform.parent.SetParent(other.transform);

            // ~~ Having trouble recalling the functioning behind this although is must be necessary otherwise it would not be included. I'll have to get back to you on this one Kerem.
            if (!other.gameObject.GetComponent<TileProperties>().hasEnemy)
            {
                gameObject.transform.parent.GetComponent<Player>().isPushBack = false;      // 'Push back' is for when the player moves into the enemy but not on an enemy weakpoint, so the player gets pushed back.
            }
        }
    }

    // When the the object moves of the tile. It (the anchor) detaches itself from the tile. 
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            transform.parent.SetParent(transform);
        }
    }    
}
