using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script attached to the player.
public class PlayerMovement : MonoBehaviour
{
    #region Public variables
    public GameObject hitZone;

    public Anchor _anchor;
    public Player _player;
    public TileController _tileController;

    public string direction;

    public bool pushBack;
    public bool isMoving;
    public bool onBeat;
    #endregion

    #region Function
    // Lerp    
    private Vector3 destination;

    private float startTime;
    private float journeyLength;
    public float speed = 1.0F;

    public bool lerp;
    #endregion    

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (lerp)
        {
            Lerp();
        }

        CheckBoundaries();

        if (pushBack)
        {
            PlayerPushBack(direction);
        }        
    }

    private void Lerp()
    {
        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(transform.position, destination, fractionOfJourney);
        if (fractionOfJourney >= 0.1f || !_player.isAlive)
        {
            lerp = false;
            _anchor.PlaceInPosition();
        }
    }

    void Movement()
    {
        if (Input.GetKeyDown("w"))
        {
            startTime = Time.time;
            onBeat = hitZone.GetComponent<CoreHitzone>().onBeat;
            direction = "w";
            transform.LookAt(transform.position + Vector3.forward);
            destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.25f);
            journeyLength = Vector3.Distance(transform.position, destination);
            lerp = true;
        }

        if (Input.GetKeyDown("s"))
        {
            startTime = Time.time;
            onBeat = hitZone.GetComponent<CoreHitzone>().onBeat;
            direction = "s";
            transform.LookAt(transform.position + Vector3.back);
            destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1.25f);
            journeyLength = Vector3.Distance(transform.position, destination);
            lerp = true;
        }

        if (Input.GetKeyDown("a"))
        {
            startTime = Time.time;
            onBeat = hitZone.GetComponent<CoreHitzone>().onBeat;
            direction = "a";
            transform.LookAt(transform.position + Vector3.left);
            destination = new Vector3(transform.position.x - 1.25f, transform.position.y, transform.position.z);
            journeyLength = Vector3.Distance(transform.position, destination);
            lerp = true;
        }

        if (Input.GetKeyDown("d"))
        {
            startTime = Time.time;
            onBeat = hitZone.GetComponent<CoreHitzone>().onBeat;
            direction = "d";
            transform.LookAt(transform.position + Vector3.right);
            destination = new Vector3(transform.position.x + 1.25f, transform.position.y, transform.position.z);
            journeyLength = Vector3.Distance(transform.position, destination);
            lerp = true;
        }
    }

    void CheckBoundaries()
    {
        if (transform.position.z > 6.25)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 6.25f);
            lerp = false;
        }

        if (!_tileController.tilesScrolling)
        {
            if (transform.position.z < 1.25)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 1.25f);
                lerp = false;
            }
        }
           
        if (transform.position.x < 0)
        {
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
            lerp = false;
        }

        if (transform.position.x > 5)
        {
            transform.position = new Vector3(5, transform.position.y, transform.position.z);
            lerp = false;
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

    public void ResetPosition()
    {
        transform.position = _player.startPos;
        destination = transform.position;
    }
}
