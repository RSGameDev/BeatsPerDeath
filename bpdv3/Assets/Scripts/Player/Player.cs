using UI.Main;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scripts.Enemy;
//You can not put a namespace with the same name of the script!!!!
namespace Scripts.A
{
    [RequireComponent(typeof(PlayerMovement))]
// Script attached to the player.
    public class Player : MonoBehaviour
    {
        #region Private & Constant variables
    
        [SerializeField] private Lives _lives;
        [SerializeField] private Vector3 _startPosition;
        private PlayerMovement _playerMovement;
        private int _livesCountPlayer = 3;

        #endregion

        #region Public & Protected variables
        #endregion

        #region Constructors
        #endregion

        #region Private Methods
        private void Awake()
        {
            _gameUI = FindObjectOfType<GameUI>();
        }

        #endregion

        #region Public Methods

        // When the player dies this function is called.
        public void StartingPosition()
        {
            transform.position = _startPosition;
        }
    
        /// <summary>
        /// Deal Damage to the player
        /// </summary>
        public void DealDamage()
        {
            _livesCountPlayer--;
            _gameUI.PlayerLoseLife(_livesCountPlayer);
            if (_livesCountPlayer == 0)
            {
                OnPlayerDie();
            }
        }

        /// <summary>
        /// OnPlayerDie should call when you want to kill the player!
        /// </summary>
        public void OnPlayerDie()
        {
            //_gameUI.enabled = false;
            /*
         * #TODO a canvas
         * FindObjectOfType<GameOverDisplay>().enabled = true; 
        */
            Destroy(gameObject);
        }
    
        #endregion
    }
}
    


