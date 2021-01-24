using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the scrolling of the floor.
public class TileController : MonoBehaviour {

    #region Private variables    
    private Vector3 _direction;

    const int s_BeatToStartScroll = 6;
    const int s_PositionToResetTileValue = 0;
    const float s_PositionToEndScrolling = -1.25f;
    const int s_BeatToEndScroll = 12;    
    #endregion

    #region Public variables
    public GameObject[] TilesArray;

    [Header("Tile movement speed")]
    [Range(0f, 10f)]
    public float Speed; 
    public float Distance;
    public bool IsMoving = false;
    #endregion

    private void Start()
    {
        _direction = Vector3.back;
        Distance = _direction.magnitude;
        SetBeatListeners();
    }

    private void Update()
    {
        if (!IsMoving)
        {
            return;
        }

        foreach (GameObject gameObject in TilesArray)
        {
            var tileMovement = _direction.normalized * (Time.deltaTime * (1.25f / 3f));
            gameObject.transform.Translate(tileMovement);

            if (gameObject.transform.position.z <= s_PositionToResetTileValue)
            {
                // Once a row has passed the last row hazard point (flames/laser). It's values can reset to zero. Ready for when it scrolls back to the top of the level to be used again.
                gameObject.GetComponent<TileProperties>().ResetValue();
            }

            if (gameObject.transform.position.z <= s_PositionToEndScrolling)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 8.75f);
            }
        }
    }

    private void SetBeatListeners()
    {
        BeatManager.Instance.AddListener(5, StartMovement);
        BeatManager.Instance.AddListener(11, EndMovement);
    }

    private void StartMovement() 
    {
        IsMoving = true;
    }

    private void EndMovement() 
    {
        IsMoving = false;
    }

}
