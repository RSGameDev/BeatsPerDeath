using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Added to each enemy in game.
public class EnemyMovement : MonoBehaviour
{
    #region Variables
    Enemy enemyScript;
    EnemyNextMove nextMoveScript;

    [SerializeField] int firstMoveTick;
    [SerializeField] bool firstMoveFlag;

    [SerializeField] bool hasFacedDirection;
    [SerializeField] bool hasMoved;

    [SerializeField] int faceDirectionRandShroom;
    [SerializeField] int faceDirectionRandRook;

    [SerializeField] bool rookMove;

    [SerializeField] Vector3 lastPosition;

    #endregion

    private void Awake()
    {
        enemyScript = GetComponent<Enemy>();
        nextMoveScript = transform.GetChild(2).GetComponent<EnemyNextMove>();
    }
   
    // Enemy spawns on 1st row, then moves down 1 space, always the same direction. The enemy moves normally after the first move.
    public void FirstMove()
    {
        if (SceneController.Instance.gameBeatCount == 1 && !firstMoveFlag)
        {
            firstMoveFlag = true;
            firstMoveTick++;
        }

        if (SceneController.Instance.gameBeatCount == 4 && firstMoveTick == 1)
        {            
            transform.position += new Vector3(0, 0, -1.25f);
            hasMoved = true;
            enemyScript.isNew = false;
        }
        
    }

    public void MovementReset()
    {
        if (SceneController.Instance.gameBeatCount == 1)
        {
            ResetFlags();
        }
    }

    public void Direction()
    {
        if (SceneController.Instance.gameBeatCount == 2 && !hasFacedDirection)
        {
            FaceDirection();
            hasFacedDirection = true;
        }
    }

    public void Movement()
    {           
        if (enemyScript.currentEnemyType == Enemy.EnemyType.Shroom)
        {
            if (SceneController.Instance.gameBeatCount == 4 && !hasMoved)
            {
                Move();
                hasMoved = true;
            }
        }

        if (enemyScript.currentEnemyType == Enemy.EnemyType.Rook)
        {
            if (SceneController.Instance.gameBeatCount == 4 && !hasMoved)
            {
                Move();
            }
        }
    }    

    public void ResetFlags()
    {
        hasFacedDirection = false;
        hasMoved = false;
    }

    void FaceDirection()
    {
        faceDirectionRandShroom = UnityEngine.Random.Range(0, 4);
        faceDirectionRandRook = UnityEngine.Random.Range(0, 2);

        if (enemyScript.currentEnemyType == Enemy.EnemyType.Shroom)
        {
            switch (faceDirectionRandShroom)
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
        }

        if (enemyScript.currentEnemyType == Enemy.EnemyType.Rook)
        {
            switch (faceDirectionRandRook)
            {
                case 0:
                    transform.LookAt(transform.position + Vector3.left);
                    break;
                case 1:
                    transform.LookAt(transform.position + Vector3.right);
                    break;
            }
        }
    }
      
    void Move()
    {
        lastPosition = transform.position;

        if (enemyScript.currentEnemyType == Enemy.EnemyType.Shroom)
        {
            switch (faceDirectionRandShroom)
            {
                case 0:
                    transform.position += new Vector3(0, 0, 1.25f);
                    if (transform.position.z > 7)                       // This keeps the enemy from going further up the floor. Keeps the movement restricted.
                    {
                        transform.position = lastPosition;
                    }
                    break;
                case 1:
                    transform.position += new Vector3(0, 0, -1.25f);
                    if (transform.position.z < 0)                       // This keeps the enemy from going further down the floor. Keeps the movement restricted.
                    {
                        transform.position = lastPosition;
                    }
                    break;
                case 2:
                    transform.position += new Vector3(-1.25f, 0, 0);
                    if (transform.position.x < 0)                       // Same for left side.
                    {
                        transform.position = lastPosition;
                    }
                    break;
                case 3:
                    transform.position += new Vector3(1.25f, 0, 0);
                    if (transform.position.x > 5)                       // Same for right side.
                    {
                        transform.position = lastPosition;
                    }
                    break;
            }
        }

        if (enemyScript.currentEnemyType == Enemy.EnemyType.Rook)
        {
            switch (faceDirectionRandRook)
            {             
                case 0:
                    if (nextMoveScript.nextMoveTrigger)                     // A reference to detect if the rook enemy type can carry on moving.
                    {
                        transform.position += new Vector3(-1.25f, 0, 0);
                        nextMoveScript.nextMoveTrigger = false;
                    }

                    if (!nextMoveScript.CanMove())
                    {
                        transform.position = lastPosition;
                        hasMoved = true;
                    }
                                        
                    if (transform.position.x < 0)
                    {
                        transform.position = lastPosition;
                    }                   
                    break;
                case 1:
                    if (nextMoveScript.nextMoveTrigger)                     // A reference to detect if the rook enemy type can carry on moving.
                    {
                        transform.position += new Vector3(1.25f, 0, 0);
                        nextMoveScript.nextMoveTrigger = false;
                    }

                    if (!nextMoveScript.CanMove())
                    {
                        transform.position = lastPosition;
                        hasMoved = true;
                    }

                    if (transform.position.x >5)
                    {
                        transform.position = lastPosition;
                    }
                    break;
            }       
        }
    }    
}
