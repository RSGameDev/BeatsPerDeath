using TMPro;
using UnityEngine;

namespace UI.Main
{
    public class Scoring : MonoBehaviour
    {
        #region Private & Constant variables
    
        [Header("Score")]
        private const int _points = 100;
        private static float _score = 0;
        [SerializeField] private TextMeshProUGUI scoreUiValue;
    
        #endregion
    
        #region Public & Protected variables
    
        [Header("Multiplier")]
        [SerializeField] private TextMeshProUGUI multiplierText;
        private static float _multiplierStatus = 1f;
        public bool multi1;
        public bool multi2;
        public bool multi3;
        public bool multi4;
        public bool multi5;
        
        #endregion

        #region Constructors

        private void Start()
        {
            scoreUiValue.text = _score.ToString();
        }

        #endregion

        #region Private methods

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Y))
            //{
            //    _score += _points * _multiplierStatus;
            //}

            if (multi1)
            {
                MultiplierStatus(1);
            }
        
            if (multi2)
            {
                multi1 = false;
                MultiplierStatus(2);
            }
        
            if (multi3)
            {
                multi2 = false;
                MultiplierStatus(3);
            }
        
            if (multi4)
            {
                multi3 = false;
                MultiplierStatus(4);
            }
        
            if (multi5)
            {
                multi4 = false;
                MultiplierStatus(5);
            }
        
            ScoreUpdate();
        }

        private void ScoreUpdate()
        {
            scoreUiValue.text = _score.ToString();
        }

        private void MultiplierStatus(int multiplier)
        {
            switch (multiplier)
            {
                case 1:
                    _multiplierStatus = 1.2f;
                    multiplierText.text = "x 1.2";
                    break;
                case 2:
                    _multiplierStatus = 1.6f;
                    multiplierText.text = "x 1.6";
                    break;
                case 3:
                    _multiplierStatus = 2.2f;
                    multiplierText.text = "x 2.2";
                    break;
                case 4:
                    _multiplierStatus = 3.0f;
                    multiplierText.text = "x 3.0";
                    break;
                case 5:
                    _multiplierStatus = 4.0f;
                    multiplierText.text = "x 4.0";
                    break;
            }
        }

        #endregion

        #region Public methods

        public static void ScorePoints()
        {
            _score += _points * _multiplierStatus;
        }
        
        #endregion
    
    }
}
