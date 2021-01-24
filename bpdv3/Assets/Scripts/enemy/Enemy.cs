using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
// Added to each enemy in game.
public class Enemy : MonoBehaviour
{
    #region Private variables

    [SerializeField] private Anchor _anchor;
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private EnemyNextMove _nextMove;
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

    // Update is called once per frame
    void Update()
    {
        if (IsEnemyAlive)
        {
            if (IsNewEnemy)
            {
                _enemyMovement.FirstMove();
            }

            if (!IsNewEnemy)
            {
                _enemyMovement.Direction();                                
                _enemyMovement.Movement();                   
            }            
        }
    }
}
    


