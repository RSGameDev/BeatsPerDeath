using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is a gameobject placed in front of the enemy. It checks the tile in front of it to work out if another object is destined to go there.
public class EnemyNextMove : MonoBehaviour
{
    public EnemyMovement EnemyMovement;
    public int OccupiedNumber;
    public bool CanEnemyMove;
    public bool NextMoveTrigger; // <- i may remove this, not sure this is needed fyi. 4/12/20 pm

    const string s_noGoMoveLayer = "AreaLimit";

    public bool IsDestinationObtained;
    public bool IsOutOfBounds;

    public Collider VacantDestination;

    bool canMove;
    public Enemy Enemy;

    public int occupiedNum;
    public GameObject tileObj;

    public GameObject originPosition;

    public int counter;

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
            //var enemyType = Enemy.CurrentEnemyType;
            //
            //switch (enemyType)
            //{
            //    case Enemy.EnemyType.Shroom:
                    if (!IsDestinationObtained)
                    {
                        IsDestinationObtained = true;
                        EnemyMovement.NextMoveLocationGO = other.gameObject;
                        OccupiedNumber = other.GetComponentInParent<TileProperties>().occupiedNum;
                        gameObject.GetComponent<Collider>().enabled = false;
                    }
                    //break;
                //case Enemy.EnemyType.Rook:
                //    print("enemynextmove rook");
                //    if (!IsDestinationObtained)
                //    {
                //        if (Enemy.IsNewEnemy)
                //        {
                //            IsDestinationObtained = true;
                //            EnemyMovement.NextMoveLocationGO = other.gameObject;
                //            OccupiedNumber = other.GetComponentInParent<TileProperties>().occupiedNum;
                //            //gameObject.GetComponent<Collider>().enabled = false;
                //        }
                //        else
                //        {
                //            IsDestinationObtained = true;
                //            EnemyMovement.NextMoveLocationGO = other.gameObject;
                //            OccupiedNumber = other.GetComponentInParent<TileProperties>().occupiedNum;
                //            //gameObject.GetComponent<Collider>().enabled = false;
                //        }     
                //    }
                //    break;
            //}            
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

    //private void OnTriggerEnter(Collider other)
    //{
    //    print(other);
    //
    //    VacantDestination = other;
    //
    //    if (other.tag == "spawnpoint")
    //    {
    //        IsOutOfBounds = true;
    //        gameObject.GetComponent<Collider>().enabled = false;
    //        return;
    //    }
    //
    //    // The next move game object detects the next location is a tile and so is able to move on it.
    //    if (other.tag == "OnTile")
    //    {
    //        if (!IsDestinationObtained)
    //        {
    //            IsDestinationObtained = true;
    //            EnemyMovement.NextMoveLocationGO = other.gameObject;
    //            OccupiedNumber = other.GetComponentInParent<TileProperties>().occupiedNum;
    //            gameObject.GetComponent<Collider>().enabled = false;
    //        }
    //
    //    }
    //    // This is for the left and right side of the level, when the object detects there is no tile to move onto, the following code executes.
    //    else if (other.gameObject.layer == LayerMask.NameToLayer(s_noGoMoveLayer))
    //    {
    //        gameObject.GetComponent<Collider>().enabled = false;
    //        IsOutOfBounds = true;
    //    }
    //}

    //public int CheckNumber()
    //{
    //    occupiedNum = tileObj.GetComponent<TileProperties>().occupiedNum;
    //    return occupiedNum;
    //}

    public bool CanMove()
    {
        canMove = OccupiedNumber == 1 ? true : false;
        return canMove;
    }
}
