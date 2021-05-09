using System;
using System.Text.RegularExpressions;
using Floor;
using Managers;
using PlayerNS;
using UnityEngine;

namespace EnemyNS
{
    public class Enemy : MonoBehaviour
    {
        private const string s_Ontile = "OnTile";
        [SerializeField] public GameObject enemyCurrentTile;
        [SerializeField] private EnemyMovement _enemyMovement;
        
        public bool isNew = true;
        
        public enum EnemyType
        {
            Shroom,
            Rook
        }
        public EnemyType currentEnemyType;
        private bool hasReset;

        private void OnDisable()
        {
            enemyCurrentTile = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            //if (other.tag == "Player")
            //{
            //    if (IsPlayerInFront() || enemyMovement.IsEnemyMoving)
            //    {
            //        HitPlayer(other);
            //    }
            //    else
            //    {
            //        Scoring.ScorePoints();
            //        if (BeatBar.thresholdZone)
            //        {
            //            ComboMetre._increment = true;
            //        }
            //        gameObject.SetActive(false);
            //    }
            //}
            
            if (other.CompareTag(s_Ontile))
            {
                enemyCurrentTile = other.gameObject;
                //enemyCurrentTile.GetComponent<OnTile>().GOonTile.Add(gameObject);
                //enemyCurrentTile.GetComponentInParent<TileDisplay>().isOccupied += 1;
                
                enemyCurrentTile.GetComponentInParent<TileDisplay>().isOccupied = true;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(s_Ontile))
            {
                //enemyCurrentTile.GetComponent<OnTile>().GOonTile.Remove(gameObject);
                //other.GetComponentInParent<TileDisplay>().isOccupied -= 1;
                other.GetComponentInParent<TileDisplay>().isOccupied = false;
            }
        }
    }
}

/*public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyDirection _enemyDirection;
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private EnemyNextMove _enemyNextMove;
        [SerializeField] public GameObject enemyCurrentTile;
        
        [SerializeField] private Transform pushBackTransform = null;

        private const string s_Ontile = "OnTile";

        public bool isAlive;
        private bool hasAssignedTile;

        public enum EnemyType
        {
            Shroom,
            Rook
        }

        public bool isNew = true; 
        
        public EnemyType CurrentEnemyType;

        private bool hasFacedDirection;

        private bool isMoving;
        [SerializeField] private Collider _nextMoveCollider;
        private Collider _collider;
        private bool hasResetValues;
        private bool canMove;

        private bool renew;

        public bool hasSpawned;

        public Vector3 startPos;

        private void OnDisable()
        {
            if (hasSpawned)
            {
                hasResetValues = false;
                hasFacedDirection = false;
                isMoving = false;
                //enemyCurrentTile = null;
                
                hasSpawned = false;
                isNew = true;
                isAlive = false;
                ResetValue();
            }
        }

        private void OnEnable()
        {
            //gameObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            transform.LookAt(transform.position + Vector3.back);
            isAlive = true;
        }
        
        //private void FixedUpdate()
        //{
        //    IsPlayerInFront();
        //}
        
        private void Update()
        {
            if (isAlive)
            {
                if (!isNew)
                {
                    // RESET VALUES
                    if (BeatManager.Instance.BeatIndex == 1 || BeatManager.Instance.BeatIndex == 5 && !hasResetValues)
                    {
                        hasResetValues = true;
                        ResetValue();
                        _enemyMovement.ResetValues();
                        _enemyNextMove.ResetValues();
                    }
                    
                    // FACE DIRECTION
                    if ((BeatManager.Instance.BeatIndex == 2 || BeatManager.Instance.BeatIndex == 6) && !hasFacedDirection)
                    {
                        hasFacedDirection = true;
                        hasResetValues = false;
                        _enemyDirection.FaceDirection();
                    }

                    // MOVE
                    if ((BeatManager.Instance.BeatIndex == 4 || BeatManager.Instance.BeatIndex == 0) && !isMoving)
                    {
                        isMoving = true;
                        if (_enemyNextMove.hasPermission)
                        {
                            _enemyMovement.hasBegunMove = true;
                        }
                    }
                }
            }
            else
            {
                OnDeath();
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            //if (other.tag == "Player")
            //{
            //    if (IsPlayerInFront() || enemyMovement.IsEnemyMoving)
            //    {
            //        HitPlayer(other);
            //    }
            //    else
            //    {
            //        Scoring.ScorePoints();
            //        if (BeatBar.thresholdZone)
            //        {
            //            ComboMetre._increment = true;
            //        }
            //        gameObject.SetActive(false);
            //    }
            //}
            
            if (other.CompareTag(s_Ontile))
            {
                enemyCurrentTile = other.gameObject;
                enemyCurrentTile.GetComponent<OnTile>().tileHasToken += 1;
                enemyCurrentTile.GetComponent<OnTile>().GOonTile.Add(gameObject);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(s_Ontile))
            {
                enemyCurrentTile.GetComponent<OnTile>().tileHasToken -= 1;
                enemyCurrentTile.GetComponent<OnTile>().GOonTile.Remove(gameObject);
            }
        }
        
        //private void HitPlayer(Collider other)
        //{
        //    var playerMovement = other.GetComponent<PlayerMovement>();
        //    playerMovement.enemy = this;
        //    playerMovement.isPushBack = true;
        //    playerMovement.IsPlayerInputDetected = false;
        //    other.gameObject.transform.position = pushBackTransform.position;
        //    other.gameObject.GetComponent<Player>().DealDamage();
        //}
//
        //private void OnCollisionEnter(Collision collision)
        //{
        //    if (collision.gameObject.tag != "Player") return;
        //    
        //    gameObject.SetActive(false);
        //}
        
        //private bool IsPlayerInFront()
        //{
        //    RaycastHit hit;
//
        //    if (Physics.Raycast(transform.position, transform.forward, out hit, 200))
        //    {
        //        Debug.DrawRay(transform.position, transform.forward * 50, Color.red);
        //        if (hit.transform.gameObject.tag == "Player")
        //            return true;
        //    }
        //    return false;
        //}
        
        public void OnDeath()
        {
            //if (enemyCurrentTile == null)
            //{
            //    gameObject.SetActive(false);
            //    return;
            //}
            //enemyCurrentTile.GetComponent<OnTile>().tileHasToken -= 1;
            //enemyCurrentTile.GetComponent<OnTile>().GOonTile.Remove(gameObject);
            gameObject.SetActive(false);
        }
        
        public Transform PushBackTransform()
        {
            return pushBackTransform;
        }
        
        void ResetValue()
        {
            hasFacedDirection = false;
            isMoving = false;
        }
    }*/


