using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Added to each enemy in game.
public class EnemyMovement : MonoBehaviour
{
    #region Private variables
    private Enemy _enemy;
    private EnemyNextMove _nextMove;
    private TileController _tileController;

    #endregion

    #region Public variables
    public GameObject nextMoveGO;

    public Anchor anchorScript;

    private int firstMoveTick;
    private int faceDirectionRandShroom;
    private int faceDirectionRandRook;

    private bool firstMoveFlag;    
    private bool hasFacedDirection;
    private bool hasMoved;  
    #endregion

    #region Function
    // Lerp    
    [SerializeField] Vector3 destination;
    private float startTime;
    private float journeyLength;
    public float speed = 1.0F;
    public bool lerp;
    #endregion

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _nextMove = transform.GetChild(2).GetComponent<EnemyNextMove>();

        GameObject temp = GameObject.FindGameObjectWithTag("TileController");
        _tileController = temp.GetComponent<TileController>();
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
        if (fractionOfJourney >= 0.1f || !_enemy.isAlive)
        {
            _enemy.isNew = false;
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
        if (_enemy.currentEnemyType == Enemy.EnemyType.Shroom)
        {
            if (SceneController.Instance.gameBeatCount == 4 && !hasMoved)
            {
                Move();
                hasMoved = true;
            }
        }

        if (_enemy.currentEnemyType == Enemy.EnemyType.Rook)
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

        if (_enemy.currentEnemyType == Enemy.EnemyType.Shroom)
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

        if (_enemy.currentEnemyType == Enemy.EnemyType.Rook)
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
        if (_enemy.currentEnemyType == Enemy.EnemyType.Shroom)
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

        if (_enemy.currentEnemyType == Enemy.EnemyType.Rook)
        {
            switch (faceDirectionRandRook)
            {             
                case 0:
                    if (_nextMove.nextMoveTrigger)                     // A reference to detect if the rook enemy type can carry on moving.
                    {                    
                        startTime = Time.time;
                        destination = new Vector3(transform.position.x - 1.25f, transform.position.y, transform.position.z);
                        journeyLength = Vector3.Distance(transform.position, destination);
                        lerp = true;
                        _nextMove.nextMoveTrigger = false;
                    }
                    
                    if (!_nextMove.CanMove())
                    {
                        transform.position = new Vector3 (_nextMove.tileObj.transform.position.x + 1.25f, _nextMove.tileObj.transform.position.y + 1, _nextMove.tileObj.transform.position.z);
                        anchorScript.PlaceInPosition();
                        hasMoved = true;
                    }                                    
                    break;
                case 1:
                    if (_nextMove.nextMoveTrigger)                     // A reference to detect if the rook enemy type can carry on moving.
                    {
                        startTime = Time.time;
                        destination = new Vector3(transform.position.x + 1.25f, transform.position.y, transform.position.z);
                        journeyLength = Vector3.Distance(transform.position, destination);
                        lerp = true;
                        _nextMove.nextMoveTrigger = false;
                    }
        
                    if (!_nextMove.CanMove())
                    {
                        transform.position = new Vector3(_nextMove.tileObj.transform.position.x - 1.25f, _nextMove.tileObj.transform.position.y + 1, _nextMove.tileObj.transform.position.z);
                        anchorScript.PlaceInPosition();
                        hasMoved = true;
                    }
                    break;
            }       
        }
    }

    void CheckBoundaries()
    {
        if (!_enemy.isNew)
        {
            if (transform.position.z > 6.25)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 6.25f);
                lerp = false;
            }
        }        

        if (!_tileController.tilesScrolling)
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
