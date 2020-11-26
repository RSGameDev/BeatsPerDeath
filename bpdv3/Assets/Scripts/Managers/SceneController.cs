using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using TMPro;

// Script for handling changes of scenes, panels etc.
// Note - Recently the music was added to the game scene. At the bottom of this script is a 'CallBackBeatFunction' that we are looking to have in another script 'a music script'.
//        So the code at the bottom of this script is here temporarily and will be moved. This was done just so we can get a build out early for people to see. 
public class SceneController : MonoBehaviour
{
    public AK.Wwise.Event menuEvent;
    public AK.Wwise.Event gplayEvent;

    public GameObject splashScreen;
    public GameObject warningScreen;
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject gameplayMenu;
    public GameObject soundsMenu;
    public GameObject creditsScreen;


    [SerializeField] int splashDelay = 4;
    [SerializeField] int warningDelay = 3;
    int currentSceneIndex;

    public TextMeshProUGUI timeUiValue;
    public float timer;
    public TextMeshProUGUI beatUiValue;
    public TextMeshProUGUI spawnUiValue;
    bool isReferenced;

    public float beatStartTime;
    public float currentTime;
    public float beatDurationTime;
    public bool timeCheck;
    bool isDone;
    public bool beatStarted;
    
    public int gameBeatCount;                    // This variable allows the enemies to move correctly.
    public int spawnBeatCount;                   // This variable allows the enemies to spawn correctly.
    public int scrollBeatCount;                 

    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        warningScreen.transform.localScale = Vector3.zero;
        mainMenu.transform.localScale = Vector3.zero;
        optionsMenu.transform.localScale = Vector3.zero;
        gameplayMenu.transform.localScale = Vector3.zero;
        soundsMenu.transform.localScale = Vector3.zero;
        creditsScreen.transform.localScale = Vector3.zero;
    }

    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            menuEvent.Post(gameObject);
            StartCoroutine(DelayOnSplashScreen());
        }
    }

    IEnumerator DelayOnSplashScreen()
    {
        yield return new WaitForSeconds(splashDelay);
        splashScreen.transform.localScale = Vector3.zero;
        LoadWarningScreen();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && !isReferenced)
        {
            isReferenced = true;
            var temp = GameObject.Find("BeatUITestValue (TMP)");
            beatUiValue = temp.GetComponent<TextMeshProUGUI>();

            var temp1 = GameObject.Find("ScoreUITest Value (TMP) (1)");
            spawnUiValue = temp1.GetComponent<TextMeshProUGUI>();

            var temp2 = GameObject.Find("TimeTest Value (TMP) (2)");
            timeUiValue = temp2.GetComponent<TextMeshProUGUI>();
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            timer += Time.deltaTime; 
            timeUiValue.text = timer.ToString("00");
        }
    }

    private void LoadWarningScreen()
    {
        warningScreen.transform.localScale = Vector3.one;
        StartCoroutine(DelayOnWarningScreen());
    }

    IEnumerator DelayOnWarningScreen()
    {
        yield return new WaitForSeconds(warningDelay);
        warningScreen.transform.localScale = Vector3.zero;
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        mainMenu.transform.localScale = Vector3.one;
        optionsMenu.transform.localScale = Vector3.zero;
    }

    public void StartGame()
    {
        menuEvent.Stop(gameObject);

        //AkSoundEngine.SetState("Music_State", "Gameplay");
        //AkSoundEngine.SetSwitch("Level", "Main_Level", MusicObject);
        SceneManager.LoadScene("scene1");
        gplayEvent.Post(gameObject, (uint)AkCallbackType.AK_MusicSyncBeat, CallBackBeatFunction);
    }


    public void LoadOptionsScreen()
    {
        optionsMenu.transform.localScale = Vector3.one;
        gameplayMenu.transform.localScale = Vector3.one;
        mainMenu.transform.localScale = Vector3.zero;
    }

    public void LoadGameplayScreen()
    {
        gameplayMenu.transform.localScale = Vector3.one;
        soundsMenu.transform.localScale = Vector3.zero;
        creditsScreen.transform.localScale = Vector3.zero;
    }

    public void LoadSoundsScreen()
    {
        gameplayMenu.transform.localScale = Vector3.zero;
        soundsMenu.transform.localScale = Vector3.one;
        creditsScreen.transform.localScale = Vector3.zero;
    }

    public void LoadCreditsScreen()
    {
        gameplayMenu.transform.localScale = Vector3.zero;
        soundsMenu.transform.localScale = Vector3.zero;
        creditsScreen.transform.localScale = Vector3.one;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    //AudioEngine Code to give us information on beat detection from WWise. < John comment
    void CallBackBeatFunction(object in_cookie, AkCallbackType in_type, object in_info)         // The code i mentioned at the top of the script. We will look to have this in a different script
                                                                                                // to SceneController as the functioning behind it relates heavily on music and so we shall look to
                                                                                                // have this in a music script in the future.  < Richard comment
    {
        gameBeatCount++;            
        spawnBeatCount++;           

        if (gameBeatCount == 5)
        {
            gameBeatCount = 1;
        }

        if (spawnBeatCount == 9)
        {
            spawnBeatCount = 1;
        }        

        beatUiValue.text = gameBeatCount.ToString();                // For development purposes. To see on the screen the values of each.                
        spawnUiValue.text = spawnBeatCount.ToString();              // For development purposes. To see on the screen the values of each.

        scrollBeatCount++;

        // This triggers the beat bar in the GameUi to start working.
        if (!beatStarted)
        {
            beatStarted = true;
        }              

        Debug.Log("Beat detected");
    }

    //public void LoadGameOver()
    //{
    //    SceneManager.LoadScene("Game Over");
    //}
}
