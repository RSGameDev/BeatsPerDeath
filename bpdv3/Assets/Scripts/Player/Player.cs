using System.Collections;
using UI.Main;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerNS
{
    [RequireComponent(typeof(PlayerMovement))]
// Script attached to the player.
    public class Player : MonoBehaviour
    {
        #region Private & Constant variables
    
        private Canvas _gameUI;
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
            _gameUI = GameObject.FindWithTag("GameUI").GetComponent<Canvas>();
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
            GetComponent<PlayerColourState>().PlayerHit();
            _livesCountPlayer--;
            _lives.PlayerLoseLife(_livesCountPlayer);
            if (_livesCountPlayer == 0)
            {
                StartCoroutine(DeathDelay(2));
            }
        }

        IEnumerator DeathDelay(int delay)
        {
            yield return new WaitForSeconds(delay);
            OnPlayerDie();
        }
        
        /// <summary>
        /// OnPlayerDie should call when you want to kill the player!
        /// </summary>
        public void OnPlayerDie()
        {
            _gameUI.enabled = false;
            Destroy(gameObject);
            GameOver();
        }


        public void GameOver()
        {
            SceneManager.LoadScene(2);
        }
    
        #endregion
    }
}
    


