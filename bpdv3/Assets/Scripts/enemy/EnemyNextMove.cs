using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is a gameobject placed in front of the enemy. It checks the tile in front of it to work out if another object is destined to go there.
public class EnemyNextMove : MonoBehaviour
{
    #region Private variables 
    const string s_noGoMoveLayer = "AreaLimit";
    private bool _canMove;
    #endregion

    #region Public variables
    public Enemy Enemy;
    public EnemyMovement EnemyMovement;
    public Collider VacantDestination;

    public int OccupiedNumber;
    public bool CanEnemyMove;
    public bool NextMoveTrigger; // <- i may remove this, not sure this is needed fyi. 4/12/20 pm
    public bool IsDestinationObtained;
    public bool IsOutOfBounds;
    #endregion 

    private void Awake()
    {
        gameObject.GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {        
        VacantDestination = other;

        if (other.tag == "spawnpoint")
        {
            IsOutOfBounds = true;
            gameObject.GetComponent<Collider>().enabled = false;
            return;
        }

        // The next move game object detects the next location is a tile and so is able to move on it.
        if (other.tag == "OnTile")
        {
            print("enemynextmove");            
            if (!IsDestinationObtained)
            {
                IsDestinationObtained = true;
                EnemyMovement.NextMoveLocationGO = other.gameObject;
                OccupiedNumber = other.GetComponentInParent<TileProperties>().occupiedNum;
                gameObject.GetComponent<Collider>().enabled = false;
            }               
        }

        // This is for the left and right side of the level, when the object detects there is no tile to move onto, the following code executes.
        else if (other.gameObject.layer == LayerMask.NameToLayer(s_noGoMoveLayer))
        {
            OccupiedNumber = other.GetComponentInParent<TileProperties>().occupiedNum;
            other.gameObject.GetComponentInParent<TileProperties>().OccupiedDecreased();
            gameObject.GetComponent<Collider>().enabled = false;
            IsOutOfBounds = true;
        }       
    } 
    
    public bool CanMove()
    {
        _canMove = OccupiedNumber == 1 ? true : false;
        return _canMove;
    }
}
