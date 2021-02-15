using TMPro;
using UnityEngine;

// A majority of code in this script was to help with the development process for the enemy AI, seeing the numbers for each tile and to see visually with different colour changes also.
// There is a core purpose for this script to, that takes the value of occupancy 'OccupiedNumber'. Which another script retrieves the value for it's own purposes.
namespace Floor
{
    public class TileProperties : MonoBehaviour
    {
        #region Private & Constant Variables
    
        [SerializeField] private TextMeshPro _occupancyValue;
        [SerializeField] private Material material0;
        [SerializeField] private Material material1;
        [SerializeField] private Material material2;
        [SerializeField] private Material material3;
        private Renderer _renderer;

        #endregion

        #region Public & Protected Variables

        public bool HasEnemy { get; set; }
        public int OccupiedNumber { get; private set; } = 0;
    
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
            _occupancyValue.text = OccupiedNumber.ToString(); // Dev purpose only - to see the value for each tile, occupancy status.
            TileColour();
        }

        // Dev purpose only - Visual recognition for tile occupancy
        private void TileColour()
        {
            switch (OccupiedNumber)
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
                case 3:
                    _renderer.material = material3;
                    break;
            }
        }

        #endregion

        #region Public Methods

        public void OccupiedIncreased()
        {
            OccupiedNumber++;
        }

        public void OccupiedDecreased()
        {
            OccupiedNumber--;
            if (OccupiedNumber < 0)
            {
                OccupiedNumber = 0;
            }
        }

        // Once a row has passed the last row hazard point (flames/laser). It's values can reset to zero. Ready for when it scrolls back to the top of the level to be used again.
        public void ResetValue()
        {
            OccupiedNumber = 0;
        }

        #endregion
    
    

    

    
    }
}
