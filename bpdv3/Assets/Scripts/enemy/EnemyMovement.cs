using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Added to each enemy in game.
public class EnemyMovement : MonoBehaviour
{
    public GameObject nextMoveGO;

    [SerializeField] TileController tileControllerScript;

    #region Variables
    Enemy enemyScript;
    EnemyNextMove nextMoveScript;
    [SerializeField] Anchor anchorScript;

    [SerializeField] int firstMoveTick;
    [SerializeField] bool firstMoveFlag;

    [SerializeField] bool hasFacedDirection;
    [SerializeField] bool hasMoved;

    [SerializeField] int faceDirectionRandShroom;
    [SerializeField] int faceDirectionRandRook;

    [SerializeField] bool rookMove;

    //[SerializeField] Vector3 lastPosition;

    // Lerping
    public bool lerp;
    [SerializeField] Vector3 testactualposition;
    [SerializeField] Vector3 destination;
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;

    #endregion

    private void Awake()
    {
        enemyScript = GetComponent<Enemy>();
        nextMoveScript = transform.GetChild(2).GetComponent<EnemyNextMove>();

        GameObject temp = GameObject.FindGameObjectWithTag("TileController");
        tileControllerScript = temp.GetComponent<TileController>();
    }

    void Update()
    {
        if (lerp)
        {
            Lerp();
        }

        CheckBoundaries();
    }

    private void Lerp()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(transform.position, destination, fractionOfJourney);
        if (fractionOfJourney >= 0.1f || !enemyScript.isAlive)
        {
            enemyScript.isNew = false;
            lerp = false;
            anchorScript.PlaceInPosition();
        }
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
            startTime = Time.time;
            destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.25f);
            journeyLength = Vector3.Distance(transform.position, destination);
            lerp = true;
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
        if (enemyScript.currentEnemyType == Enemy.EnemyType.Shroom)
        {
            switch (faceDirectionRandShroom)
            {
                case 0:
                    startTime = Time.time;
                    destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.25f);
                    journeyLength = Vector3.Distance(transform.position, destination);
                    lerp = true;
                    break;
                case 1:
                    startTime = Time.time;
                    destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.25f);
                    journeyLength = Vector3.Distance(transform.position, destination);
                    lerp = true;
                    break;
                case 2:
                    startTime = Time.time;
                    destination = new Vector3(transform.position.x - 1.25f, transform.position.y, transform.position.z);
                    journeyLength = Vector3.Distance(transform.position, destination);
                    lerp = true;
                    break;
                case 3:
                    startTime = Time.time;
                    destination = new Vector3(transform.position.x + 1.25f, transform.position.y, transform.position.z);
                    journeyLength = Vector3.Distance(transform.position, destination);
                    lerp = true;
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
                        startTime = Time.time;
                        destination = new Vector3(transform.position.x - 1.25f, transform.position.y, transform.position.z);
                        journeyLength = Vector3.Distance(transform.position, destination);
                        lerp = true;
                        nextMoveScript.nextMoveTrigger = false;
                    }
                    
                    if (!nextMoveScript.CanMove())
                    {
                        transform.position = new Vector3 (nextMoveScript.tileObj.transform.position.x + 1.25f, nextMoveScript.tileObj.transform.position.y + 1, nextMoveScript.tileObj.transform.position.z);
                        anchorScript.PlaceInPosition();
                        hasMoved = true;
                    }                                    
                    break;
                case 1:
                    if (nextMoveScript.nextMoveTrigger)                     // A reference to detect if the rook enemy type can carry on moving.
                    {
                        startTime = Time.time;
                        destination = new Vector3(transform.position.x + 1.25f, transform.position.y, transform.position.z);
                        journeyLength = Vector3.Distance(transform.position, destination);
                        lerp = true;
                        nextMoveScript.nextMoveTrigger = false;
                    }
        
                    if (!nextMoveScript.CanMove())
                    {
                        transform.position = new Vector3(nextMoveScript.tileObj.transform.position.x - 1.25f, nextMoveScript.tileObj.transform.position.y + 1, nextMoveScript.tileObj.transform.position.z);
                        anchorScript.PlaceInPosition();
                        hasMoved = true;
                    }
                    break;
            }       
        }
    }

    void CheckBoundaries()
    {
        if (!enemyScript.isNew)
        {
            if (transform.position.z > 6.25)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 6.25f);
                lerp = false;
            }
        }        

        if (!tileControllerScript.tilesScrolling)
        {
            if (transform.position.z < 1.25)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 1.25f);
                lerp = false;
            }
        }

        if (transform.position.x < 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
            lerp = false;
        }

        if (transform.position.x > 5)
        {
            transform.position = new Vector3(5, transform.position.y, transform.position.z);
            lerp = false;
        }
    }
}
