using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
   
    public void FirstMove()
    {
        //////////// because of old wwise taken out
        ////////////if (MusicScript.gameBeatCount == 1 && !firstMoveFlag)
        {
            firstMoveFlag = true;
            firstMoveTick++;
        }

        //////////// because of old wwise taken out
        //////////if (MusicScript.gameBeatCount == 4 && firstMoveTick == 1)
        {            
            transform.position += new Vector3(0, 0, -1.25f);
            hasMoved = true;
            enemyScript.isNew = false;
        }
        
    }

    public void MovementReset()
    {
        //////////// because of old wwise taken out
        ///////////if (MusicScript.gameBeatCount == 1)
        {
            ResetFlags();
        }
    }

    public void Direction()
    {
        //////////// because of old wwise taken out
        //////////if (MusicScript.gameBeatCount == 2 && !hasFacedDirection)
        {
            FaceDirection();
            hasFacedDirection = true;
        }
    }

    public void Movement()
    {           
        if (enemyScript.currentEnemyType == Enemy.EnemyType.Shroom)
        {
            //////////// because of old wwise taken out
            /////////if (MusicScript.gameBeatCount == 4 && !hasMoved)
            {
                Move();
                hasMoved = true;
            }
        }

        if (enemyScript.currentEnemyType == Enemy.EnemyType.Rook)
        {
            //////////// because of old wwise taken out
            //////////if (MusicScript.gameBeatCount == 4 && !hasMoved)
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
                    if (transform.position.z > 7) 
                    {
                        transform.position = lastPosition;
                    }
                    break;
                case 1:
                    transform.position += new Vector3(0, 0, -1.25f);
                    if (transform.position.z < 0)
                    {
                        transform.position = lastPosition;
                    }
                    break;
                case 2:
                    transform.position += new Vector3(-1.25f, 0, 0);
                    if (transform.position.x < 0)
                    {
                        transform.position = lastPosition;
                    }
                    break;
                case 3:
                    transform.position += new Vector3(1.25f, 0, 0);
                    if (transform.position.x > 5)
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
                    if (nextMoveScript.nextMoveTrigger)                     // allow time for trigger on other object to trigger.
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
                    if (nextMoveScript.nextMoveTrigger)
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

            //switch (faceDirectionRandRook)
            //{
            //    case 0:
            //        transform.position += new Vector3(-1.25f, 0, 0);
            //        if (transform.position.x < 0)
            //        {
            //            transform.position = position;
            //        }
            //        break;
            //    case 1:
            //        transform.position += new Vector3(1.25f, 0, 0);
            //        if (transform.position.x > 5)
            //        {
            //            transform.position = position;
            //        }
            //        break;
            //}
        }
    }

    void MoveLeft()
    {
        transform.position += new Vector3(-1.25f, 0, 0);
    }

    void MoveRight()
    {
        transform.position += new Vector3(1.25f, 0, 0);
    }

    private void Update()
    {
    }
}
