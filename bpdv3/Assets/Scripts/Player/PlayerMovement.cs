using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script attached to the player.
public class PlayerMovement : MonoBehaviour
{
    Player playerScript;

    public bool pushBack;
    public bool isMoving;
    public bool onBeat;
    public GameObject hitZone;

    public string direction;

    private void Awake()
    {
        playerScript = GetComponent<Player>();
    }        

    // Update is called once per frame
    void Update()
    {
        Movement();
        if (pushBack)
        {
            PlayerPushBack(direction);
        }        
    }

    void Movement()
    {
        Vector3 position;
        position = transform.position;

        if (Input.GetKeyDown("w"))
        {
            isMoving = true;
            onBeat = hitZone.GetComponent<CoreHitzone>().onBeat;
            direction = "w";
            transform.LookAt(transform.position + Vector3.forward);
            transform.position += new Vector3(0, 0, 1.25f);
            if (transform.position.z > 7)
            {
                transform.position = position;
            }
        }

        if (Input.GetKeyDown("s"))
        {
            isMoving = true;
            onBeat = hitZone.GetComponent<CoreHitzone>().onBeat;
            direction = "s";
            transform.LookAt(transform.position + Vector3.back);
            transform.position += new Vector3(0, 0, -1.25f);
            if (transform.position.z < 0)
            {
                transform.position = position;
            }
        }

        if (Input.GetKeyDown("a"))
        {
            isMoving = true;
            onBeat = hitZone.GetComponent<CoreHitzone>().onBeat;
            direction = "a";
            transform.LookAt(transform.position + Vector3.left);
            transform.position += new Vector3(-1.25f, 0, 0);
            if (transform.position.x < 0)
            {
                transform.position = position;
            }
        }

        if (Input.GetKeyDown("d"))
        {
            isMoving = true;
            onBeat = hitZone.GetComponent<CoreHitzone>().onBeat;
            direction = "d";
            transform.LookAt(transform.position + Vector3.right);
            transform.position += new Vector3(1.25f, 0, 0);
            if (transform.position.x > 5)
            {
                transform.position = position;
            }
        }
    }

    // When the player meets the enemy but not at a weakpoint, so the player gets pushed back a tile.
    public void PlayerPushBack(string direction)
    {
        switch (direction)
        {
            case "w":
                transform.position += new Vector3(0, 0, -1.25f);
                break;
            case "s":
                transform.position += new Vector3(0, 0, 1.25f);
                break;
            case "a":
                transform.position += new Vector3(1.25f, 0, 0);
                break;
            case "d":
                transform.position += new Vector3(-1.25f, 0, 0);
                break;                
        }
        pushBack = false;
    }
}
