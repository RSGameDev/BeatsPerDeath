using Floor;
using UnityEngine;


// This script is a gameobject placed in front of the enemy. It checks the tile in front of it to work out if another object is destined to go there.
namespace Scripts.Enemy
{
    public class EnemyNextMove : MonoBehaviour
    {
        #region Private & Constant Variables 
    
        [SerializeField] private EnemyMovement _enemyMovement;
        private const string s_inAccessibleArea = "AreaLimit";
        private const string s_SpawnPoint = "spawnpoint";
        private const string s_Ontile = "OnTile";
        private int _occupiedNumber;
        private bool _canMove;

        #endregion

        #region Public & Protected Variables

        public bool IsDestinationObtained { get; set; }
    
        //public bool IsOutOfBounds;
    
        #endregion

        #region Constructors

        private void Awake()
        {
            gameObject.GetComponent<Collider>().enabled = false;
        }

        #endregion

        #region Private Methods

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(s_SpawnPoint))
            {
                //**IsOutOfBounds = true;
                gameObject.GetComponent<Collider>().enabled = false;
                return;
            }

            // The next move game object detects the next location is a tile and so is able to move on it.
            if (other.CompareTag(s_Ontile))
            {
                print("enemynextmove");            
                if (!IsDestinationObtained)
                {
                    IsDestinationObtained = true;
                    _enemyMovement.NextMoveLocationGO = other.gameObject;
                    _occupiedNumber = other.GetComponentInParent<TileProperties>().OccupiedNumber;
                    gameObject.GetComponent<Collider>().enabled = false;
                }               
            }

            // This is for the left and right side of the level, when the object detects there is no tile to move onto, the following code executes.
            else if (other.gameObject.layer == LayerMask.NameToLayer(s_inAccessibleArea))
            {
                _occupiedNumber = other.GetComponentInParent<TileProperties>().OccupiedNumber;
                other.gameObject.GetComponentInParent<TileProperties>().OccupiedDecreased();
                gameObject.GetComponent<Collider>().enabled = false;
                //**IsOutOfBounds = true;
            }       
        } 
    
        #endregion

        #region Public Methods

        public bool AllowedToMove()
        {
            _canMove = _occupiedNumber == 1 ? true : false;
            return _canMove;
        }

        #endregion
    
    
    
    
    }
}
