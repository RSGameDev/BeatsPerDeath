using System;
using Floor;
using Managers;
using UnityEngine;

namespace EnemyNS
{
    public class EnemyNextMove : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private EnemyMovement _enemyMovement;
        
        private const string s_Ontile = "OnTile";
        private const string s_Enemy = "enemy";

        //public bool nextMoveHasPermission;
        public bool hasPermission;
        private bool isAssigned;

        public GameObject enemyOnTile;
        
        private void Update()
        {
            if (hasPermission && !isAssigned)
            {
                isAssigned = true;
                _enemyMovement.AssignNextTile();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_enemy.isAlive)
            {
                //if (_enemyMovement.IsEnemyMoving)
                //{
                //    return;
                //}

                if (other.CompareTag(s_Ontile))
                {
                    if (transform.position.z > 6.25f)
                    {
                        return;
                    }
                    _enemyMovement.NextMoveLocationGO = other.gameObject;
                    //if (other.gameObject.GetComponent<OnTile>().tileHasToken == 0)
                    //{
                        other.gameObject.GetComponent<OnTile>().NextMoveGameObjects.Add(gameObject); 
                        other.gameObject.GetComponent<OnTile>().tileHasToken += 1;
                    //}
                }

                if (other.CompareTag(s_Enemy))
                {
                    enemyOnTile = other.gameObject;
                }
                    
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(s_Ontile))
            {
                other.gameObject.GetComponent<OnTile>().tileHasToken -= 1;
                other.gameObject.GetComponent<OnTile>().NextMoveGameObjects.Remove(gameObject); 
            }
        }

        public void ResetValues()
        {
            isAssigned = false;
            hasPermission = false;
        }
    }
}