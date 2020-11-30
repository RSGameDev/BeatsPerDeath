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

    public bool tilesScrolling;
    
    Vector3 direction;
    public float distance;

    Vector3 tileMovement;

    // Use this for initialization
    void Start()
    {
        direction = Vector3.back;
        distance = 1.25f;
    }

    // Update is called once per frame
    void Update()
    {
        // Specified by the GDD, on the 6th beat the floor moves until it reaches the 12th beat. Seen below **.
        if (SceneController.instance.scrollBeatCount >= s_BeatToStartScroll)
        {
            tilesScrolling = true;

            foreach (GameObject gameObject in tilesArray)
            {
                tileMovement = direction * (Time.deltaTime * (distance / 3f));                
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

        if (SceneController.instance.scrollBeatCount == s_BeatToEndScroll)
        {
            tilesScrolling = false;

            SceneController.instance.scrollBeatCount = 0;
        }
    }

}
