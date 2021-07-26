using EnemyNS;
using Floor;
using Managers;
using Mechanics;
using UI.Main;
using UnityEngine;

namespace PlayerNS
{
    [RequireComponent(typeof(Player))]
    public class PlayerMovement : MonoBehaviour
    {
        #region Private & Constant variables

        //[SerializeField] private PlayerAnimations _playerAnimations;
        //[SerializeField] private GameObject _playerDestinationGO;
        [SerializeField] private PlayerDestination _playerDestination;
        [SerializeField] private Anchor _anchor;
        //private Collider _colliderOfPlayerDestinationGO;
        [SerializeField] private float _speed = 1.0f;
        private float _step;

        #endregion

        #region Public & Protected variables

        
        public Enemy enemy = null;
        // ***** include again /public TileProperties tilePropertiesOnNextMoveTileGO;
        public bool IsPlayerInputDetected { get; set; }

        public bool isPushBack = false;
        //public bool IsMoving; // not used currently, gameui may use this. planning

        #endregion

        [SerializeField] private ComboMetre _comboMetre;
        [SerializeField] private GameObject nextMoveGameObject;
        public GameObject nextMoveDestinationGO;
        private bool assignedPosition;

        #region Constructor

        private void Awake()
        {
            //_colliderOfPlayerDestinationGO = _playerDestinationGO.GetComponent<Collider>();
        }

        #endregion

        #region Private methods

        private void Update()
        {
            if (Input.anyKey)
            {
                InputCapture();
            }
            

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
                    
                    //nextMoveGameObject.SetActive(true);
                    _anchor.isMoving = true;
                    //_playerAnimations.Jump();
                    //_comboMetre.perform = true;
                    _playerDestination.isObtainingDestination = true;
                    transform.LookAt(transform.position + Vector3.forward);
                    //_colliderOfPlayerDestinationGO.enabled = true;
                }
//
                if (Input.GetKeyDown(KeyCode.S))
                {
                    //nextMoveGameObject.SetActive(true);
                    _anchor.isMoving = true;
                    //_playerAnimations.Jump();
                    //_comboMetre.perform = true;
                    _playerDestination.isObtainingDestination = true;
                    transform.LookAt(transform.position + Vector3.back);
                    //_colliderOfPlayerDestinationGO.enabled = true;
                }
//
                if (Input.GetKeyDown(KeyCode.A))
                {
                    //nextMoveGameObject.SetActive(true);
                    _anchor.isMoving = true;
                    //_playerAnimations.Jump();
                    //_comboMetre.perform = true;
                    _playerDestination.isObtainingDestination = true;
                    transform.LookAt(transform.position + Vector3.left);
                    //_colliderOfPlayerDestinationGO.enabled = true;
                }
//
                if (Input.GetKeyDown(KeyCode.D))
                {
                    //nextMoveGameObject.SetActive(true);
                    _anchor.isMoving = true;
                    //_playerAnimations.Jump();
                    //_comboMetre.perform = true;
                    _playerDestination.isObtainingDestination = true;
                    transform.LookAt(transform.position + Vector3.right);
                    //_colliderOfPlayerDestinationGO.enabled = true;
                }
            
        }

        public void AssignPosition()
        {
            assignedPosition = true;
            IsPlayerInputDetected = true;
            //nextMoveGameObject.SetActive(false);
        }
        
        private void Move()
        {
            if (!nextMoveDestinationGO)
            {
                return;
            }

            if (assignedPosition)
            {
                _step = _speed * Time.deltaTime;
                //Vector3 location;
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
                    transform.position = Vector3.MoveTowards(transform.position, nextMoveDestinationGO.transform.position, _step);
                    if (Vector3.Distance(transform.position, nextMoveDestinationGO.transform.position) < 0.01f)
                    {
                        print("done");
                        transform.position = nextMoveDestinationGO.transform.position;
                        IsPlayerInputDetected = false;
                        _anchor.isMoving = false;
                        _playerDestination.isObtainingDestination = false;
                        assignedPosition = false;
                    }
                }
//
                //isPushBack = false;
            }
        }

        #endregion

        #region Public Methods

        #endregion
    }
}