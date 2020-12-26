using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestination : MonoBehaviour
{
    #region Private variables
    const string s_nextMoveLayer = "OnTile";
    const string s_noGoMoveLayer = "AreaLimit";

    private const float s_verticalTopLimit = 7.5F;
    private const float s_verticalBottomLimit = 0F;
    #endregion

    #region Public variables
    public Anchor AnchorPlayer;
    public PlayerMovement PlayerMovement;
    public Collider VacantDestination;

    public bool IsObtained;
    public bool IsOutOfBounds;
    #endregion

    private void Awake()
    {
        gameObject.GetComponent<Collider>().enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        VacantDestination = other;

        if (other.gameObject.layer == LayerMask.NameToLayer(s_nextMoveLayer))
        {
            if (!IsObtained)
            {
                PlayerMovement.NextMoveLocationGO = other.gameObject;
                CheckBoundaries(other);
                gameObject.GetComponent<Collider>().enabled = false;
            }
        }
        // This is for the left and right side of the level, when the object detects there is no tile to move onto, the following code executes.
        else if (other.gameObject.layer == LayerMask.NameToLayer(s_noGoMoveLayer))
        {
            gameObject.GetComponent<Collider>().enabled = false;
            PlayerMovement.IsInput = false;
        }
    }

    private void CheckBoundaries(Collider other)
    {
        if (transform.position.z >= s_verticalTopLimit || transform.position.z <= s_verticalBottomLimit)
        {
            IsObtained = false;
            PlayerMovement.IsInput = false;
            other.gameObject.GetComponentInParent<TileProperties>().OccupiedDecreased();
            IsOutOfBounds = true;
        }
        else
        {
            IsObtained = true;
        }
    }

    private void Update()
    {
        PositionContraint(VacantDestination);
    }

    private void PositionContraint(Collider other)
    {
        if (transform.position.z >= s_verticalTopLimit || transform.position.z <= s_verticalBottomLimit)
        {
            PlayerMovement.IsInput = false;
            IsObtained = false;
            other.gameObject.GetComponentInParent<TileProperties>().OccupiedDecreased();
            IsOutOfBounds = true;
        }
    }
}
    

