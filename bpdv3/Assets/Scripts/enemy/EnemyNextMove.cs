using System;
using Floor;
using Managers;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyNextMove : MonoBehaviour
    {
        #region Private & Constant Variables

        [SerializeField] private EnemyMovement _enemyMovement;
        private const string s_Ontile = "OnTile";

        #endregion

        #region Public & Protected Variables

        public bool token = true;
        
        #endregion
       
        
        #region Constructors
        #endregion

        #region Private Methods

        private void OnDisable()
        {
            NewCycle();
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
                if (!other.gameObject.GetComponent<OnTile>().possessToken)
                {
                    other.gameObject.GetComponent<OnTile>().possessToken = true;
                    token = false;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(s_Ontile))
            {
                other.gameObject.GetComponent<OnTile>().possessToken = false;
            }
        }

        #endregion

        #region Public Methods

        public void NewCycle()
        {
            token = true;
        }

        #endregion
    }
}