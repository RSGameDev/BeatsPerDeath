using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Assets.Scripts.Core;

public class SceneController : Singleton<SceneController>
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

    public TextMeshProUGUI beatUiValue;
    public TextMeshProUGUI spawnUiValue;
    bool isReferenced;

    public float beatStartTime;
    public float currentTime;
    public float beatDurationTime;
    public bool timeCheck;
    bool isDone;
    public bool beatStarted;
    
    public int gameBeatCount;
    public int spawnBeatCount;
    public int scrollBeatCount;

    private new void Awake()
    {
        base.Awake();

        warningScreen.transform.localScale = Vector3.zero;
        mainMenu.transform.localScale = Vector3.zero;
        optionsMenu.transform.localScale = Vector3.zero;
        gameplayMenu.transform.localScale = Vector3.zero;
        soundsMenu.transform.localScale = Vector3.zero;
        creditsScreen.transform.localScale = Vector3.zero;
    }

    void Start()
    {
        SetSceneSettings();
    }

    private void SetSceneSettings()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        switch (currentSceneIndex)
        {
            case 0:
                SetIntroSceneSettings();
                break;
            case 1:
                SetGamePlaySceneSettings();
                break;
        }
    }

    // TODO temporary method will be deleted with the implenentation of the AudioManager
    private void SetGamePlaySceneSettings()
    {
        gplayEvent.Post(gameObject, (uint)AkCallbackType.AK_MusicSyncBeat, CallBackBeatFunction);
    }

    private void SetIntroSceneSettings()
    {
        menuEvent.Post(gameObject);
        StartCoroutine(DelayOnSplashScreen());
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
        SetGamePlaySceneSettings();
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

    //AudioEngine Code to give us information on beat detection from WWise.
    void CallBackBeatFunction(object in_cookie, AkCallbackType in_type, object in_info)
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

        beatUiValue.text = gameBeatCount.ToString();
        spawnUiValue.text = spawnBeatCount.ToString();

        scrollBeatCount++;

        if (!beatStarted)
        {
            //beatStartTime = Time.timeSinceLevelLoad;
            beatStarted = true;
        }
        
        //if (!timeCheck)
        //{
        //    currentTime = Time.time;
        //    timeCheck = true;
        //}
        //else if (timeCheck && !isDone)
        //{
        //    beatDurationTime = Time.time - currentTime;
        //    isDone = true;
        //}
        //
        //print("beatdurationtime " + beatDurationTime);

        Debug.Log("Beat detected");
    }
    //public void LoadGameOver()
    //{
    //    SceneManager.LoadScene("Game Over");
    //}



}
