using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        if (isAlive)
        {
        //    Movement(); 
        }
    }

    public void StartPosition()
    {
        transform.position = startPos;
    }

    public void PlayerDied()    // To reset coin spawn detection when player dies - SpawnDetectPlayer
    {
        livesCountPlayer--;
        gameUIScript.PlayerLoseLife(livesCountPlayer);

        foreach (GameObject go in spawnPlayerDetect)
        {
            go.GetComponentInChildren<SpawnDetectPlayer>().playerInFront = false;
        }

        //gameObject.SetActive(false);
    }
}
