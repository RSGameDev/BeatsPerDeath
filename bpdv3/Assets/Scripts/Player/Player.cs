using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Script attached to the player.
public class Player : MonoBehaviour
{
    #region Private variables
    private PlayerMovement _playerMovement;
    #endregion

    #region Public variables
    public GameObject[] spawnPlayerDetect;

    public GameUI gameUIScript;
    public Vector3 startPos;

    public int livesCountPlayer = 3;

    public bool isAlive = true;
    public bool isPushBack;
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
        if (!isAlive)                    
        {
            _playerMovement.enabled = false;
        }
    }

    // When the player dies this function is called.
    public void StartPosition()
    {
        transform.position = startPos;
    }

    public void PlayerDied()    
    {
        isAlive = false;
        livesCountPlayer--;
        gameUIScript.PlayerLoseLife(livesCountPlayer);

        foreach (GameObject go in spawnPlayerDetect)
        {
            go.GetComponentInChildren<SpawnDetectPlayer>().playerInFront = false;
        }
        StartPosition();
        _playerMovement.ResetPosition();   // This is use so the lerp function does not continue to use the old values still.
        StartCoroutine(EnableMove());
    }

    IEnumerator EnableMove()
    {
        yield return new WaitForSeconds(0.5f);
        isAlive = true;
        _playerMovement.enabled = true;
    }
}
