using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
// Added to each enemy in game.
public class EnemyMovement : MonoBehaviour
{
    #region Private variables
    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemyNextMove _enemyNextMove;

    private const float s_verticalTopLimit = 6.25F;
    private const float s_verticalBottomLimit = 1.25F;

    private const int s_firstBeat = 1;
    private const int s_secondBeat = 2;
    private const int s_fourthBeat = 4;
    
    public int _faceDirectionRandRook;
    
    #endregion

    #region Public variables
    public GameObject NextMoveGO;

    public Anchor Anchor;
    #endregion

    public GameObject NextMoveLocationGO;

    //public string Direction;
    [Header("new")]
    public float Speed = 2.0f;
    public float Step;
    //public bool PushBack;
    public bool IsOnBeat;
    
    [SerializeField] private bool _hasFacedDirection;
    [SerializeField] private int _faceDirectionRandShroom;

    public bool startMoveEnemy;
    public bool IsEnemyMoving;

    public bool firstMoveMet;

    public bool firstMoveBool;
    public int firstMoveBeatCount;

    public bool fourthBeatTriggered;

    [SerializeField] Vector3 lastPosition;

    public bool hasDirection;

    void Update()
    {
        if (_enemyNextMove.IsDestinationObtained && !firstMoveMet)
        {
            Move();
        }

        if (fourthBeatTriggered && startMoveEnemy)
        {
            if (_enemy.CurrentEnemyType  == Enemy.EnemyType.Rook)
            {
                if (_enemyNextMove.CanMove())
                {
                    startMoveEnemy = false;
                    NextMoveGO.transform.position += Vector3.forward * 5;
                    NextMoveGO.GetComponent<Collider>().enabled = true;
                    return;
                }                
            }
            Move();
        }
        else if (fourthBeatTriggered && !startMoveEnemy)
        {           
            ResetValues();
        }        
    }

    // The enemy waits for a moment then starts to move.
    public void FirstMove()
    {
        print("firstmove");
        if (BeatManager.Instance.BeatIndex == 0)
        {
            firstMoveBool = true;
        }

        if (BeatManager.Instance.BeatIndex == 3 && firstMoveBool)
        {
            transform.LookAt(transform.position + Vector3.back);
            NextMoveGO.GetComponent<Collider>().enabled = true;            
        }    
    }

    public void Direction()
    {
        if (BeatManager.Instance.BeatIndex == 1 && !_hasFacedDirection)
        {
            _hasFacedDirection = true;
            FaceDirection();
        }
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
                        _enemyNextMove.IsDestinationObtained = false;
                        transform.LookAt(transform.position + Vector3.forward);
                        NextMoveGO.GetComponent<Collider>().enabled = true;
                        break;
                    case 1:
                        _enemyNextMove.IsDestinationObtained = false;
                        transform.LookAt(transform.position + Vector3.back);
                        NextMoveGO.GetComponent<Collider>().enabled = true;
                        break;
                    case 2:
                        _enemyNextMove.IsDestinationObtained = false;
                        transform.LookAt(transform.position + Vector3.left);
                        NextMoveGO.GetComponent<Collider>().enabled = true;
                        break;
                    case 3:
                        _enemyNextMove.IsDestinationObtained = false;
                        transform.LookAt(transform.position + Vector3.right);
                        NextMoveGO.GetComponent<Collider>().enabled = true;
                        break;
                }
                break;
            case Enemy.EnemyType.Rook:
                switch (_faceDirectionRandRook)
                {
                    case 0:
                        _enemyNextMove.IsDestinationObtained = false;
                        transform.LookAt(transform.position + Vector3.left);
                        NextMoveGO.GetComponent<Collider>().enabled = true;
                        break;
                    case 1:
                        _enemyNextMove.IsDestinationObtained = false;
                        transform.LookAt(transform.position + Vector3.right);
                        NextMoveGO.GetComponent<Collider>().enabled = true;
                        break;
                }
                break;
        }
    }

    public void Movement()
    {
        print("movement");
        
        if (BeatManager.Instance.BeatIndex == 3 && _enemyNextMove.IsDestinationObtained)
        {
            startMoveEnemy = true;
            fourthBeatTriggered = true;
        }
        else if (BeatManager.Instance.BeatIndex == 3 && !_enemyNextMove.IsDestinationObtained)
        {
            fourthBeatTriggered = true;
        }       
    }

    private void Move()
    {
        print("move");
        IsEnemyMoving = true;
        Step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, NextMoveLocationGO.transform.position, Step);

        if (Vector3.Distance(transform.position, NextMoveLocationGO.transform.position) < 0.01f)
        {
            transform.position = NextMoveLocationGO.transform.position;
            //NextMoveGO.transform.position = _enemyNextMove.originPosition.transform.position;
            ResetValues();
        }             
    }

    //private void Move()
    //{
    //    IsEnemyMoving = true;
    //    Step = Speed * Time.deltaTime;
    //    transform.position = Vector3.MoveTowards(transform.position, NextMoveLocationGO.transform.position, Step);
    //
    //    if (Vector3.Distance(transform.position, NextMoveLocationGO.transform.position) < 0.01f)
    //    {
    //        transform.position = NextMoveLocationGO.transform.position;
    //        ResetValues();
    //    }
    //}

    public void ResetValues()
    {
        print("resetvalues");
        NextMoveLocationGO.GetComponentInParent<TileProperties>().OccupiedDecreased();
        _enemy.IsNewEnemy = false;
        firstMoveMet = true;
        IsEnemyMoving = false;
        _hasFacedDirection = false;        
        startMoveEnemy = false;
        fourthBeatTriggered = false;
        _enemyNextMove.IsOutOfBounds = false;
    }   

}
