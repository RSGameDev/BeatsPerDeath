using System.Collections;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Main
{
    public class ComboMetre : MonoBehaviour
    {
        #region Grading

        [SerializeField] private Scoring scoring;
        [Header("Grading Dial")] public ParticleSystem pS; // I believe this worked when i tried this out in the past. Adds a particle effect to when a new grade occurs.
        public Animator anim;
        public Slider slider;
        public float sliderStartValue = 0;
        private bool _newGrade;
        public TextMeshProUGUI text;
        [Range(0f, 80f)] public float speed;

        #endregion
        //Audio code added to communicate with AudioManager script
        public AudioManager audioManagerObj;
        private bool _isIncrementing;

        public bool perform;
        public static bool _increment;
        private bool _decrement;

        // Update is called once per frame
        void Update()
        {
            if (_increment)
            {
                Increment();
                Grading(slider.value);
            }

            if (_decrement)
            {
                Decrement();
                Grading(slider.value);
            }


        }

        void Grading(float value)
        {
            if (!(value >= 100)) return;

            _newGrade = true;
            _increment = false;
            //_decrement = false;
            perform = false;
            slider.value = 0;
            sliderStartValue = slider.value;
            switch (text.text)
            {
                case " ":
                    anim.Play("PopIn");
                    pS.gameObject.SetActive(true);
                    StartCoroutine(ParticleTermination(3));
                    text.text = "D";
                    scoring.multi1 = true;
                    audioManagerObj.MusicLayering("D");
                    // --------- AUDIO can be included here
                    return;
                case "D":
                    anim.Play("PopIn");
                    pS.gameObject.SetActive(true);
                    StartCoroutine(ParticleTermination(3));
                    text.text = "C";
                    scoring.multi2 = true;
                    audioManagerObj.MusicLayering("C");
                    // --------- AUDIO can be included here
                    return;
                case "C":
                    anim.Play("PopIn");
                    pS.gameObject.SetActive(true);
                    StartCoroutine(ParticleTermination(3));
                    text.text = "B";
                    scoring.multi3 = true;
                    audioManagerObj.MusicLayering("B");
                    // --------- AUDIO can be included here
                    return;
                case "B":
                    anim.Play("PopIn");
                    pS.gameObject.SetActive(true);
                    StartCoroutine(ParticleTermination(3));
                    text.text = "A";
                    scoring.multi4 = true;
                    audioManagerObj.MusicLayering("A");
                    // --------- AUDIO can be included here
                    return;
                case "A":
                    anim.Play("PopIn");
                    pS.gameObject.SetActive(true);
                    StartCoroutine(ParticleTermination(3));
                    text.text = "S";
                    scoring.multi5 = true;
                    audioManagerObj.MusicLayering("S");
                    // --------- AUDIO can be included here
                    return;
            }

            if (_newGrade)
            {
                _newGrade = false;
            }
        }

        IEnumerator ParticleTermination(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            pS.gameObject.SetActive(false);
            print("stop");

        }

        public void Increment()
        {
            slider.value += speed * Time.deltaTime;
            if (slider.value >= sliderStartValue + 20)
            {
                sliderStartValue = slider.value;
                perform = false;
                _increment = false;
            }
        }

        private void Decrement()
        {
            if (slider.value <= 0)
            {
                sliderStartValue = 0;
                perform = false;
                _decrement = false;
                return;
            }

            slider.value -= speed * Time.deltaTime;
            if (slider.value <= sliderStartValue - 20)
            {
                sliderStartValue = slider.value;
                perform = false;
                _decrement = false;
            }
        }
    }
}