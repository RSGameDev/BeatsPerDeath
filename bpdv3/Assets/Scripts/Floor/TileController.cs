using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handles the scrolling of the floor.
public class TileController : MonoBehaviour {

    #region Private variables
    private const float s_distance = 1.25f;
    private const int s_beatToStartScroll = 6;
    private const float s_durationOfScrolling = 3f;
    private const int s_positionToResetTileValue = 0;
    private const float s_positionToEndScrolling = -1.25f;
    private const int s_beatToEndScroll = 12;
    private Vector3 _direction;
    private Vector3 _tileMovement;
    #endregion

    #region Public variables
    public GameObject[] TilesGOArray;    
    public bool IsPlatformMoving;
    #endregion

    // Use this for initialization
    void Start()
    {
        _direction = Vector3.back;
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneController.Instance.scrollBeatCount >= s_beatToStartScroll)
        {
            IsPlatformMoving = true;

            foreach (GameObject gameObject in TilesGOArray)
            {
                _tileMovement = _direction * (Time.deltaTime * (s_distance / s_durationOfScrolling));                
                gameObject.transform.Translate(_tileMovement);

                if (gameObject.transform.position.z <= s_positionToResetTileValue)
                {
                    // Once a row has passed the last row hazard point (flames/laser). It's values can reset to zero. Ready for when it scrolls back to the top of the level to be used again.
                    gameObject.GetComponent<TileProperties>().ResetValue();         
                }

                if (gameObject.transform.position.z <= s_positionToEndScrolling)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 8.75f);
                }
            }
        }

        if (SceneController.Instance.scrollBeatCount == s_beatToEndScroll)
        {
            IsPlatformMoving = false;

            SceneController.Instance.scrollBeatCount = 0;
        }
    }

}
