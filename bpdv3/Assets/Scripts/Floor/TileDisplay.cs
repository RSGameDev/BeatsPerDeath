using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// A majority of code in this script was to help with the development process for the enemy AI, seeing the numbers for each tile and to see visually with different colour changes also.
// There is a core purpose for this script to, that takes the value of occupancy 'OccupiedNumber'. Which another script retrieves the value for it's own purposes.
namespace Floor
{
    public class TileDisplay : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private Material material0;
        [SerializeField] private Material material1;
        [SerializeField] private Material material2;
        private Renderer _renderer;
        public bool isOccupied;
        private bool _hasResetTileValues;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }
        
        private void Update()
        {
            //if (turnOffDevTileValues)
            //{
            //    return;
            //}
//
            //if (!turnOffDevTileValues)
            //{
            //    TileColour();
            //}
//
            //if (BeatManager.Instance.BeatIndex == 1 || BeatManager.Instance.BeatIndex == 5 && !_hasResetTileValues)
            //{
            //    _hasResetTileValues = true;
            //    occupationValue = 0;
            //    hasReset = false;
            //    ClearObjectList();
            //}
//
            //if ((BeatManager.Instance.BeatIndex == 3 || BeatManager.Instance.BeatIndex == 7) && !hasReset)
            //{
            //    hasReset = true;
            //    _hasResetTileValues = false;
            //}

            TileColour();
            
            //LowestValue();
        }
        
        private void TileColour()
        {
            text.text = isOccupied.ToString();

            _renderer.material = !isOccupied ? material0 : material1;

            //switch (isOccupied)
            //{
            //    case 0:
            //        _renderer.material = material0;
            //        break;
            //    case 1:
            //        _renderer.material = material1;
            //        break;
            //    case 2:
            //        _renderer.material = material2;
            //        break;
            //}
        }
        
        public void ResetTokenOnTile()
        {
            isOccupied = false;
        }
        
        public void LowestValue()
        {
            //if (isOccupied <= 0)
            //{
            //    isOccupied = 0;
            //}
        }
    }
    
    /*public class TileProperties : MonoBehaviour
    {
        #region Private & Constant variables

        [SerializeField] private Material material0;
        [SerializeField] private Material material1;
        [SerializeField] private Material material2;
        private Renderer _renderer;

        [SerializeField] private Text text;

        public bool turnOffDevTileValues;

        #endregion

        #region Public & Protected variables

        public int tileWithToken = 0;

        #endregion

        #region Constructors

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        private void Start()
        {
            TileColour();
        }

        #endregion

        #region Private methods

        private void Update()
        {
            if (turnOffDevTileValues)
            {
                return;
            }

            if (!turnOffDevTileValues)
            {
                TileColour();
            }

            //LowestValue();
        }

        public void LowestValue()
        {
            if (tileWithToken <= 0)
            {
                tileWithToken = 0;
            }
        }

        // Dev purpose only - Visual recognition for tile occupancy
        private void TileColour()
        {
            switch (tileWithToken)
            {
                case 0:
                    _renderer.material = material0;
                    break;
                case 1:
                    _renderer.material = material1;
                    break;
                case 2:
                    _renderer.material = material2;
                    break;
            }
            text.text = tileWithToken.ToString();
        }

        #endregion

        #region Public methods
        #endregion
    }*/
}