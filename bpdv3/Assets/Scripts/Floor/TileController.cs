using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the scrolling of the floor.
public class TileController : MonoBehaviour {

    [Header("Tile movement speed")]
    [Range(0f, 10f)]
    public float speed;
    const int s_BeatToStartScroll = 6;
    const int s_PositionToResetTileValue = 0;
    const float s_PositionToEndScrolling = -1.25f;
    const int s_BeatToEndScroll = 12;

    public GameObject[] tilesArray;
    public GameObject player;
    Vector3 direction;
    public float distance;

    public bool _isMoving = false;

    private void Start()
    {
        direction = Vector3.back;
        distance = direction.magnitude;
        SetBeatListeners();
    }

    private void Update()
    {
        if (!_isMoving)
        {
            return;
        }

        foreach (GameObject gameObject in tilesArray)
        {
            var tileMovement = direction.normalized * (Time.deltaTime * (1.25f / 4f));
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
        _isMoving = true;
    }

    private void EndMovement() 
    {
        _isMoving = false;
    }

}
