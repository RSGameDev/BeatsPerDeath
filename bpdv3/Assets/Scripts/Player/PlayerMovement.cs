using EnemyNS;
using Floor;
using Managers;
using UI.Main;
using UnityEngine;

namespace PlayerNS
{
    [RequireComponent(typeof(Player))]
    public class PlayerMovement : MonoBehaviour
    {
        #region Private & Constant variables

        [SerializeField] private PlayerAnimations _playerAnimations;
        [SerializeField] private GameObject _playerDestinationGO;
        [SerializeField] private PlayerDestination _playerDestination;
        private Collider _colliderOfPlayerDestinationGO;
        [SerializeField] private float _speed = 1.0f;
        private float _step;

        #endregion

        #region Public & Protected variables

        public GameObject nextMoveTileGO;
        public Enemy enemy = null;
        // ***** include again /public TileProperties tilePropertiesOnNextMoveTileGO;
        public bool IsPlayerInputDetected { get; set; }

        public bool isPushBack = false;
        //public bool IsMoving; // not used currently, gameui may use this. planning

        #endregion

        [SerializeField] private ComboMetre _comboMetre;
        private Vector3 nextMovePosition;

        #region Constructor

        private void Awake()
        {
            //_colliderOfPlayerDestinationGO = _playerDestinationGO.GetComponent<Collider>();
        }

        #endregion

        #region Private methods

        private void Update()
        {
            InputCapture();

            //if (_playerDestination.IsDestinationObtained)
            //{
                Move();
            //}
        }

        private void InputCapture()
        {
            //if (!IsPlayerInputDetected)
            //{
                if (Input.GetKeyDown(KeyCode.W))
                {
                    //_playerAnimations.Jump();
                    IsPlayerInputDetected = true;
                    //_comboMetre.perform = true;
                    //_playerDestination.IsDestinationObtained = false;
                    transform.LookAt(transform.position + Vector3.forward);
                    AssignPosition();
                    //_colliderOfPlayerDestinationGO.enabled = true;
                }
//
                if (Input.GetKeyDown(KeyCode.S))
                {
                    //_playerAnimations.Jump();
                    IsPlayerInputDetected = true;
                    //_comboMetre.perform = true;
                    //_playerDestination.IsDestinationObtained = false;
                    transform.LookAt(transform.position + Vector3.back);
                    AssignPosition();
                    //_colliderOfPlayerDestinationGO.enabled = true;
                }
//
                if (Input.GetKeyDown(KeyCode.A))
                {
                    //_playerAnimations.Jump();
                    IsPlayerInputDetected = true;
                    //_comboMetre.perform = true;
                    //_playerDestination.IsDestinationObtained = false;
                    transform.LookAt(transform.position + Vector3.left);
                    AssignPosition();
                    //_colliderOfPlayerDestinationGO.enabled = true;
                }
//
                if (Input.GetKeyDown(KeyCode.D))
                {
                    //_playerAnimations.Jump();
                    IsPlayerInputDetected = true;
                    //_comboMetre.perform = true;
                    //_playerDestination.IsDestinationObtained = false;
                    transform.LookAt(transform.position + Vector3.right);
                    AssignPosition();
                    //_colliderOfPlayerDestinationGO.enabled = true;
                }
            
        }

        void AssignPosition()
        {
            nextMovePosition = nextMoveTileGO.transform.position;
            nextMoveTileGO.SetActive(false);
        }
        
        private void Move()
        {
            _step = _speed * Time.deltaTime;
            Vector3 location;
//
            //if (isPushBack)
            //{
            //    IsPlayerInputDetected = false;
            //    // ***** include again / location = enemy.PushBackTransform().position;
            //    // ***** include again / transform.position = location;
            //    isPushBack = false;
            //}
            //else if (!isPushBack && IsPlayerInputDetected)
            if (IsPlayerInputDetected)
            {
                //location = new Vector3(position.x, position.y, position.z);
                transform.position = Vector3.MoveTowards(transform.position, nextMovePosition, _step);
                if (Vector3.Distance(transform.position, nextMovePosition) < 0.01f)
                {
                    transform.position = nextMovePosition;
                    IsPlayerInputDetected = false;
                }
            }
//
            //isPushBack = false;
        }

        #endregion

        #region Public Methods

        #endregion
    }
}