using System;
using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.UI;

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
        public GameObject[] beatMarkers;
        public Collider2D coreHitZone;
        private bool newBeats;
        [Range(0f, 500f)] public float totalTime;
        Vector3 direction;
        public float distance;
        public GameObject theCore;
        private RectTransform[] _rectTransform;
        private bool done;
        public bool threshHoldValue;
        public static bool thresholdZone;

        #endregion

        // Start is called before the first frame update
        private void Awake()
        {
            _rectTransform = new RectTransform[beatMarkers.Length];
            _rectTransform[0] = beatMarkers[0].GetComponent<RectTransform>();
            _rectTransform[1] = beatMarkers[1].GetComponent<RectTransform>();
            _rectTransform[2] = beatMarkers[2].GetComponent<RectTransform>();
            _rectTransform[3] = beatMarkers[3].GetComponent<RectTransform>();
        }

        void Start()
        {
            direction = startBar.position - endBar.position;
            distance = direction.magnitude;
        }

        // Update is called once per frame
        void Update()
        {
            threshHoldValue = thresholdZone;
            BeatBarBehaviour();
        }

        void BeatBarBehaviour()
        {
            if (!BeatManager.Instance.AreBeatsStarted)
            {
                return;
            }

            // The dots in the beat bar scroll along. 
            beatMarkers[0].transform.Translate(direction.normalized * (Time.deltaTime * (distance / totalTime)));
            beatMarkers[1].transform.Translate(direction.normalized * (Time.deltaTime * (distance / totalTime)));
            beatMarkers[2].transform.Translate(direction.normalized * (Time.deltaTime * (distance / totalTime)));
            beatMarkers[3].transform.Translate(direction.normalized * (Time.deltaTime * (distance / totalTime)));
            
            // When they reach the end they go back to the start position and repeat scrolling.
            if (_rectTransform[0].anchoredPosition.x >= 250)
            {
                _rectTransform[0].anchoredPosition = new Vector3(-250, 0f, 0);
            }

            if (_rectTransform[1].anchoredPosition.x >= 250)
            {
                _rectTransform[1].anchoredPosition = new Vector3(-250, 0f, 0);
            }

            if (_rectTransform[2].anchoredPosition.x >= 250)
            {
                _rectTransform[2].anchoredPosition = new Vector3(-250, 0f, 0);
            }

            if (_rectTransform[3].anchoredPosition.x >= 250)
            {
                _rectTransform[3].anchoredPosition = new Vector3(-250, 0f, 0);
            }
        }

        
    }
}