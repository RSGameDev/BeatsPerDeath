using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(PlayerMovement))]
// Script attached to the player.
    public class Player : MonoBehaviour
    {
        #region Private & Constant Variables
    
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private Vector3 _startPosition;
        private PlayerMovement _playerMovement;
        private int _livesCountPlayer = 3;

        #endregion

        #region Public & Protected Variables
        #endregion

        #region Constructors
        #endregion

        #region Private Methods
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
            _gameUI.enabled = false;
            /*
         * #TODO a canvas
         * FindObjectOfType<GameOverDisplay>().enabled = true; 
        */
            Destroy(gameObject);
        }
    
        #endregion
    }
}

