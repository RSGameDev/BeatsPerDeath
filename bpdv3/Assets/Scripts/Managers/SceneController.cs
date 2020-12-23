using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Assets.Scripts.Core;

// Script for handling changes of scenes, panels etc.
// Note - Recently the music was added to the game scene. At the bottom of this script is a 'CallBackBeatFunction' that we are looking to have in another script 'a music script'.
// So the code at the bottom of this script is here temporarily and will be moved. This was done just so we can get a build out early for people to see.
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

    // TODO will be deleted, here for debug purposes
    public TextMeshProUGUI beatUiValue;
    // TODO will be deleted, here for debug purposes
    public TextMeshProUGUI spawnUiValue;
    bool isReferenced;           

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

        // TODO will be deleted, here for debug purposes
        BeatManager.Instance.AddListenerToAll(UpdateBeatUI);
        // TODO will be deleted, here for debug purposes
        BeatManager.Instance.AddListenerToAll(UpdateSpawnCountUI);
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

    // TODO will be moved into audio class
    //AudioEngine Code to give us information on beat detection from WWise. < John comment
    void CallBackBeatFunction(object in_cookie, AkCallbackType in_type, object in_info)                                                                                         
    {
        BeatManager.Instance.UpdateBeat();          
    }

    private void UpdateBeatUI() 
    {
        var currentBeat = BeatManager.Instance.BeatIndex % 4;
        beatUiValue.text = currentBeat.ToString();
    }

    private void UpdateSpawnCountUI()
    {
        var currentBeat = BeatManager.Instance.BeatIndex % 8;
        spawnUiValue.text = currentBeat.ToString();
    }
}
