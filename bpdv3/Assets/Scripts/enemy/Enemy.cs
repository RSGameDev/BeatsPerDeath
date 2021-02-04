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
    [SerializeField] private Transform pushBackTransform = null;
    [SerializeField] private GameObject inFront = null;
    [SerializeField] private bool isInFront = false;
    #endregion

    #region Public variables
    public bool IsEnemyAlive = true;
    public bool IsNewEnemy = true;
    #endregion

    public Transform PushBackTransform()
    {
        return pushBackTransform;
    }

    public enum EnemyType
    {
        Shroom, Rook
    }
    public EnemyType CurrentEnemyType;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var playerMovement = other.GetComponent<PlayerMovement>();
            playerMovement.enemy = this;
            playerMovement.isPushBack = true;
            FindObjectOfType<Player>().DealDamage();
        }
    }



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
    


