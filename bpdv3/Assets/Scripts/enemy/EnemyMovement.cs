using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
// Added to each enemy in game.
public class EnemyMovement : MonoBehaviour
{
    #region Private variables
    private Enemy _enemy;
    private EnemyNextMove _nextMove;
    private TileController _tileController;

    private const float s_verticalTopLimit = 6.25F;
    private const float s_verticalBottomLimit = 1.25F;
    private const float s_horizontalLeftLimit = 0F;
    private const float s_horizontalRightLimit = 5F;

    private const float s_moveUnits = 1.25F;
    private const int s_firstBeat = 1;
    private const int s_secondBeat = 2;
    private const int s_fourthBeat = 4;
    private int _firstMoveTick;
    private int _faceDirectionRandShroom;
    private int _faceDirectionRandRook;
    private bool _firstMoveFlag;
    private bool _hasFacedDirection;
    private bool _hasMoved;
    #endregion

    #region Public variables
    public GameObject NextMoveGO;

    public Anchor Anchor;
    #endregion

    #region Lerp Behaviour
    private Vector3 _destination;
    private float _startTime;
    private float _journeyLength;
    public const float s_Speed = 1.0F;
    public bool IsLerpingStarted;
    #endregion

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _nextMove = transform.GetChild(2).GetComponent<EnemyNextMove>();

        GameObject temp = GameObject.FindGameObjectWithTag("TileController");
        _tileController = temp.GetComponent<TileController>();
    }

    void Update()
    {
        if (IsLerpingStarted)
        {
            SmoothMovement();
        }

        CheckBoundaries();
    }

    private void SmoothMovement()
    {
        float distCovered = (Time.time - _startTime) * s_Speed;
        float fractionOfJourney = distCovered / _journeyLength;
        transform.position = Vector3.Lerp(transform.position, _destination, fractionOfJourney);
        if (fractionOfJourney >= 0.1f || !_enemy.IsEnemyAlive)
        {
            _enemy.IsNewEnemy = false;
            IsLerpingStarted = false;
            Anchor.PlaceInPosition();
        }
    }

    // Enemy spawns on 1st row, then moves down 1 space, always the same direction. The enemy moves normally after the first move.
    public void FirstMove()
    {
        if (SceneController.Instance.gameBeatCount == s_firstBeat && !_firstMoveFlag)
        {
            _firstMoveFlag = true;
            _firstMoveTick++;
        }

        if (SceneController.Instance.gameBeatCount == s_fourthBeat && _firstMoveTick == 1)
        {
            _startTime = Time.time;
            _destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - s_moveUnits);
            _journeyLength = Vector3.Distance(transform.position, _destination);
            IsLerpingStarted = true;
        }
    }

    public void MovementReset()
    {
        if (SceneController.Instance.gameBeatCount == s_firstBeat)
        {
            ResetFlags();
        }
    }

    public void Direction()
    {
        if (SceneController.Instance.gameBeatCount == s_secondBeat && !_hasFacedDirection)
        {
            FaceDirection();
            _hasFacedDirection = true;
        }
    }

    public void Movement()
    {
        var enemyType = _enemy.CurrentEnemyType;

        switch (enemyType)
        {
            case Enemy.EnemyType.Shroom:
                if (SceneController.Instance.gameBeatCount == s_fourthBeat && !_hasMoved)
                {
                    Move();
                    _hasMoved = true;
                }
                break;
            case Enemy.EnemyType.Rook:
                if (SceneController.Instance.gameBeatCount == s_fourthBeat && !_hasMoved)
                {
                    Move();
                }
                break;
        }
    }

    public void ResetFlags()
    {
        _hasFacedDirection = false;
        _hasMoved = false;
    }

    void FaceDirection()
    {
        _faceDirectionRandShroom = UnityEngine.Random.Range(0, 4);
        _faceDirectionRandRook = UnityEngine.Random.Range(0, 2);

        var enemyType = _enemy.CurrentEnemyType;

        switch (enemyType)
        {
            case Enemy.EnemyType.Shroom:
                switch (_faceDirectionRandShroom)
                {
                    case 0:
                        transform.LookAt(transform.position + Vector3.forward);
                        break;
                    case 1:
                        transform.LookAt(transform.position + Vector3.back);
                        break;
                    case 2:
                        transform.LookAt(transform.position + Vector3.left);
                        break;
                    case 3:
                        transform.LookAt(transform.position + Vector3.right);
                        break;
                }
                break;
            case Enemy.EnemyType.Rook:
                switch (_faceDirectionRandRook)
                {
                    case 0:
                        transform.LookAt(transform.position + Vector3.left);
                        break;
                    case 1:
                        transform.LookAt(transform.position + Vector3.right);
                        break;
                }
                break;
        }
    }

    void Move()
    {
        var enemyType = _enemy.CurrentEnemyType;

        switch (enemyType)
        {
            case Enemy.EnemyType.Shroom:
                switch (_faceDirectionRandShroom)
                {
                    case 0:
                        _startTime = Time.time;
                        _destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + s_moveUnits);
                        _journeyLength = Vector3.Distance(transform.position, _destination);
                        IsLerpingStarted = true;
                        break;
                    case 1:
                        _startTime = Time.time;
                        _destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - s_moveUnits);
                        _journeyLength = Vector3.Distance(transform.position, _destination);
                        IsLerpingStarted = true;
                        break;
                    case 2:
                        _startTime = Time.time;
                        _destination = new Vector3(transform.position.x - s_moveUnits, transform.position.y, transform.position.z);
                        _journeyLength = Vector3.Distance(transform.position, _destination);
                        IsLerpingStarted = true;
                        break;
                    case 3:
                        _startTime = Time.time;
                        _destination = new Vector3(transform.position.x + s_moveUnits, transform.position.y, transform.position.z);
                        _journeyLength = Vector3.Distance(transform.position, _destination);
                        IsLerpingStarted = true;
                        break;
                }
                break;
            case Enemy.EnemyType.Rook:
                switch (_faceDirectionRandRook)
                {
                    case 0:
                        if (_nextMove.NextMoveTrigger)                     // A reference to detect if the rook enemy type can carry on moving.
                        {
                            _startTime = Time.time;
                            _destination = new Vector3(transform.position.x - s_moveUnits, transform.position.y, transform.position.z);
                            _journeyLength = Vector3.Distance(transform.position, _destination);
                            IsLerpingStarted = true;
                            _nextMove.NextMoveTrigger = false;
                        }

                        if (!_nextMove.CanMove())
                        {
                            transform.position = new Vector3(_nextMove.EnemyNextMoveTileObject.transform.position.x + s_moveUnits, _nextMove.EnemyNextMoveTileObject.transform.position.y + 1, _nextMove.EnemyNextMoveTileObject.transform.position.z);
                            Anchor.PlaceInPosition();
                            _hasMoved = true;
                        }
                        break;
                    case 1:
                        if (_nextMove.NextMoveTrigger)                     // A reference to detect if the rook enemy type can carry on moving.
                        {
                            _startTime = Time.time;
                            _destination = new Vector3(transform.position.x + s_moveUnits, transform.position.y, transform.position.z);
                            _journeyLength = Vector3.Distance(transform.position, _destination);
                            IsLerpingStarted = true;
                            _nextMove.NextMoveTrigger = false;
                        }

                        if (!_nextMove.CanMove())
                        {
                            transform.position = new Vector3(_nextMove.EnemyNextMoveTileObject.transform.position.x - s_moveUnits, _nextMove.EnemyNextMoveTileObject.transform.position.y + 1, _nextMove.EnemyNextMoveTileObject.transform.position.z);
                            Anchor.PlaceInPosition();
                            _hasMoved = true;
                        }
                        break;
                }
                break;
        }
    }         

    void CheckBoundaries()
    {
        if (!_enemy.IsNewEnemy)
        {
            if (transform.position.z > s_verticalTopLimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, s_verticalTopLimit);
                IsLerpingStarted = false;
            }
        }        

        if (!_tileController.IsPlatformMoving)
        {
            if (transform.position.z < s_verticalBottomLimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, s_verticalBottomLimit);
                IsLerpingStarted = false;
            }
        }

        if (transform.position.x < s_horizontalLeftLimit)
        {
            transform.position = new Vector3(s_horizontalLeftLimit, transform.position.y, transform.position.z);
            IsLerpingStarted = false;
        }

        if (transform.position.x > s_horizontalRightLimit)
        {
            transform.position = new Vector3(s_horizontalRightLimit, transform.position.y, transform.position.z);
            IsLerpingStarted = false;
        }
    }
}
