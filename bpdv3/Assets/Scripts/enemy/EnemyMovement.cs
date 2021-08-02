using System;
using Floor;
using Managers;
using UnityEngine;

namespace EnemyNS
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] EnemyDestination _enemyNextMove;

        private float _step;
        [SerializeField] private float _speed = 2.0f;
        private Vector3 location;

        private bool _isFirstTime = true;
        public bool _canMove;

        //private void OnDisable()
        //{
        //    _isFirstTime = true;
        //    _canMove = false;
        //}

        private void Update()
        {
            if (BeatManager.Instance.BeatIndex == 5)
            {
                print("5)");
                _isFirstTime = false;
            }
//
//
        //    if (_canMove)
        //    {
        //        Movement();
        //    }
//
//
        //    //if ((BeatManager.Instance.BeatIndex == 4 || BeatManager.Instance.BeatIndex == 0) && !isMoving)
        //    //{
        //    //    isMoving = true;
        //    //    if (_enemyNextMove.hasPermission)
        //    //    {
        //    //        hasBegunMove = true;
        //    //    }
        //    //}
        }

        public void Movement()
        {
            //if (_enemy.isNew)
            {
                if (_isFirstTime)
                {
                    return;
                }
                //}

                if (_enemyNextMove.hasPermission)
                {
                    //isEnemyMoving = true;

                    _step = _speed * Time.deltaTime;

                    //var position = _designatedTileGameObject.transform.position;
                    var position = _enemyNextMove.nextMoveLocation.transform.position;
                    location = new Vector3(position.x, position.y, position.z);

                    transform.position = Vector3.MoveTowards(transform.position, location, _step);

                    if (Vector3.Distance(transform.position, location) < 0.01f)
                    {
                        transform.position = location;
                        _enemyNextMove.hasPermission = false;
                        _canMove = false;
                        _enemy.isNew = false;
                        //_enemyNextMove.nextMoveLocation.GetComponentInParent<TileDisplay>().isOccupied -= 1;
                        //_enemyNextMove.nextMoveLocation.GetComponentInParent<TileDisplay>().isOccupied = false;
                    }
                }
                //else if (!deniedMove)
                //{
                else
                {
                    _canMove = false;
                    //_enemyNextMove.nextMoveLocation.GetComponentInParent<TileDisplay>().isOccupied -= 1;
                    //_enemyNextMove.nextMoveLocation.GetComponentInParent<TileDisplay>().isOccupied = false;
                }
            }
        }
    }
}

/*public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemyNextMove _enemyNextMove;
    [SerializeField] private GameObject _designatedTileGameObject;
    public GameObject NextMoveLocationGO; 
    
    private float _step;
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private Vector3 location;

    public bool IsEnemyMoving;
    
    public bool hasBegunMove;

    private void OnDisable()
    {
        ResetValues();
    }

    private void Update()
    {
        if (hasBegunMove)
        {
            Movement();
        }
    }

    public void Movement()
    {
        //VacateEnemyStartTile();
        IsEnemyMoving = true;
        _step = _speed * Time.deltaTime;

        var position = _designatedTileGameObject.transform.position;
        location = new Vector3(position.x, position.y, position.z);

        transform.position = Vector3.MoveTowards(transform.position, location, _step);

        if (Vector3.Distance(transform.position, location) < 0.01f)
        {
            transform.position = location;
            IsEnemyMoving = false;
        }
    }
    
    public void AssignNextTile()
    {
        _designatedTileGameObject = NextMoveLocationGO;
    }

    public void ResetValues()
    {
        hasBegunMove = false;
        IsEnemyMoving = false;
    }
}*/
//}