using System;
using Floor;
using Managers;
using UnityEngine;

namespace EnemyNS
{
    public class EnemyNextMove : MonoBehaviour
    {
        #region Private & Constant variables

        [SerializeField] private Enemy _enemy;
        private GameObject nextMoveCurrentTile;
        [SerializeField] private EnemyMovement _enemyMovement;
        private const string s_Ontile = "OnTile";

        #endregion

        #region Public & Protected variables

        public bool nextMoveHasToken = true;

        #endregion


        #region Constructors

        #endregion

        #region Private methods

        private void OnDisable()
        {
            if (_enemy.hasSpawned)
            {
                OnDeath();
                //NewCycle();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_enemyMovement.IsEnemyMoving)
            {
                return;
            }

            if (other.CompareTag(s_Ontile))
            {
                _enemyMovement.NextMoveLocationGO = other.gameObject;
                nextMoveCurrentTile = other.gameObject;
                if (!other.gameObject.GetComponent<OnTile>().tileHasToken)
                {
                    other.gameObject.GetComponent<OnTile>().tileHasToken = true;
                    nextMoveHasToken = false;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(s_Ontile))
            {
                other.gameObject.GetComponent<OnTile>().tileHasToken = false;
            }
        }

        #endregion

        #region Public methods

        public void OnDeath()
        {
            nextMoveCurrentTile.GetComponent<OnTile>().tileHasToken = false;
            nextMoveHasToken = true;
        }
        
        public void NewCycle()
        {
            nextMoveHasToken = true;
        }

        #endregion
    }
}