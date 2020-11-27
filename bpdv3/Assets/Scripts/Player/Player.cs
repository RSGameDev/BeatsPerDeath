using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Script attached to the player.
public class Player : MonoBehaviour
{
    public GameUI gameUIScript;

    public int livesCountPlayer = 3;

    bool isAlive;

    public bool isPushBack;
    
    public Vector3 startPos;

    public GameObject[] spawnPlayerDetect;


    private void Update()
    {
        if (isAlive)                    // Not quite implemented this yet as one can see. The idea was there but nothing done yet with this.
        {
        //    Movement(); 
        }
    }

    // When the player dies this function is called.
    public void StartPosition()
    {
        transform.position = startPos;
    }

    public void PlayerDied()    
    {
        livesCountPlayer--;
        gameUIScript.PlayerLoseLife(livesCountPlayer);

        foreach (GameObject go in spawnPlayerDetect)
        {
            go.GetComponentInChildren<SpawnDetectPlayer>().playerInFront = false;
        }
    }
}
