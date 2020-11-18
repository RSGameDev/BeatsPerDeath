using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour {

    [Header("Tile movement speed")]
    [Range(0f, 10f)]
    public float speed;

    //public GameObject Tile;
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
        //////////// because of old wwise taken out
        ///////////if (MusicScript.scrollBeatCount >= 6)
        {
            foreach (GameObject go in tilesArray)
            {
                go.transform.Translate(direction.normalized * (Time.deltaTime * (1.25f / 4.0000005f)));

                if (go.transform.position.z <= 0)
                {
                    go.GetComponent<TileProperties>().ResetValue();
                }

                if (go.transform.position.z <= -1.25)
                {
                    go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, 8.75f);
                }
            }
        }

        //////////// because of old wwise taken out
        ////////////if (MusicScript.scrollBeatCount == 12)
        {
            //////////// because of old wwise taken out
            ////////////MusicScript.scrollBeatCount = 0;
        }
    }

}
