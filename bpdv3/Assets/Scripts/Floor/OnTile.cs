using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;

// This detects when an object has moved on a tile and if it is an enemy.
namespace Floor
{
    public class OnTile : MonoBehaviour
    {
        #region Private & Constant variables

        [SerializeField] private TileProperties _tileProperties;
        
        #endregion

        #region Public & Protected variables
        
        public bool possessToken;
        
        #endregion
     
        #region Constructors
        #endregion

        #region Private methods

        // this needs looking at. I dont think the enemy will work with this commented out.
        // I believe this was done in order for the enemy weakpoint code to work.
        // So we will have to see how this can be included whilst weakpoint code to function also.
        private void Update()
        {
            switch (possessToken)
            {
                case true:
                    _tileProperties.tileWithToken = 1;
                    break;
                case false:
                    _tileProperties.tileWithToken = 0;
                    break;
            }
        }

        public void ResetTokenOnTile()
        {
            possessToken = false;
        }

        #endregion

        #region Public methods
        #endregion
    }
}