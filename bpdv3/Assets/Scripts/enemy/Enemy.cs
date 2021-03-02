using Floor;
using PlayerNS;
using UnityEngine;

namespace EnemyNS
{
    [RequireComponent(typeof(EnemyMovement))]
    public class Enemy : MonoBehaviour
    {
        #region Private & Constant variables

        [SerializeField] private Transform pushBackTransform = null;
        [SerializeField] private GameObject inFront = null;
        [SerializeField] private bool isInFront = false;
        private const string s_Ontile = "OnTile";

        #endregion

        #region Public & Protected variables

        public enum EnemyType
        {
            Shroom,
            Rook
        }

        public EnemyType CurrentEnemyType;
        public bool token = true;

        #endregion

        #region Constructors

        #endregion

        #region Private methods

        private void OnDisable()

        {
            token = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                var playerMovement = other.GetComponent<PlayerMovement>();
                playerMovement.enemy = this;
                playerMovement.isPushBack = true;
                playerMovement.IsPlayerInputDetected = false;
                other.gameObject.transform.position = pushBackTransform.position;
                other.gameObject.GetComponent<Player>().DealDamage();
            }

            if (other.CompareTag(s_Ontile))
            {
                if (!other.gameObject.GetComponent<OnTile>().possessToken)
                {
                    other.gameObject.GetComponent<OnTile>().possessToken = true;
                    token = false;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(s_Ontile))
            {
                other.gameObject.GetComponent<OnTile>().possessToken = false;
                token = true;
            }
        }

        #endregion

        #region Public methods

        public Transform PushBackTransform()
        {
            return pushBackTransform;
        }

        #endregion
    }
}