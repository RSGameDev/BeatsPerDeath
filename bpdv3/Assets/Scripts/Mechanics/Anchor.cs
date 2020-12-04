using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to an object beneath the player, enemy, coin. So it can attach itself to the tile. As when the tile moves across the object needs to be moving along with it.
public class Anchor : MonoBehaviour
{
    #region Private variables
    private Vector3 _newPosition;
    #endregion

    #region Public variables
    public GameObject AnchorTileObject;
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if ((gameObject.transform.parent.CompareTag("enemy") || gameObject.transform.parent.CompareTag("coin")) && other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            AnchorTileObject = other.gameObject;
            _newPosition = other.GetComponent<Renderer>().bounds.center;
            transform.parent.position = new Vector3(_newPosition.x, _newPosition.y+1f, _newPosition.z);
            transform.parent.SetParent(other.transform);
        }        

        if (gameObject.transform.parent.CompareTag("Player") && other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            AnchorTileObject = other.gameObject;
            _newPosition = other.GetComponent<Renderer>().bounds.center;
            
            transform.parent.position = new Vector3(_newPosition.x, _newPosition.y + 1f, _newPosition.z);
           
            transform.parent.SetParent(other.transform);

            // ~~ Having trouble recalling the functioning behind this although is must be necessary otherwise it would not be included. I'll have to get back to you on this one Kerem.
            if (!other.gameObject.GetComponent<TileProperties>().hasEnemy)
            {
                gameObject.transform.parent.GetComponent<Player>().IsPlayerPushedBack = false;      // 'Push back' is for when the player moves into the enemy but not on an enemy weakpoint, so the player gets pushed back.
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
    
    public void PlaceInPosition()
    {
        _newPosition = AnchorTileObject.GetComponent<Renderer>().bounds.center;
        transform.parent.position = new Vector3(_newPosition.x, _newPosition.y + 1f, _newPosition.z);
    }
}
