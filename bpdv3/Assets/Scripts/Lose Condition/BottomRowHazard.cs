using System;
using System.Collections;
using Core;
using EnemyNS;
using Floor;
using Mechanics;
using PlayerNS;
using UnityEngine;

// The last row represented with the flame/laser was the intention from the GDD but we couldnt find laser placeholder for time being.
namespace Lose_Condition
{
    public class BottomRowHazard : MonoBehaviour
    {
        #region Private & Constant variables

        private const string s_FireHazard = "FireHazard";
        private const string s_Player = "Player";
        [SerializeField] private Spawner _spawner;

        #endregion

        #region Public & Protected variables

        #endregion
        
        #region Constructors

        #endregion

        #region Private methods

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(s_FireHazard))
            {
                other.gameObject.transform.parent.gameObject.SetActive(false);
            }

            if (other.gameObject.CompareTag(s_Player))
            {
                _spawner.ClearListeners();
                other.GetComponent<Player>().OnPlayerDie();
            }
        }

        #endregion

        #region Public methods
        #endregion
    }
}