using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the scrolling of the floor.
public class TileController : MonoBehaviour {

    [Header("Tile movement speed")]
    [Range(0f, 10f)]
    public float speed;

    public GameObject[] tilesArray;
    public GameObject player;

    Vector3 direction;
    public float distance;

    // Use this for initialization
    void Start()
    {
        direction = Vector3.back;
        distance = direction.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        // Specified by the GDD, on the 6th beat the floor moves until it reaches the 12th beat. Seen below **.
        if (SceneController.instance.scrollBeatCount >= 6)
        {
            foreach (GameObject go in tilesArray)
            {
                go.transform.Translate(direction.normalized * (Time.deltaTime * (1.25f / 4f)));

                if (go.transform.position.z <= 0)
                {
                    go.GetComponent<TileProperties>().ResetValue();         // Once a row has passed the last row hazard point (flames/laser). It's values can reset to zero.
                                                                            // Ready for when it scrolls back to the top of the level to be used again.
                }

                if (go.transform.position.z <= -1.25)
                {
                    go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, 8.75f);
                }
            }
        }

        // ** The 12th beat.
        if (SceneController.instance.scrollBeatCount == 12)
        {
            SceneController.instance.scrollBeatCount = 0;
        }
    }

}
