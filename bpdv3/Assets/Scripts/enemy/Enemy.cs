using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
// Added to each enemy in game.
public class Enemy : MonoBehaviour
{
    #region Private variables
    private GameObject _gameUiGO;
    private GameObject _scoreManagerGO;

    private Anchor _anchor;
    private EnemyMovement _enemyMovement;
    private EnemyNextMove _nextMove;
    #endregion

    #region Public variables
    public bool IsPushBack; 
    public bool IsEnemyAlive = true;
    public bool IsNewEnemy = true;
    #endregion

    public enum EnemyType
    {
        Shroom, Rook
    }
    public EnemyType CurrentEnemyType;

    

    private void Awake()
    {
        _gameUiGO = GameObject.FindGameObjectWithTag("GameUI");
        _scoreManagerGO = GameObject.FindGameObjectWithTag("ScoreManager");
        _anchor = GetComponentInChildren<Anchor>();
        _nextMove = transform.GetChild(2).GetComponent<EnemyNextMove>();
        _enemyMovement = GetComponent<EnemyMovement>();
    }
            
    // Update is called once per frame
    void Update()
    {
        if (IsEnemyAlive)
        {
            _enemyMovement.MovementReset();

            if (!IsNewEnemy)
            {
                _enemyMovement.Direction();

                if (_nextMove.CanMove())
                {
                    _enemyMovement.Movement();
                }
            }
            else if (_nextMove.CanMove())
            {
                _enemyMovement.FirstMove();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<Player>().IsPlayerPushedBack)
            {
                other.GetComponent<PlayerMovement>().PushBack = true;
            }
            else
            {
                KillEnemy();
            }
        }
    }

    private void KillEnemy()
    {
        _anchor.AnchorTileObject.GetComponent<TileProperties>().OccupiedDecreased();
        _nextMove.EnemyNextMoveTileObject.GetComponent<TileProperties>().OccupiedDecreased();
        string enemytype = CurrentEnemyType.ToString();
        int score = _scoreManagerGO.GetComponent<ScoreManager>().EnemyScore(enemytype);
        _gameUiGO.GetComponent<GameUI>().Scoring(score);
        gameObject.SetActive(false);
    }
}
