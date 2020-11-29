namespace Assets.Scripts.Managers 
{
    using TMPro;
    using UnityEngine;

    public class TestUIManager : MonoBehaviour
    {
        #region Private & Const Variables

        private float _timer;

        #endregion

        #region Public & Protected Variables

        public TextMeshProUGUI TimerUiElement;

        #endregion

        #region Constructors

        #endregion

        #region Private Methods

        private void Update()
        {
            IncrementTimer();
        }

        private void IncrementTimer()
        {
            _timer += Time.deltaTime;

            if (TimerUiElement?.text == null) 
            {
                return;
            }

            TimerUiElement.text = _timer.ToString("00");
        }

        #endregion

        #region Public & Protected Methods      

        #endregion
    }
}

