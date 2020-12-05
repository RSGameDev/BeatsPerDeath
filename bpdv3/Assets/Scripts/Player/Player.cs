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

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        _playerMovement.enabled = true;
    }

    private void Update()
    {
        if (!IsPlayerAlive)                    
        {
            _playerMovement.enabled = false;
        }
    }

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
        _playerMovement.ResetPosition();   // This is use so the lerp function does not continue to use the old values still.
        StartCoroutine(EnableMove());
    }

    private IEnumerator EnableMove()
    {
        yield return new WaitForSeconds(0.5f);
        IsPlayerAlive = true;
        _playerMovement.enabled = true;
    }
}
