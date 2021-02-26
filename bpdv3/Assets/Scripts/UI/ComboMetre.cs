using System.Collections;
using System.Collections.Generic;
using Managers;
using Scripts.Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboMetre : MonoBehaviour
{
    #region Grading
    [Header("Grading Dial")]
    public ParticleSystem pS;             // I believe this worked when i tried this out in the past. Adds a particle effect to when a new grade occurs.
    public Animator anim;
    public Slider slider;
    public float sliderStartValue = 0;    
    bool newGrade;
    public TextMeshProUGUI text;
    [Range(0f, 30f)] public float speed;
    #endregion
    
    PlayerMovement playerMoveScript;

    public GameObject player;
    
    public GameObject beatPanelTest;
    public GameObject tileController;
    
    // Start is called before the first frame update
    void Start()
    {
        playerMoveScript = player.GetComponent<PlayerMovement>();

        if (tileController.GetComponent<TileController>().turnOffDevTileValues)
        {
            beatPanelTest.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ComboMetre();   
    }
    
    void Grading(float value)
    {
        if (value >= 100)
        {
            newGrade = true;
            slider.value = 0;
            sliderStartValue = slider.value;
            if (text.text == "D")
            {
                anim.Play("PopIn");
                //pS.Play();
                text.text = "C";
                return;
            }

            if (text.text == "C")
            {
                anim.Play("PopIn");
                //pS.Play();
                text.text = "B";
                return;
            }

            if (text.text == "B")
            {
                anim.Play("PopIn");
                //pS.Play();
                text.text = "A";
                return;
            }
        }
    }
    
    //TEMP out of action - Richard working on
    //private void ComboMetre()
    //{
    //    if (playerMoveScript.IsMoving)
    //    {
    //        if (!playerMoveScript.IsOnBeat)
    //        {
    //            slider.value -= speed * Time.deltaTime;
    //            if (slider.value <= sliderStartValue - 20)
    //            {
    //                playerMoveScript.IsMoving = false;
    //                sliderStartValue = slider.value;
    //            }
    //        }

    //        // Hit on the beat 
    //        if (playerMoveScript.IsOnBeat)
    //        {
    //            slider.value += speed * Time.deltaTime;
    //            if (slider.value >= sliderStartValue + 20)
    //            {
    //                playerMoveScript.IsOnBeat = false;
    //                playerMoveScript.IsMoving = false;
    //                sliderStartValue = slider.value;
    //            }
    //        }
    //    }        

    //    Grading(slider.value);

    //    if (newGrade)
    //    {
    //        newGrade = false;
    //    }
    //}
}
