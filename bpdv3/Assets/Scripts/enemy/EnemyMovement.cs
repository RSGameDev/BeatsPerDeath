using Floor;
using Managers;
using Mechanics;
using UnityEngine;

namespace Scripts.Enemy
{
    [RequireComponent(typeof(Enemy))]
// Added to each enemy in game.
    public class EnemyMovement : MonoBehaviour
    {
        #region Private & Constant Variables
        
        [SerializeField] private GameObject _nextMoveValidityGO;
        [SerializeField] private Enemy _enemy;
        [SerializeField] private EnemyNextMove _enemyNextMove;
        [SerializeField] private int _randomDirectionForShroomToFace;
        //[SerializeField] private int _faceDirectionRandRook;
        [SerializeField] private bool _hasFacedDirection;
        [SerializeField] private float _speed = 2.0f;
        private float _step;
        private bool _hasEnemyStartedMoving;
        
        private bool _firstMoveCompleted;
        private bool _hasForthBeatOccured;
        
        #endregion

        #region Public & Protected Variables

        public GameObject NextMoveLocationGO { private get; set; }
        public bool IsEnemyMoving { get; private set; }
        
        
        public bool FirstMoveBool;

        #endregion

        #region Constructors
        #endregion

        #region Private Methods

        private void Update()
        {
            if (_enemyNextMove.IsDestinationObtained && !_firstMoveCompleted)
            {
                Move();
            }

            if (_hasForthBeatOccured && _hasEnemyStartedMoving)
            {
                if (_enemy.CurrentEnemyType  == Enemy.EnemyType.Rook)
                {
                    if (_enemyNextMove.AllowedToMove())
                    {
                        _hasEnemyStartedMoving = false;
                        _nextMoveValidityGO.transform.position += Vector3.forward * 5;
                        _nextMoveValidityGO.GetComponent<Collider>().enabled = true;
                        return;
                    }                
                }
                Move();
            }
            else if (_hasForthBeatOccured && !_hasEnemyStartedMoving)
            {           
                ResetValues();
            }        
        }
        
        private void FaceDirection()
        {
            _randomDirectionForShroomToFace = UnityEngine.Random.Range(0, 4);
            //_faceDirectionRandRook = UnityEngine.Random.Range(0, 2);

            var enemyType = _enemy.CurrentEnemyType;

            switch (enemyType)
            {
                case Enemy.EnemyType.Shroom:
                    switch (_randomDirectionForShroomToFace)
                    {
                        case 0:
                            _enemyNextMove.IsDestinationObtained = false;
                            transform.LookAt(transform.position + Vector3.forward);
                            _nextMoveValidityGO.GetComponent<Collider>().enabled = true;
                            break;
                        case 1:
                            _enemyNextMove.IsDestinationObtained = false;
                            transform.LookAt(transform.position + Vector3.back);
                            _nextMoveValidityGO.GetComponent<Collider>().enabled = true;
                            break;
                        case 2:
                            _enemyNextMove.IsDestinationObtained = false;
                            transform.LookAt(transform.position + Vector3.left);
                            _nextMoveValidityGO.GetComponent<Collider>().enabled = true;
                            break;
                        case 3:
                            _enemyNextMove.IsDestinationObtained = false;
                            transform.LookAt(transform.position + Vector3.right);
                            _nextMoveValidityGO.GetComponent<Collider>().enabled = true;
                            break;
                    }
                    break;
                //case global::Enemy.Enemy.EnemyType.Rook:
                //    switch (_faceDirectionRandRook)
                //    {
                //        case 0:
                //            _enemyNextMove.IsDestinationObtained = false;
                //            transform.LookAt(transform.position + Vector3.left);
                //            NextMoveGO.GetComponent<Collider>().enabled = true;
                //            break;
                //        case 1:
                //            _enemyNextMove.IsDestinationObtained = false;
                //            transform.LookAt(transform.position + Vector3.right);
                //            NextMoveGO.GetComponent<Collider>().enabled = true;
                //            break;
                //    }
                //    break;
            }
        }
        
        private void Move()
        {
            if (_enemyNextMove.AllowedToMove())
            {
                print("move");
                IsEnemyMoving = true;
                _step = _speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, NextMoveLocationGO.transform.position, _step);

                if (Vector3.Distance(transform.position, NextMoveLocationGO.transform.position) < 0.01f)
                {
                    transform.position = NextMoveLocationGO.transform.position;
                    ResetValues();
                }
            }
            else
            {
                ResetValues();
            }
        }    
        
        #endregion

        #region Public Methods

        // The enemy waits for a moment then starts to move.
        public void FirstMove()
        {
            print("firstmove");
            if (BeatManager.Instance.BeatIndex == 2)
            {
                FirstMoveBool = true;
            }

            if (BeatManager.Instance.BeatIndex == 4 && FirstMoveBool)
            {
                transform.LookAt(transform.position + Vector3.back);
                _nextMoveValidityGO.GetComponent<Collider>().enabled = true;            
            }    
        }

        public void Direction()
        {
            if (BeatManager.Instance.BeatIndex == 2 && !_hasFacedDirection)
            {
                _hasFacedDirection = true;
                FaceDirection();
            }
        }

        public void Movement()
        {
            print("movement");
        
            if (BeatManager.Instance.BeatIndex == 4 && _enemyNextMove.IsDestinationObtained)
            {
                _hasEnemyStartedMoving = true;
                _hasForthBeatOccured = true;
            }
            else if (BeatManager.Instance.BeatIndex == 4 && !_enemyNextMove.IsDestinationObtained)
            {
                _hasForthBeatOccured = true;
            }       
        }
        
        public void ResetValues()
        {
            print("resetvalues");
            NextMoveLocationGO.GetComponentInParent<TileProperties>().OccupiedDecreased();
            _enemy.IsNewEnemy = false;
            _firstMoveCompleted = true;
            IsEnemyMoving = false;
            _hasFacedDirection = false;        
            _hasEnemyStartedMoving = false;
            _hasForthBeatOccured = false;
            //_enemyNextMove.IsOutOfBounds = false;
        }
        #endregion
    }
}
