using Floor;
using UnityEngine;

namespace Scripts.A
{
    public class PlayerDestination : MonoBehaviour
    {
        #region Private & Constant Variables

        const string s_nextMoveLayer = "OnTile";
        const string s_noGoMoveLayer = "AreaLimit";
        private const float s_verticalTopLimit = 7.5F;
        private const float s_verticalBottomLimit = 0F;

        #endregion

        #region Public & Protected Variables

        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Collider _vacantDestinationCollider;
        public bool IsDestinationObtained { get; set; }

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
            _vacantDestinationCollider = other;

            if (other.gameObject.layer == LayerMask.NameToLayer(s_nextMoveLayer))
            {
                if (!IsDestinationObtained)
                {
                    _playerMovement.nextMoveTileGO = other.gameObject;
                    _playerMovement.tilePropertiesOnNextMoveTileGO = other.gameObject.GetComponentInParent<TileProperties>();
                    CheckBoundaries(other);
                    gameObject.GetComponent<Collider>().enabled = false;
                }
            }
            // This is for the left and right side of the level, when the object detects there is no tile to move onto, the following code executes.
            else if (other.gameObject.layer == LayerMask.NameToLayer(s_noGoMoveLayer))
            {
                gameObject.GetComponent<Collider>().enabled = false;
                _playerMovement.IsPlayerInputDetected = false;
            }
        }

        private void CheckBoundaries(Collider other)
        {
            if (transform.position.z >= s_verticalTopLimit || transform.position.z <= s_verticalBottomLimit)
            {
                IsDestinationObtained = false;
                _playerMovement.IsPlayerInputDetected = false;
                other.gameObject.GetComponentInParent<TileProperties>().OccupiedDecreased();
            }
            else
            {
                IsDestinationObtained = true;
            }
        }

        private void Update()
        {
            MovementBoundary(_vacantDestinationCollider);
        }

        private void MovementBoundary(Collider other)
        {
            // TODO repeat code here and in check boundaries
            if (transform.position.z >= s_verticalTopLimit || transform.position.z <= s_verticalBottomLimit)
            {
                IsDestinationObtained = false;
                _playerMovement.IsPlayerInputDetected = false;
                other.gameObject.GetComponentInParent<TileProperties>().OccupiedDecreased();
            }
        }

        #endregion

        #region Public Methods
        #endregion
    
    }
}


    

