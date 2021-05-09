using System;
using Floor;
using UnityEngine;

// Handles the scrolling of the floor.
namespace Managers
{
    public class TileController : MonoBehaviour {

        #region Private & Constant variables

        [SerializeField] private GameObject[] _tilesArray;
        private OnTile[] _onTileScript;
        private const float s_Distance = 1.25f;
        private const float s_Time = 3f;
        private const int s_SpawningRow = 7;
        private const float s_DeathRow = 0.5f;
        private const int s_ResetTileOccupantValue = 0;
        private const float s_RepositionLastRowToTheStart = -1.25f;
        private Vector3 _directionOfMovement;
        private float _distance;
        private bool _isTilesMoving;

        public bool turnOffDevTileValues;
        
        //[SerializeField] private GameObject[] _tilesOutsideArray;
        //private OnTile[] _onTileOutsideScript;
        
        [SerializeField] private BoxCollider[] _collidersArray;
        
        #endregion

        #region Public & Protected variables
        #endregion

        #region Constructors

        private void Awake()
        {
            InitScriptsOnLists();
            InitCollidersOnTiles();
        }

        
        private void Start()
        {
            _directionOfMovement = Vector3.back;
            
            SetBeatListeners();
            DevelopmentTestingFeature();
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
                var tileMovement = _directionOfMovement.normalized * (Time.deltaTime * (s_Distance / s_Time));
                tile.transform.Translate(tileMovement);

                //if (tile.transform.position.z >= s_SpawningRow || tile.transform.position.z <= s_DeathRow)
                //{
                //    var index = Array.IndexOf(_tilesArray,tile);
                //    _collidersArray[index].enabled = false;
                //}
                //else
                //{
                //    var index = Array.IndexOf(_tilesArray,tile);
                //    _collidersArray[index].enabled = true;
                //}
                
                // ************ consider including again
                //if (tile.transform.position.z <= s_ResetTileOccupantValue)
                //{
                //    var index = Array.IndexOf(_tilesArray,tile);
                //    // Once a row has passed the last row hazard point (flames/laser). It's values can reset to zero.
                //    // Ready for when it scrolls back to the top of the level to be used again.
                //    _onTileScript[index].ResetTokenOnTile();
                //}

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

        private void DevelopmentTestingFeature()
        {
            if (turnOffDevTileValues)
            {
                foreach (var tile in _onTileScript)
                {
                    //tile.GetComponentInParent<TileProperties>().turnOffDevTileValues = true; ******* consider including again
                }

                //foreach (var tile in _onTileOutsideScript)
                //{
                //    tile.GetComponentInParent<TileProperties>().turnOffDevTileValues = true;
                //}
            }
        }

        private void InitScriptsOnLists()
        {
            _onTileScript = new OnTile[_tilesArray.Length];
            for (var i = 0; i < _tilesArray.Length; i++)
            {
                _onTileScript[i] = _tilesArray[i].transform.GetChild(0).GetComponentInChildren<OnTile>();
            }

            //_onTileOutsideScript = new OnTile[_tilesOutsideArray.Length];
            //for (var i = 0; i < _tilesOutsideArray.Length; i++)
            //{
            //    _onTileOutsideScript[i] = _tilesOutsideArray[i].transform.GetChild(0).GetComponentInChildren<OnTile>();
            //}
        }
        
        private void InitCollidersOnTiles()
        {
            _collidersArray = new BoxCollider[_onTileScript.Length];
            for (var i = 0; i < _onTileScript.Length; i++)
            {
                _collidersArray[i] = _onTileScript[i].gameObject.GetComponent<BoxCollider>();
            }
        }

        //public void TilePermissionCheck()
        //{
        //    foreach (var tile in _tilesArray)
        //    {
        //        tile.transform.GetChild(0).GetComponentInChildren<OnTile>().PermissionToMove();
        //        tile.transform.GetChild(0).GetComponentInChildren<OnTile>().ContinueOrHold();
        //    }
        //}
        
        
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
