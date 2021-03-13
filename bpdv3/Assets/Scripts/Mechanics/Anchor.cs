using System;
using EnemyNS;
using Floor;
using PlayerNS;
using UnityEngine;

// This script is attached to an object beneath the player, enemy, coin. So it can attach itself to the tile. As when the tile moves across the object needs to be moving along with it.
namespace Mechanics
{
    public class Anchor : MonoBehaviour
    {
        #region Private & Constant variables

        private const string s_Enemy = "enemy";
        private const string s_Coin = "coin";
        private const string s_Player = "Player";
        private const string s_FloorLayer = "Floor";
        private Vector3 _newPosition;

        #endregion

        #region Public & Protected variables

        public GameObject anchorTileObject;

        #endregion

        //new
        public Enemy enemy;
        public PlayerMovement playermovement;
        public EnemyMovement enemymovement;

        private string tag;

        private void OnDisable()
        {
            DetachFromTile();
        }

        private void Awake()
        {
            tag = gameObject.transform.parent.tag;
        }

        private void OnTriggerStay(Collider other)
        {
            if ((tag == s_Enemy || tag == s_Coin) && other.gameObject.layer == LayerMask.NameToLayer(s_FloorLayer))
            {
                if (!enemymovement.IsEnemyMoving)
                {
                    //print("anchorattach");
                    AttachObjectToTile(other);
                }
                else
                {
                    //print("anchordettach");
                    DetachFromTile();
                }
            }

            if (tag == s_Player && other.gameObject.layer == LayerMask.NameToLayer(s_FloorLayer))
            {
                if (!playermovement.IsPlayerInputDetected)
                {
                    AttachObjectToTile(other);
                }
                else
                {
                    DetachFromTile();
                }
            }
        }

        // This function was made so that the objects will stick to the tiles as the level scrolls. Otherwise the objects would stay in place and the level moves underneath them.
        private void AttachObjectToTile(Collider other)
        {
            anchorTileObject = other.gameObject;
            _newPosition = other.GetComponent<Renderer>().bounds.center;
            switch (transform.parent.tag)
            {
                case s_Enemy:
                    transform.parent.position = new Vector3(anchorTileObject.transform.position.x,
                        anchorTileObject.transform.position.y + 1f, anchorTileObject.transform.position.z);
                    break;
                case s_Player:
                    transform.parent.position = new Vector3(anchorTileObject.transform.position.x,
                        anchorTileObject.transform.position.y + 0.5f, anchorTileObject.transform.position.z);
                    break;
            }

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
            //anchorTileObject = null;
            transform.parent.SetParent(null);
        }
    }
}