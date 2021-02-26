using Scripts.Enemy;
using TMPro;
using UnityEngine;

// A majority of code in this script was to help with the development process for the enemy AI, seeing the numbers for each tile and to see visually with different colour changes also.
// There is a core purpose for this script to, that takes the value of occupancy 'OccupiedNumber'. Which another script retrieves the value for it's own purposes.
namespace Floor
{
    public class TileProperties : MonoBehaviour
    {
        #region Private & Constant Variables

        [SerializeField] private Material material0;
        [SerializeField] private Material material1;
        [SerializeField] private Material material2;
        private Renderer _renderer;

        public bool turnOffDevTileValues;

        #endregion

        #region Public & Protected Variables

        public int tileWithToken;

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

        #region Private Methods

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
        }

        #endregion

        #region Public Methods
        #endregion
    }
}