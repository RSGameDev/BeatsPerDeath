using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    public bool isPushBack; 
    public bool isAlive = true;
    public bool isNew = true;
    #endregion

    public enum EnemyType
    {
        Shroom, Rook
    }
    public EnemyType currentEnemyType;

    

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
        if (isAlive)
        {
            _enemyMovement.MovementReset();

            if (!isNew)
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
            if (other.GetComponent<Player>().isPushBack)
            {
                other.GetComponent<PlayerMovement>().pushBack = true;
            }
            else
            {
                EnemyDies();
            }
        }
    }

    private void EnemyDies()
    {
        _anchor.tileObj.GetComponent<TileProperties>().OccupiedDecreased();
        _nextMove.tileObj.GetComponent<TileProperties>().OccupiedDecreased();
        string enemytype = currentEnemyType.ToString();
        int score = _scoreManagerGO.GetComponent<ScoreManager>().EnemyScore(enemytype);
        _gameUiGO.GetComponent<GameUI>().Scoring(score);
        gameObject.SetActive(false);
    }
}
