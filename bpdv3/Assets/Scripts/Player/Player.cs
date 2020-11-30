using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Script attached to the player.
public class Player : MonoBehaviour
{
    public GameUI gameUIScript;
    PlayerMovement playerMovementScript;

    public int livesCountPlayer = 3;
    public bool isAlive = true;

    public bool isPushBack;
    
    public Vector3 startPos;

    public GameObject[] spawnPlayerDetect;

    private void Awake()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        playerMovementScript.enabled = true;
    }

    private void Update()
    {
        if (!isAlive)                    
        {
            playerMovementScript.enabled = false;
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
        playerMovementScript.ResetPosition();   // This is use so the lerp function does not continue to use the old values still.
        StartCoroutine(EnableMove());
        //gameObject.SetActive(false);
    }

    IEnumerator EnableMove()
    {
        yield return new WaitForSeconds(0.5f);
        isAlive = true;
        playerMovementScript.enabled = true;
    }
}
