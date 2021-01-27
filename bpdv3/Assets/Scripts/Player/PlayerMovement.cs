using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
// Script attached to the player.
public class PlayerMovement : MonoBehaviour
{
    #region Private Variables
    private const float s_moveUnits = 1.25F;
    #endregion

    #region Public variables
    public GameObject BeatHitZone;
    public GameObject PlayerDestinationGO;
    public GameObject NextMoveLocationGO;

    public Anchor PlayerAnchor;
    public Player Player;
    public PlayerDestination PlayerDestination;
    public TileController TileController;

    public string Direction;
    [Header("new")]
    public float Speed = 1.0f;
    public float Step;
    public bool PushBack;
    public bool IsMoving;
    public bool IsOnBeat;
    public bool IsStartMove = true;
    public bool IsInput;
    #endregion

    // Update is called once per frame
    void Update()
    {
        InputCapture();

        if (PlayerDestination.IsObtained)
        {
            Move();
        }            
    }

    // IsOnBeat - this gives idea how it can be used, for now keep. But will likely be moved to where is actually needed. Temp keep if that;s ok Kerem.
    //            it shows how it can be used anyways.
    private void InputCapture()
    {
        if (!IsInput)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                IsInput = true;
                PlayerDestination.IsObtained = false;
                transform.LookAt(transform.position + Vector3.forward);
                PlayerDestinationGO.GetComponent<Collider>().enabled = true;
            }            
    
            if (Input.GetKeyDown(KeyCode.S))
            {
                IsInput = true;
                PlayerDestination.IsObtained = false;
                transform.LookAt(transform.position + Vector3.back);
                PlayerDestinationGO.GetComponent<Collider>().enabled = true;
            }
    
            if (Input.GetKeyDown(KeyCode.A))
            {
                IsInput = true;
                PlayerDestination.IsObtained = false;
                transform.LookAt(transform.position + Vector3.left);
                PlayerDestinationGO.GetComponent<Collider>().enabled = true;
            }
    
            if (Input.GetKeyDown(KeyCode.D))
            {
                IsInput = true;
                PlayerDestination.IsObtained = false;
                transform.LookAt(transform.position + Vector3.right);
                PlayerDestinationGO.GetComponent<Collider>().enabled = true;
            }
        }        
    }

    private void Move()
    {    
        Step = Speed * Time.deltaTime;
        var location = new Vector3(NextMoveLocationGO.transform.position.x, NextMoveLocationGO.transform.position.y - 0.5f, NextMoveLocationGO.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, location, Step);
    
        if (Vector3.Distance(transform.position, location) < 0.01f)
        {
            transform.position = location;
            NextMoveLocationGO.GetComponentInParent<TileProperties>().OccupiedDecreased();
            IsInput = false;
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
}
