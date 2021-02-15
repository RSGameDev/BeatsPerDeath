using System;
using UnityEngine;

// This detects when an object has moved on a tile and if it is an enemy.
namespace Floor
{
    public class OnTile : MonoBehaviour
    {
        #region Private & Constant variables
        
        private const string s_Enemy = "enemy";
        private const string s_NextMove = "NextMove";
        private const string s_Coin = "coin";
        private const float s_areaWhereInactiveTopLimit = 6.5f;
        private const float s_areaWhereInactiveBottomLimit = 1f;
        private Collider _collider;

        #endregion

        #region Public & Protected variables
        #endregion

        #region Constructors
   
        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        #endregion

        #region Private Methods

        private void OnTriggerEnter(Collider other)
        {
            var otherTag = other.gameObject.tag;

            if (otherTag == s_Enemy || otherTag == s_NextMove || otherTag == s_Coin)
            {
                GetComponentInParent<TileProperties>().OccupiedIncreased();
            }        

            if (otherTag == s_Enemy)
            {
                GetComponentInParent<TileProperties>().HasEnemy = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var otherTag = other.gameObject.tag;

            if (otherTag == s_Enemy || otherTag == s_NextMove)
            {
                GetComponentInParent<TileProperties>().OccupiedDecreased();
            }        

            if (otherTag == s_Enemy)
            {
                GetComponentInParent<TileProperties>().HasEnemy = false;
            }
        }

        // This is carried out so the top row where objects spawn do not interfere with the tile ontile collider. So the ontile colliders are
        // turned off for the top row and for the final row. For unwanted collision reasons here also.
        private void Update()
        {
            if (transform.position.z >= s_areaWhereInactiveTopLimit || transform.position.z <= s_areaWhereInactiveBottomLimit)
            {
                _collider.enabled = false;
            }
            else
            {
                _collider.enabled = true;
            }
        }

        #endregion

        #region Public methods
        #endregion
        
    }
}
