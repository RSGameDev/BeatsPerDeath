using System;
using System.Collections.Generic;
using EnemyNS;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

// This detects when an object has moved on a tile and if it is an enemy.
namespace Floor
{
    public class OnTile : MonoBehaviour
    {
        #region Private & Constant variables

        [SerializeField] private TileProperties _tileProperties;
        
        #endregion

        #region Public & Protected variables
        
        public int tileHasToken;
        
        #endregion

        public List<GameObject> NextMoveGameObjects = new List<GameObject>();
        public List<GameObject> GOonTile = new List<GameObject>();
        private GameObject chosenGO;
        
        private bool isCleared;

        #region Constructors
        #endregion

        #region Private methods

        // this needs looking at. I dont think the enemy will work with this commented out.
        // I believe this was done in order for the enemy weakpoint code to work.
        // So we will have to see how this can be included whilst weakpoint code to function also.
        private void Update()
        {
            if (BeatManager.Instance.BeatIndex == 1 || BeatManager.Instance.BeatIndex == 5 && !isCleared)
            {
                isCleared = true;
                ClearObjectList();
            }
            
            if (BeatManager.Instance.BeatIndex == 6)
            {
                isCleared = false;
            }

            switch (tileHasToken)
            {
                case 0:
                    _tileProperties.tileWithToken = 0;
                    break;
                case 1:
                    _tileProperties.tileWithToken = 1;
                    break;
                case 2:
                    _tileProperties.tileWithToken = 2;
                    break;
            }
        }

        private void ClearObjectList()
        {
            //NextMoveGameObjects.Clear();
            //GOonTile.Clear();
            //chosenGO = null;
        }

        public void PermissionToMove()
        {
            if (NextMoveGameObjects.Count == 0)
            {
                print("zero count");
                return;
            }
            
            if (NextMoveGameObjects.Count == 1)
            {
                //print("One count: " + NextMoveGameObjects[0].name);
                NextMoveGameObjects[0].GetComponent<EnemyNextMove>().hasPermission = true;
                chosenGO = NextMoveGameObjects[0];
                //if (NextMoveGameObjects[0].GetComponent<EnemyNextMove>().enemyOnTile == null)
                //{
                //    return;
                //}

                //if (!NextMoveGameObjects[0].GetComponent<EnemyNextMove>().enemyOnTile.GetComponentInChildren<EnemyNextMove>().hasPermission)
                //{
                //    NextMoveGameObjects[0].GetComponent<EnemyNextMove>().hasPermission = false;
                //    return;
                //}
            }

            if (NextMoveGameObjects.Count == 2)
            {
                int allowed = Random.Range(0, NextMoveGameObjects.Count);
                NextMoveGameObjects[allowed].GetComponent<EnemyNextMove>().hasPermission = true;
                chosenGO = NextMoveGameObjects[allowed];
                //print("Two count: chosen object " + NextMoveGameObjects[allowed].name);
            }
            
            

            // _designatedTileGameObject = NextMoveLocationGO;
            //return _enemyNextMove.nextMoveHasPermission;
        }

        public void ContinueOrHold()
        {
            if (GOonTile.Count == 0)
            {
                return;
            }

            if (GOonTile.Count == 1)
            {
                if (!GOonTile[0].GetComponentInChildren<EnemyNextMove>().hasPermission)
                {
                    if (chosenGO == null)
                    {
                        return;
                    }
                    chosenGO.GetComponent<EnemyNextMove>().hasPermission = false;
                }
            }
        }
        
        public void ResetTokenOnTile()
        {
            tileHasToken = 0;
        }

        #endregion

        #region Public methods
        #endregion
    }
}