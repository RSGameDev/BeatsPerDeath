using Mechanics;
using Scripts.A;
using UnityEngine;

namespace Scripts.Enemy
{
    [RequireComponent(typeof(EnemyMovement))]
    public class Enemy : MonoBehaviour
    {
        #region Private & Constant Variables

        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private Transform pushBackTransform = null;
        [SerializeField] private GameObject inFront = null;
        [SerializeField] private bool isInFront = false;
        private const string s_Player = "Player";
        private bool _isEnemyAlive = true;
        
        #endregion

        #region Public & Protected Variables

        public bool IsNewEnemy { private get; set; } = true;

        #endregion

        #region Constructors
        #endregion

        #region Private Methods
        // Update is called once per frame
        private void Update()
        {
            if (!_isEnemyAlive) return;
            
            switch (IsNewEnemy)
            {
                case true:
                    _enemyMovement.FirstMove();
                    break;
                case false:
                    _enemyMovement.Direction();                                
                    _enemyMovement.Movement();
                    break;
            }
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == s_Player)
            {
                var playerMovement = other.GetComponent<PlayerMovement>();
                playerMovement.enemy = this;
                playerMovement.isPushBack = true;
                playerMovement.IsPlayerInputDetected = false;
                other.gameObject.transform.position = pushBackTransform.position;
                other.gameObject.GetComponent<Player>().DealDamage();
            }
        }

        #endregion

        #region Public Methods

        public Transform PushBackTransform()
        {
            return pushBackTransform;
        }

        public enum EnemyType
        {
            Shroom, Rook
        }
        public EnemyType CurrentEnemyType;

        #endregion
    }
}
    


