using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool isAlive;

    public bool isPushBack;
    
    public Vector3 startPos;

    public GameObject[] spawnPlayerDetect;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            PlayerDied();   // To reset coin spawn detection when player dies - SpawnDetectPlayer
            gameObject.SetActive(false);
        }

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
        foreach (GameObject go in spawnPlayerDetect)
        {
            go.GetComponentInChildren<SpawnDetectPlayer>().playerInFront = false;
        }
    }
}
