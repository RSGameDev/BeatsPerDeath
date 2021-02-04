using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
// Script attached to the player.
public class Player : MonoBehaviour
{
    #region Private variables
    private PlayerMovement _playerMovement;
    #endregion

    #region Public variables
    public GameObject[] SpawnPlayerDetectionGO;

    public GameUI GameUI;
    public Vector3 StartPosition;

    public int LivesCountPlayer = 3;
    public bool IsPlayerAlive = true;
    public bool IsPlayerPushedBack;
    #endregion    
    
    // When the player dies this function is called.
    public void StartingPosition()
    {
        transform.position = StartPosition;
    }


    //#Note Bad code! 
    /*public void KillPlayer()
    //{
    //    IsPlayerAlive = false;
    //    LivesCountPlayer--;
    //    GameUI.PlayerLoseLife(LivesCountPlayer);

    //    foreach (GameObject go in SpawnPlayerDetectionGO)
    //    {
    //        go.GetComponentInChildren<SpawnDetectPlayer>().playerInFront = false;
    //    }
    //    StartingPosition();
    //    _playerMovement.IsInput = false;
      }
    */
    /// <summary>
    /// Deal Damage to the player
    /// </summary>
    public void DealDamage()
    {
        LivesCountPlayer--;
        GameUI.PlayerLoseLife(LivesCountPlayer);

        if (LivesCountPlayer == 0)
        {
            OnPlayerDie();
        }
    }


    /// <summary>
    /// OnPlayerDie should call when you want to kill the player!
    /// </summary>
    public void OnPlayerDie()
    {
        GameUI.enabled = false;
        /*
         * #TODO a canvas
         * FindObjectOfType<GameOverDisplay>().enabled = true; 
        */
        Destroy(gameObject);
    }
}
