using Managers;
using UnityEngine;

namespace UI.Main
{
    public class BeatBar : MonoBehaviour
    {
        #region Beat Bar
        [Header("Beat bar")]
        // Beatbar
        // GameObject beatBar;
        public Transform startBar;
        public Transform endBar;
        public GameObject beatMark1;
        public GameObject beatMark2;
        public GameObject beatMark3;
        public GameObject beatMark4;
        private bool newBeats;
        [Range(0f, 3f)] public float totalTime;
        Vector3 direction;
        public float distance;
        public GameObject theCore;
        private RectTransform _rectTransform;
        private RectTransform _rectTransform1;
        private RectTransform _rectTransform2;
        private RectTransform _rectTransform3;

        #endregion
    
        // Start is called before the first frame update
        private void Awake()
        {
            _rectTransform3 = beatMark4.GetComponent<RectTransform>();
            _rectTransform2 = beatMark3.GetComponent<RectTransform>();
            _rectTransform1 = beatMark2.GetComponent<RectTransform>();
            _rectTransform = beatMark1.GetComponent<RectTransform>();
        }

        void Start()
        {
            direction = startBar.position - endBar.position;
            distance = direction.magnitude;
        }

        // Update is called once per frame
        void Update()
        {
            BeatBarBehaviour();
        }
    
        void BeatBarBehaviour()
        {
            if (!BeatManager.Instance.AreBeatsStarted)
            {
                return;
            }

            // The dots in the beat bar scroll along. 
            beatMark1.transform.Translate(direction.normalized * (Time.deltaTime * (distance / totalTime)));
            beatMark2.transform.Translate(direction.normalized * (Time.deltaTime * (distance / totalTime)));
            beatMark3.transform.Translate(direction.normalized * (Time.deltaTime * (distance / totalTime)));
            beatMark4.transform.Translate(direction.normalized * (Time.deltaTime * (distance / totalTime)));

            // When they reach the end they go back to the start position and repeat scrolling.
            if (_rectTransform.anchoredPosition.x >= 350)
            {
                _rectTransform.anchoredPosition = new Vector3(-350, 0f, 0);
            }

            if (_rectTransform1.anchoredPosition.x >= 350)
            {
                _rectTransform1.anchoredPosition = new Vector3(-350, 0f, 0);
            }

            if (_rectTransform2.anchoredPosition.x >= 350)
            {
                _rectTransform2.anchoredPosition = new Vector3(-350, 0f, 0);
            }

            if (_rectTransform3.anchoredPosition.x >= 350)
            {
                _rectTransform3.anchoredPosition = new Vector3(-350, 0f, 0);
            }
        }
    }
}
