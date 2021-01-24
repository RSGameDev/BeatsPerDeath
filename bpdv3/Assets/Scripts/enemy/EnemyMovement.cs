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

    [SerializeField] private int _faceDirectionRandShroom;
    [SerializeField] private int _faceDirectionRandRook;
    [SerializeField] private bool _hasFacedDirection;
    #endregion

    #region Public variables
    public GameObject NextMoveGO;
    public GameObject NextMoveLocationGO;

    public Anchor Anchor;

    public float Speed = 2.0f;
    public float Step;
    public bool IsOnBeat;

    public bool StartMoveEnemy;
    public bool IsEnemyMoving;
    public bool FirstMoveMet;
    public bool FirstMoveBool;
    public int FirstMoveBeatCount;
    public bool FourthBeatTriggered;
    #endregion

    void Update()
    {
        if (_enemyNextMove.IsDestinationObtained && !FirstMoveMet)
        {
            Move();
        }

        if (FourthBeatTriggered && StartMoveEnemy)
        {
            if (_enemy.CurrentEnemyType  == Enemy.EnemyType.Rook)
            {
                if (_enemyNextMove.CanMove())
                {
                    StartMoveEnemy = false;
                    NextMoveGO.transform.position += Vector3.forward * 5;
                    NextMoveGO.GetComponent<Collider>().enabled = true;
                    return;
                }                
            }
            Move();
        }
        else if (FourthBeatTriggered && !StartMoveEnemy)
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
            FirstMoveBool = true;
        }

        if (BeatManager.Instance.BeatIndex == 3 && FirstMoveBool)
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
            StartMoveEnemy = true;
            FourthBeatTriggered = true;
        }
        else if (BeatManager.Instance.BeatIndex == 3 && !_enemyNextMove.IsDestinationObtained)
        {
            FourthBeatTriggered = true;
        }       
    }

    private void Move()
    {
        if (_enemyNextMove.CanMove())
        {
            print("move");
            IsEnemyMoving = true;
            Step = Speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, NextMoveLocationGO.transform.position, Step);

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

    public void ResetValues()
    {
        print("resetvalues");
        NextMoveLocationGO.GetComponentInParent<TileProperties>().OccupiedDecreased();
        _enemy.IsNewEnemy = false;
        FirstMoveMet = true;
        IsEnemyMoving = false;
        _hasFacedDirection = false;        
        StartMoveEnemy = false;
        FourthBeatTriggered = false;
        _enemyNextMove.IsOutOfBounds = false;
    }   

}
