using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
// Script attached to the player.
public class PlayerMovement : MonoBehaviour
{
    #region Private Variables
    private const float s_moveUnits = 1.25F;
    private const float s_verticalTopLimit = 6.25F;
    private const float s_verticalBottomLimit = 1.25F;
    private const float s_horizontalLeftLimit = 0F;
    private const float s_horizontalRightLimit = 5F;
    #endregion

    #region Public variables
    public GameObject BeatHitZone;

    public Anchor PlayerAnchor;
    public Player Player;
    public TileController TileController;

    public string Direction;
    public bool PushBack;
    public bool IsMoving;
    public bool IsOnBeat;
    #endregion

    #region Lerp Behaviour
    private Vector3 _destination;

    private float _startTime;
    private float _journeyLength;
    public const float s_Speed = 1.0F;
    public bool IsLerpingStarted;
    #endregion    

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (IsLerpingStarted)
        {
            SmoothMovement();
        }

        CheckBoundaries();

        if (PushBack)
        {
            PlayerPushBack(Direction);
        }        
    }
       
    private void SmoothMovement()
    {
        float distCovered = (Time.time - _startTime) * s_Speed;
        float fractionOfJourney = distCovered / _journeyLength;
        transform.position = Vector3.Lerp(transform.position, _destination, fractionOfJourney);
        if (fractionOfJourney >= 0.1f || !Player.IsPlayerAlive)
        {
            IsLerpingStarted = false;
            PlayerAnchor.PlaceInPosition();
        }
    }

    private void Movement()
    {
        // Upon input, The lerp time starts, currently this code shows the player has hit on beat (this will have to change i'm sure). The direction key has been logged to help wth push back workings.
        // According to the input the player will face the set direction. The destination is now set due to the way the player is facing and will move 1.25f (s_MoveUnits) units in that direction.
        // The lerp distance is calculated and then lerp can commence after the previous lines of code have been processed.
        //
        // IsOnBeat - this allows the combo metre to rise. So when the player moves on beat or however the design document requires, the combo metre can then rise using code like this.
        if (Input.GetKeyDown("w"))
        {
            _startTime = Time.time;
            IsOnBeat = BeatHitZone.GetComponent<CoreHitzone>().onBeat;
            Direction = "w";
            transform.LookAt(transform.position + Vector3.forward);
            _destination = new Vector3(transform.position.x, transform.position.y, transform.position.z + s_moveUnits);
            _journeyLength = Vector3.Distance(transform.position, _destination);
            IsLerpingStarted = true;
        }

        if (Input.GetKeyDown("s"))
        {
            _startTime = Time.time;
            IsOnBeat = BeatHitZone.GetComponent<CoreHitzone>().onBeat;
            Direction = "s";
            transform.LookAt(transform.position + Vector3.back);
            _destination = new Vector3(transform.position.x, transform.position.y, transform.position.z - s_moveUnits);
            _journeyLength = Vector3.Distance(transform.position, _destination);
            IsLerpingStarted = true;
        }

        if (Input.GetKeyDown("a"))
        {
            _startTime = Time.time;
            IsOnBeat = BeatHitZone.GetComponent<CoreHitzone>().onBeat;
            Direction = "a";
            transform.LookAt(transform.position + Vector3.left);
            _destination = new Vector3(transform.position.x - s_moveUnits, transform.position.y, transform.position.z);
            _journeyLength = Vector3.Distance(transform.position, _destination);
            IsLerpingStarted = true;
        }

        if (Input.GetKeyDown("d"))
        {
            _startTime = Time.time;
            IsOnBeat = BeatHitZone.GetComponent<CoreHitzone>().onBeat;
            Direction = "d";
            transform.LookAt(transform.position + Vector3.right);
            _destination = new Vector3(transform.position.x + s_moveUnits, transform.position.y, transform.position.z);
            _journeyLength = Vector3.Distance(transform.position, _destination);
            IsLerpingStarted = true;
        }
    }

    private void CheckBoundaries()
    {
        if (transform.position.z > s_verticalTopLimit)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, s_verticalTopLimit);
            IsLerpingStarted = false;
        }

        if (!TileController.IsPlatformMoving)
        {
            if (transform.position.z < s_verticalBottomLimit)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, s_verticalBottomLimit);
                IsLerpingStarted = false;
            }
        }
           
        if (transform.position.x < s_horizontalLeftLimit)
        {
            transform.position = new Vector3(s_horizontalLeftLimit, transform.position.y, transform.position.z);
            IsLerpingStarted = false;
        }

        if (transform.position.x > s_horizontalRightLimit)
        {
            transform.position = new Vector3(s_horizontalRightLimit, transform.position.y, transform.position.z);
            IsLerpingStarted = false;
        }
    }

    // When the player meets the enemy but not at a weakpoint, so the player gets pushed back a tile.
    public void PlayerPushBack(string direction)
    {
        switch (direction)
        {
            case "w":
                transform.position += new Vector3(0, 0, -s_moveUnits);
                break;
            case "s":
                transform.position += new Vector3(0, 0, s_moveUnits);
                break;
            case "a":
                transform.position += new Vector3(s_moveUnits, 0, 0);
                break;
            case "d":
                transform.position += new Vector3(-s_moveUnits, 0, 0);
                break;                
        }
        PushBack = false;
    }

    public void ResetPosition()
    {
        transform.position = Player.StartPosition;
        _destination = transform.position;
    }
}
