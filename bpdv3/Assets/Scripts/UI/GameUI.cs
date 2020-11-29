using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    #region Score
    // Add score code
    [Header("Score")]
    public int score = 0;
    public TextMeshProUGUI scoreUiValue;
    #endregion

    #region Life
    [Header("Life Count")]
    public Image[] lifeCount;
    #endregion

    #region Grading
    [Header("Grading Dial")]
    //public GameObject dial;
    //public ParticleSystem pS;
    public Animator anim;
    public Slider slider;
    public float sliderStartValue = 0;    
    bool testLose;
    bool newGrade;
    public TextMeshProUGUI text;
    [Range(0f, 30f)] public float speed;
    #endregion

    #region Beat Bar
    [Header("Beat bar")]
    // Beatbar
    public GameObject beatBar;
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
    #endregion

    #region Multiplier
    [Header("Multiplier")]
    public GameObject multiplierGO;
    #endregion



    public GameObject player;

    PlayerMovement playerMoveScript;    

    // Start is called before the first frame update
    void Start()
    {
        scoreUiValue.text = score.ToString();

        playerMoveScript = player.GetComponent<PlayerMovement>();

        direction = startBar.position - endBar.position;
        distance = direction.magnitude;
    }

    void Update()
    {
        // todo Score in update

        #region lifecount
        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    Debug.Log("loselife3rd");
        //    PlayerLoseLife(3);
        //}

        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerLoseLife(2);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerLoseLife(1);
        }
        #endregion

        Grading();                    

        BeatBarBehaviour();

        // todo this code may not be needed, keep for now
        #region MultiplierStatus
        if (Input.GetKeyDown(KeyCode.H))
        {
            MultiplerStatus(1);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            MultiplerStatus(2);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            MultiplerStatus(3);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            MultiplerStatus(4);
        }
        #endregion

    }

    // todo Score function ////////////////////////////////////////////
    public void Scoring(int value)
    {
        score += value;
        scoreUiValue.text = score.ToString();
    }

    public void PlayerLoseLife(int lifecount)
    {
        //switch (playerScript.lifeCount)
        switch (lifecount)
        {
            case 2:
                lifeCount[2].GetComponent<Image>().color = new Color32(0, 0, 0, 100);
                break;
            case 1:
                lifeCount[1].GetComponent<Image>().color = new Color32(0, 0, 0, 100);
                break;
            case 0:
                lifeCount[0].GetComponent<Image>().color = new Color32(0, 0, 0, 100);
                break;
                //default:
        }
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
    
    void BeatBarBehaviour()
    {
        //beatMark1.GetComponent<RectTransform>().anchoredPosition
        if (SceneController.Instance.beatStarted)
        {
            beatMark1.transform.Translate(direction.normalized * (Time.deltaTime * (distance / totalTime)));  
            beatMark2.transform.Translate(direction.normalized * (Time.deltaTime * (distance / totalTime)));  
            beatMark3.transform.Translate(direction.normalized * (Time.deltaTime * (distance / totalTime)));  
            beatMark4.transform.Translate(direction.normalized * (Time.deltaTime * (distance / totalTime)));  
                                                                                                              
                                                                                                              
        }
        
        if (beatMark1.GetComponent<RectTransform>().anchoredPosition.x >= 230)
        {
            beatMark1.GetComponent<RectTransform>().anchoredPosition = new Vector3(-230, 0.5f, 0);
        }

        if (beatMark2.GetComponent<RectTransform>().anchoredPosition.x >= 230)
        {
            beatMark2.GetComponent<RectTransform>().anchoredPosition = new Vector3(-230, 0.5f, 0);
        }
        
        if (beatMark3.GetComponent<RectTransform>().anchoredPosition.x >= 230)
        {
            beatMark3.GetComponent<RectTransform>().anchoredPosition = new Vector3(-230, 0.5f, 0);
        }
        
        if (beatMark4.GetComponent<RectTransform>().anchoredPosition.x >= 230)
        {
            beatMark4.GetComponent<RectTransform>().anchoredPosition = new Vector3(-230, 0.5f, 0);
        }
    }

    // todo keep for time being, deletion possibly
    void MultiplerStatus(int multiplier)
    {
        switch (multiplier)
        {
            case 1:
                multiplierGO.GetComponentInChildren<TextMeshProUGUI>().text = "Multiplier x2";
                break;
            case 2:
                multiplierGO.GetComponentInChildren<TextMeshProUGUI>().text = "Multiplier x4";
                break;
            case 3:
                multiplierGO.GetComponentInChildren<TextMeshProUGUI>().text = "Multiplier x6";
                break;
            case 4:
                multiplierGO.GetComponentInChildren<TextMeshProUGUI>().text = "Multiplier x8";
                break;
                //default:
        }
    }     

    private void Grading()
    {
        if (playerMoveScript.isMoving)
        {
            if (!playerMoveScript.onBeat)
            {
                slider.value -= speed * Time.deltaTime;
                if (slider.value <= sliderStartValue - 20)
                {
                    //playerScript.hitBeat = false;
                    playerMoveScript.isMoving = false;
                    sliderStartValue = slider.value;
                    //testLose = false;
                }
            }
    
            // Hit on the beat 
            if (playerMoveScript.onBeat)
            {
                slider.value += speed * Time.deltaTime;
                if (slider.value >= sliderStartValue + 20)
                {
                    playerMoveScript.onBeat = false;
                    playerMoveScript.isMoving = false;
                    sliderStartValue = slider.value;
                }
            }
        }
    
        // Miss a beat
        if (Input.GetKeyDown(KeyCode.Y))
        {
            testLose = true;
        }
    
        Grading(slider.value);
    
        if (newGrade)
        {
            //playerMoveScript.hitBeat = false;
            newGrade = false;
        }
    }
}

