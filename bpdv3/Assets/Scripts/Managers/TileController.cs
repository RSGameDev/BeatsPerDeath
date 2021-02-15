using System;
using Floor;
using UnityEngine;

// Handles the scrolling of the floor.
namespace Managers
{
    public class TileController : MonoBehaviour {

        #region Private & Constant variables
        
        [SerializeField] private GameObject[] _tilesArray;
        private TileProperties[] _tileScript;
        private TileProperties _tileProperties;
        private const int s_ResetTileOccupantValue = 0;
        private const float s_RepositionLastRowToTheStart = -1.25f;
        private Vector3 _directionOfMovement;
        private float _distance;
        private bool _isTilesMoving;
    
        #endregion

        #region Public & Protected variables
        #endregion

        #region Constructors
    
        private void Start()
        {
            _tileScript = new TileProperties[_tilesArray.Length];
            for (var i = 0; i < _tilesArray.Length; i++)
            {
                _tileScript[i] = _tilesArray[i].GetComponent<TileProperties>();
            }
            
            _directionOfMovement = Vector3.back;
            
            SetBeatListeners();
        }
    
        #endregion

        #region Private Methods

        private void Update()
        {
            if (!_isTilesMoving)
            {
                return;
            }

            foreach (var tile in _tilesArray)
            {
                var tileMovement = _directionOfMovement.normalized * (Time.deltaTime * (1.25f / 3f));
                tile.transform.Translate(tileMovement);

                if (tile.transform.position.z <= s_ResetTileOccupantValue)
                {
                    var index = Array.IndexOf(_tilesArray,tile);
                    // Once a row has passed the last row hazard point (flames/laser). It's values can reset to zero.
                    // Ready for when it scrolls back to the top of the level to be used again.
                    _tileScript[index].ResetValue();
                }

                if (tile.transform.position.z <= s_RepositionLastRowToTheStart)
                {
                    var position = tile.transform.position;
                    position = new Vector3(position.x, position.y, 8.75f);
                    tile.transform.position = position;
                }
            }
        }

        private void SetBeatListeners()
        {
            BeatManager.Instance.AddListener(5, FloorMoves);
            BeatManager.Instance.AddListener(11, FloorStops);
        }

        private void FloorMoves() 
        {
            _isTilesMoving = true;
        }

        private void FloorStops() 
        {
            _isTilesMoving = false;
        }

        #endregion

        #region Public Methods
        #endregion
    }
}
