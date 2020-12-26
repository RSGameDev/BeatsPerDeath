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

    public void KillPlayer()
    {
        IsPlayerAlive = false;
        LivesCountPlayer--;
        GameUI.PlayerLoseLife(LivesCountPlayer);

        foreach (GameObject go in SpawnPlayerDetectionGO)
        {
            go.GetComponentInChildren<SpawnDetectPlayer>().playerInFront = false;
        }
        StartingPosition();
        _playerMovement.IsInput = false;
    }
}
