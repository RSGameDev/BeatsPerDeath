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

        private GameObject enemyCurrentTile;
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
        public bool token = true;
        public bool hasSpawned = false;
        
        #endregion

        #region Constructors

        #endregion

        #region Private Methods

        private void OnDisable()
        {
            if (hasSpawned)
            {
                NewCycle();
            }
        }

        private void Awake()
        {
            enemyMovement = GetComponent<EnemyMovement>();
        }
        private void FixedUpdate()
        {
            IsPlayerInFront();
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
                if (!other.gameObject.GetComponent<OnTile>().possessToken)
                {
                    other.gameObject.GetComponent<OnTile>().possessToken = true;
                    token = false;
                }
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
                other.gameObject.GetComponent<OnTile>().possessToken = false;
                token = true;
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

        private void NewCycle()
        {
            enemyCurrentTile.GetComponent<OnTile>().possessToken = false;
            token = true;
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