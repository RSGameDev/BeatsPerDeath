using TMPro;
using UnityEngine;

namespace UI.Main
{
    public class Scoring : MonoBehaviour
    {
        #region Private & Constant variables
    
        [Header("Score")]
        private const int _points = 100;
        private float _score = 0;
        [SerializeField] private TextMeshProUGUI scoreUiValue;
    
        #endregion
    
        #region Public & Protected variables
    
        [Header("Multiplier")]
        [SerializeField] private TextMeshProUGUI multiplierText;
        private float _multiplierStatus = 1f;
    
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
            if (Input.GetKeyDown(KeyCode.Y))
            {
                _score += _points * _multiplierStatus;
            }

            if (_score >= 500)
            {
                MultiplierStatus(1);
            }
        
            if (_score >= 1000)
            {
                MultiplierStatus(2);
            }
        
            if (_score >= 1500)
            {
                MultiplierStatus(3);
            }
        
            if (_score >= 2500)
            {
                MultiplierStatus(4);
            }
        
            if (_score >= 4000)
            {
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
        #endregion
    
    }
}
