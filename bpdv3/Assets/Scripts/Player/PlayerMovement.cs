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

        [SerializeField] private GameObject _playerDestinationGO;
        [SerializeField] private PlayerDestination _playerDestination;
        private Collider _colliderOfPlayerDestinationGO;
        [SerializeField] private float _speed = 1.0f;
        private float _step;

        #endregion

        #region Public & Protected variables

        public GameObject nextMoveTileGO;
        public Enemy enemy = null;
        public TileProperties tilePropertiesOnNextMoveTileGO;
        public bool IsPlayerInputDetected { get; set; }

        public bool isPushBack = false;
        //public bool IsMoving; // not used currently, gameui may use this. planning

        #endregion

        [SerializeField] private ComboMetre _comboMetre;

        #region Constructor

        private void Awake()
        {
            _colliderOfPlayerDestinationGO = _playerDestinationGO.GetComponent<Collider>();
        }

        #endregion

        #region Private methods

        private void Update()
        {
            InputCapture();

            if (_playerDestination.IsDestinationObtained)
            {
                Move();
            }
        }

        private void InputCapture()
        {
            if (!IsPlayerInputDetected)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    IsPlayerInputDetected = true;
                    _comboMetre.perform = true;
                    _playerDestination.IsDestinationObtained = false;
                    transform.LookAt(transform.position + Vector3.forward);
                    _colliderOfPlayerDestinationGO.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    IsPlayerInputDetected = true;
                    _comboMetre.perform = true;
                    _playerDestination.IsDestinationObtained = false;
                    transform.LookAt(transform.position + Vector3.back);
                    _colliderOfPlayerDestinationGO.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.A))
                {
                    IsPlayerInputDetected = true;
                    _comboMetre.perform = true;
                    _playerDestination.IsDestinationObtained = false;
                    transform.LookAt(transform.position + Vector3.left);
                    _colliderOfPlayerDestinationGO.enabled = true;
                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    IsPlayerInputDetected = true;
                    _comboMetre.perform = true;
                    _playerDestination.IsDestinationObtained = false;
                    transform.LookAt(transform.position + Vector3.right);
                    _colliderOfPlayerDestinationGO.enabled = true;
                }
            }
        }

        private void Move()
        {
            _step = _speed * Time.deltaTime;
            Vector3 location;

            if (isPushBack)
            {
                IsPlayerInputDetected = false;
                location = enemy.PushBackTransform().position;
                transform.position = location;
                isPushBack = false;
            }
            else if (!isPushBack && IsPlayerInputDetected)
            {
                var position = nextMoveTileGO.transform.position;
                location = new Vector3(position.x, position.y, position.z);
                transform.position = Vector3.MoveTowards(transform.position, location, _step);
                if (Vector3.Distance(transform.position, location) < 0.01f)
                {
                    transform.position = location;
                    IsPlayerInputDetected = false;
                }
            }

            isPushBack = false;
        }

        #endregion

        #region Public Methods

        #endregion
    }
}