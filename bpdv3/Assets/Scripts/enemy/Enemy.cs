using System;
using Floor;
using Managers;
using PlayerNS;
using UI.Main;
using UnityEngine;

namespace EnemyNS
{
    [RequireComponent(typeof(EnemyMovement))]
    public class Enemy : MonoBehaviour
    {
        #region Private & Constant variables

        [SerializeField] public GameObject enemyCurrentTile;
        [SerializeField] private Transform pushBackTransform = null;

        private const string s_Ontile = "OnTile";

        EnemyMovement enemyMovement = null;

        #endregion

        #region Public & Protected variables

        public enum EnemyType
        {
            Shroom,
            Rook
        }

        public EnemyType CurrentEnemyType;
        //public bool token = true;
        public bool hasSpawned = false;
        public bool isAlive;
        
        #endregion
        
        #region Constructors

        #endregion

        #region Private Methods

        private void OnDisable()
        {
            if (hasSpawned)
            {
                //NewCycle();
                OnDeath();
            }
        }

        private void OnEnable()
        {
            gameObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            isAlive = true;
        }

        private void Awake()
        {
            enemyMovement = GetComponent<EnemyMovement>();
        }
        private void FixedUpdate()
        {
            IsPlayerInFront();
        }

        private void Update()
        {
            if (!isAlive)
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                if (IsPlayerInFront() || enemyMovement.IsEnemyMoving)
                {
                    HitPlayer(other);
                }
                else
                {
                    Scoring.ScorePoints();
                    if (BeatBar.thresholdZone)
                    {
                        ComboMetre._increment = true;
                    }
                    gameObject.SetActive(false);
                }
            }

            if (other.CompareTag(s_Ontile))
            {
                enemyCurrentTile = other.gameObject;
                other.gameObject.GetComponent<OnTile>().tileHasToken = true;
                
            }
        }

        private void HitPlayer(Collider other)
        {
            var playerMovement = other.GetComponent<PlayerMovement>();
            playerMovement.enemy = this;
            playerMovement.isPushBack = true;
            playerMovement.IsPlayerInputDetected = false;
            other.gameObject.transform.position = pushBackTransform.position;
            other.gameObject.GetComponent<Player>().DealDamage();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag != "Player") return;
            
            gameObject.SetActive(false);
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(s_Ontile))
            {
                other.gameObject.GetComponent<OnTile>().tileHasToken = false;
                //token = true;
            }
        }
        
        private bool IsPlayerInFront()
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 200))
            {
                Debug.DrawRay(transform.position, transform.forward * 50, Color.red);
                if (hit.transform.gameObject.tag == "Player")
                    return true;
            }
            return false;
        }

        public void OnDeath()
        {
            enemyCurrentTile.GetComponent<OnTile>().tileHasToken = false;
            //token = true;
        }

        #endregion

        #region Public methods

        public Transform PushBackTransform()
        {
            return pushBackTransform;
        }

        #endregion
    }
}