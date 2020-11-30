using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Added to each enemy in game.
public class Enemy : MonoBehaviour
{
    GameObject gameUiGo;
    GameObject scoreManagerGo;

    Anchor anchorScript;
    EnemyNextMove nextMoveScript;
    EnemyMovement enemyMovementScript;

    public bool isPushBack;

    public enum EnemyType
    {
        Shroom, Rook
    }
    public EnemyType currentEnemyType;

    public bool isAlive = true;
    public bool isNew = true;

    private void Awake()
    {
        anchorScript = GetComponentInChildren<Anchor>();
        enemyMovementScript = GetComponent<EnemyMovement>();
        nextMoveScript = transform.GetChild(2).GetComponent<EnemyNextMove>();
        gameUiGo = GameObject.FindGameObjectWithTag("GameUI");
        scoreManagerGo = GameObject.FindGameObjectWithTag("ScoreManager");
    }
            
    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            enemyMovementScript.MovementReset();

            if (!isNew)
            {
                enemyMovementScript.Direction();

                if (nextMoveScript.CanMove())
                {
                    enemyMovementScript.Movement();
                }
            }
            else if (nextMoveScript.CanMove())
            {
                enemyMovementScript.FirstMove();
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
        anchorScript.tileObj.GetComponent<TileProperties>().OccupiedDecreased();
        nextMoveScript.tileObj.GetComponent<TileProperties>().OccupiedDecreased();
        string enemytype = currentEnemyType.ToString();
        int score = scoreManagerGo.GetComponent<ScoreManager>().EnemyScore(enemytype);
        gameUiGo.GetComponent<GameUI>().Scoring(score);
        gameObject.SetActive(false);
    }
}
