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

        [Header("Grading Dial")] public ParticleSystem pS; // I believe this worked when i tried this out in the past. Adds a particle effect to when a new grade occurs.
        public Animator anim;
        public Slider slider;
        public float sliderStartValue = 0;
        private bool _newGrade;
        public TextMeshProUGUI text;
        [Range(0f, 80f)] public float speed;

        #endregion
        
        private bool _isIncrementing;

        public bool perform;
        private bool _increment;
        private bool _decrement;

        // Update is called once per frame
        void Update()
        {
            if (perform)
            {
                perform = false;

                if (BeatManager.Instance.IsOnBeat)
                {
                    _increment = true;
                }
                else 
                {
                    _decrement = true;
                }
            }

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
                    return;
                case "D":
                    anim.Play("PopIn");
                    pS.gameObject.SetActive(true);
                    StartCoroutine(ParticleTermination(3));
                    text.text = "C";
                    return;
                case "C":
                    anim.Play("PopIn");
                    pS.gameObject.SetActive(true);
                    StartCoroutine(ParticleTermination(3));
                    text.text = "B";
                    return;
                case "B":
                    anim.Play("PopIn");
                    pS.gameObject.SetActive(true);
                    StartCoroutine(ParticleTermination(3));
                    text.text = "A";
                    return;
                case "A":
                    anim.Play("PopIn");
                    pS.gameObject.SetActive(true);
                    StartCoroutine(ParticleTermination(3));
                    text.text = "S";
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
        
        private void Increment()
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