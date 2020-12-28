using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to an object beneath the player, enemy, coin. So it can attach itself to the tile. As when the tile moves across the object needs to be moving along with it.
public class Anchor : MonoBehaviour
{
    #region Private variables
    const string s_Enemy = "enemy";
    const string s_Coin = "coin";
    const string s_Player = "Player";
    const string s_FloorLayer = "Floor";
    private Vector3 _newPosition;
    #endregion

    #region Public variables
    public GameObject AnchorTileObject;
    #endregion

    //new
    public PlayerMovement playermovement;
    public EnemyMovement enemymovement;

    private void OnTriggerStay(Collider other)
    {
        var tag = gameObject.transform.parent.tag;

        if ((tag == s_Enemy || tag == s_Coin) && other.gameObject.layer == LayerMask.NameToLayer(s_FloorLayer))
        {
            if (!enemymovement.IsEnemyMoving)
            {
                print("anchorattach");
                AttachObjectToTile(other);
            }
            else
            {
                print("anchordettach");
                DetachFromTile();
            }
        }

        if (tag == s_Player && other.gameObject.layer == LayerMask.NameToLayer(s_FloorLayer))
        {
            if (!playermovement.IsInput)
            {
                AttachObjectToTile(other);
            }
            else
            {
                DetachFromTile();
            }             

            // This is for the pushback but it's still a work in progress. Since adding the lerping this isnt working as well. So the pushback feature will need creating again to make it work well again.
            if (!other.gameObject.GetComponent<TileProperties>().hasEnemy)
            {
                gameObject.transform.parent.GetComponent<Player>().IsPlayerPushedBack = false;      
            }
        }
    }

    // This function was made so that the objects will stick to the tiles as the level scrolls. Otherwise the objects would stay in place and the level moves underneath them.
    private void AttachObjectToTile(Collider other)
    {
        AnchorTileObject = other.gameObject;
        _newPosition = other.GetComponent<Renderer>().bounds.center;
        transform.parent.position = new Vector3(_newPosition.x, _newPosition.y + 1f, _newPosition.z);
        transform.parent.SetParent(other.transform);
    }

    // When the object moves off a tile, the parent for the objecxt is reassigned.
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(s_FloorLayer))
        {
            transform.parent.SetParent(transform);
        }
    }   
    
    public void DetachFromTile()
    {
        transform.parent.SetParent(transform);
    }

    public void PlaceInPosition()
    {
        _newPosition = AnchorTileObject.GetComponent<Renderer>().bounds.center;
        transform.parent.position = new Vector3(_newPosition.x, _newPosition.y + 1f, _newPosition.z);
    }
}
