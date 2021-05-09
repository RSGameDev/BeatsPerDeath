using System;
using Floor;
using Managers;
using UnityEngine;

namespace EnemyNS
{
    public class EnemyNextMove : MonoBehaviour
    {
        [SerializeField] private EnemyDirection _enemyDirection;
        [SerializeField] private EnemyMovement _enemyMovement;
        private Enemy _enemy;
        public GameObject nextMoveLocation;

        private const string s_Ontile = "OnTile";

        public bool hasPermission;

        private void OnDisable()
        {
            nextMoveLocation = null;
            hasPermission = false;
        }

        //private void Update()
        //{
        //    if ((BeatManager.Instance.BeatIndex == 0 || BeatManager.Instance.BeatIndex == 4) && !_resetValues)
        //    {
        //        _resetValues = true;
        //        _deductTile = false;
        //    }
        //    
        //    if ((BeatManager.Instance.BeatIndex == 1 || BeatManager.Instance.BeatIndex == 5) && !_deductTile)
        //    {
        //        _resetValues = false;
        //        _deductTile = true;
        //        nextMoveLocation.GetComponentInParent<TileDisplay>().occupationValue -= 1;
        //    }
        //}

        
            
        private void OnTriggerEnter(Collider other)
        {
            if (_enemyMovement._canMove)
            {
                return;
            }
            
            if (other.CompareTag(s_Ontile))
            {
                print("hit");
                if (transform.position.z > 7f)
                {
                    //noNextMove = true;
                    return;
                }

                nextMoveLocation = other.gameObject;
                //if (other.gameObject.GetComponent<OnTile>().tileHasToken == 0)
                //{
                //nextMoveLocation.GetComponent<OnTile>().NextMoveGameObjects.Add(gameObject);

                //if (_enemyDirection.reassignNextObj)
                //{
                //    _enemyDirection.reassignNextObj = false;
                //    return;
                //}
                //nextMoveLocation.GetComponentInParent<TileDisplay>().isOccupied += 1;
                if (!nextMoveLocation.GetComponentInParent<TileDisplay>().isOccupied)
                {
                    nextMoveLocation.GetComponentInParent<TileDisplay>().isOccupied = true;
                    hasPermission = true;
                }
                else
                {
                    hasPermission = false;
                }
                

                //}
            }

            //if (other.CompareTag(s_Enemy)) // CHECK assess this code
            //{
            //    enemyOnTile = other.gameObject;
            //}
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(s_Ontile))
            {
                //if (noNextMove)
                //{
                //    noNextMove = false;
                //    return;
                //}

                //if (other.GetComponentInParent<TileDisplay>().occupationValue == 0)
                //{
                //    return;
                //}
                
                /// other.GetComponentInParent<TileDisplay>().occupationValue -= 1;
                
            }
        }
    }
}

/*public class EnemyNextMove : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemyMovement _enemyMovement;
    
    private const string s_Ontile = "OnTile";
    private const string s_Enemy = "enemy";

    //public bool nextMoveHasPermission;
    public bool hasPermission;
    private bool isAssigned;

    public GameObject enemyOnTile;
    [SerializeField] private bool noNextMove;

    private void OnDisable()
    {
        ResetValues();
    }

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
                    noNextMove = true;
                    return;
                }
                _enemyMovement.NextMoveLocationGO = other.gameObject;
                //if (other.gameObject.GetComponent<OnTile>().tileHasToken == 0)
                //{
                    other.gameObject.GetComponent<OnTile>().NextMoveGameObjects.Add(gameObject); 
                    other.gameObject.GetComponent<OnTile>().tileHasToken += 1;
                //}
            }

            if (other.CompareTag(s_Enemy)) // CHECK assess this code
            {
                enemyOnTile = other.gameObject;
            }
                
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(s_Ontile))
        {
            if (noNextMove)
            {
                noNextMove = false;
                return;
            }
            other.gameObject.GetComponent<OnTile>().tileHasToken -= 1;
            other.gameObject.GetComponent<OnTile>().NextMoveGameObjects.Remove(gameObject); 
        }
    }

    public void ResetValues()
    {
        hasPermission = false;
        isAssigned = false;
        noNextMove = false;
    }
}*/
