using System;
using System.Data.SqlTypes;
using Floor;
using Managers;
using UnityEngine;

namespace EnemyNS
{
    [RequireComponent(typeof(Enemy))]
// Added to each enemy in game.
    public class EnemyMovement : MonoBehaviour
    {
        #region Private & Constant variables

        [SerializeField] private GameObject _designatedTileGameObject;
        [SerializeField] private EnemyDirection _enemyDirection;
        [SerializeField] private EnemyNextMove _enemyNextMove;
        [SerializeField] private float _speed = 2.0f;
        private float _step;
        private bool _isFreeToMoveCheck;
        private bool _hasStartedMovement;
        private bool _assignNextTile;

        #endregion

        #region Public & Protected variables

        public GameObject NextMoveLocationGO { get; set; }
        public bool IsEnemyMoving { get; private set; }

        #endregion

        #region Constructors

        #endregion

        #region Private methods

        private void OnDisable()
        {
            NewCycle();
        }

        private void Update()
        {
            if (BeatManager.Instance.BeatIndex == 4 ||
                (BeatManager.Instance.BeatIndex == 0 && _enemyDirection.hasFacedDirection) && !_hasStartedMovement)
            {
                _hasStartedMovement = true;
            }

            if (_hasStartedMovement)
            {
                AssignNextTile();
                Movement();
            }

            if (BeatManager.Instance.BeatIndex == 5 || (BeatManager.Instance.BeatIndex == 1))
            {
                NewCycle();
            }
        }

        private void Movement()
        {
            if (!_enemyNextMove.nextMoveHasToken)
            {
                IsEnemyMoving = true;
                _step = _speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position,
                    _designatedTileGameObject.transform.position, _step);

                if (Vector3.Distance(transform.position, _designatedTileGameObject.transform.position) < 0.01f)
                {
                    transform.position = _designatedTileGameObject.transform.position;
                    IsEnemyMoving = false;
                    _enemyNextMove.NewCycle();
                }
            }
        }

        private void AssignNextTile()
        {
            if (!_assignNextTile)
            {
                _assignNextTile = true;
                _designatedTileGameObject = NextMoveLocationGO;
            }
        }

        private void NewCycle()
        {
            _hasStartedMovement = false;
            _assignNextTile = false;
        }

        #endregion

        #region Public methods

        #endregion
    }
}