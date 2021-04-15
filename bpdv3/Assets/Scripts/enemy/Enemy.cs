using System.Text.RegularExpressions;
using Floor;
using Managers;
using PlayerNS;
using UnityEngine;

namespace EnemyNS
{
    public class Enemy : MonoBehaviour
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
        private bool repeatFirstMove;
        [SerializeField] private Collider _nextMoveCollider;
        private Collider _collider;
        private bool hasResetValues;
        private bool canMove;

        private bool renew;
        
        //private void OnDisable()
        //{
        //    if (hasSpawned)
        //    {
        //        //NewCycle();
        //        OnDeath();
        //    }
        //}
        
        private void OnEnable()
        {
            gameObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
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
                        hasResetValues = false;
                        hasFacedDirection = true;
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
                else
                {
                    if (BeatManager.Instance.BeatIndex == 1 || BeatManager.Instance.BeatIndex == 5 && !hasResetValues)
                    {
                        hasResetValues = true;
                        repeatFirstMove = true;
                        ResetValue();
                    }
                    
                    if (BeatManager.Instance.BeatIndex == 4 && !repeatFirstMove)
                    {
                        return;
                    }

                    if ((BeatManager.Instance.BeatIndex == 4 || BeatManager.Instance.BeatIndex == 0) && !isMoving)
                    {
                        isMoving = true;
                        if (_enemyNextMove.hasPermission)
                        {
                            _enemyMovement.hasBegunMove = true;
                            isNew = false;
                        }
                    }

                    if (BeatManager.Instance.BeatIndex == 2 || BeatManager.Instance.BeatIndex == 6 && !hasFacedDirection)
                    {
                        hasResetValues = false;
                        hasFacedDirection = true;
                    }
                }
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
                other.gameObject.GetComponent<OnTile>().tileHasToken += 1;
                other.gameObject.GetComponent<OnTile>().GOonTile.Add(gameObject);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(s_Ontile))
            {
                other.gameObject.GetComponent<OnTile>().tileHasToken -= 1;
                other.gameObject.GetComponent<OnTile>().GOonTile.Remove(gameObject);
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
        
        //public void OnDeath()
        //{
        //    enemyCurrentTile.GetComponent<OnTile>().tileHasToken = false;
        //    //token = true;
        //}
        
        public Transform PushBackTransform()
        {
            return pushBackTransform;
        }
        
        void ResetValue()
        {
            hasFacedDirection = false;
            isMoving = false;
        }
    }
}



/*using Floor;
using Managers;
using UnityEngine;

namespace EnemyNS
{
    public class Enemy : MonoBehaviour
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
        private bool repeatFirstMove;
        [SerializeField] private Collider _nextMoveCollider;
        private Collider _collider;
        private bool hasResetValues;
        private bool canMove;

        private bool renew;
        
        private void Start()
        {
            _collider = GetComponent<Collider>();
            _nextMoveCollider = _enemyNextMove.gameObject.GetComponent<Collider>();
        }

        private void OnEnable()
        {
            gameObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            isAlive = true;
        }
        
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
                        hasResetValues = false;
                        hasFacedDirection = true;
                        //_collider.enabled = false;
                        //_collider.enabled = true;
                        //_nextMoveCollider.enabled = false;
                        //_nextMoveCollider.enabled = true;
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
                else
                {
                    if (BeatManager.Instance.BeatIndex == 4 && !repeatFirstMove)
                    {
                        return;
                    }

                    if ((BeatManager.Instance.BeatIndex == 0) && !isMoving)
                    {
                        repeatFirstMove = true;
                        if (!_enemyNextMove.hasPermission)
                        {
                            ResetValue();
                            return;
                        }
                        isMoving = true;
                        isNew = false;
                        _enemyMovement.hasBegunMove = true;
                    }

                    if (BeatManager.Instance.BeatIndex == 4 && repeatFirstMove)
                    {
                        if (!_enemyNextMove.hasPermission)
                        {
                            ResetValue();
                            return;
                        }
                        isMoving = true;
                        isNew = false;
                        _enemyMovement.hasBegunMove = true;
                    }
                    
                    if (BeatManager.Instance.BeatIndex == 2 || BeatManager.Instance.BeatIndex == 6 && !hasFacedDirection)
                    {
                        hasFacedDirection = true;
                        //_nextMoveCollider.enabled = false;
                        //_nextMoveCollider.enabled = true;
                    }
                }
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
                other.gameObject.GetComponent<OnTile>().tileHasToken += 1;
                other.gameObject.GetComponent<OnTile>().GOonTile.Add(gameObject);

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(s_Ontile))
            {
                other.gameObject.GetComponent<OnTile>().tileHasToken -= 1;
                other.gameObject.GetComponent<OnTile>().GOonTile.Remove(gameObject);
                print("exiting");
            }
        }
        
        public Transform PushBackTransform()
        {
            return pushBackTransform;
        }

        void ResetValue()
        {
            hasFacedDirection = false;
            isMoving = false;
            hasAssignedTile = false;
        }
    }
}*/