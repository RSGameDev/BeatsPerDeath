﻿using System.Collections;
using Assets.Scripts.Core;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

// Script for handling changes of scenes, panels etc.
// Note - Recently the music was added to the game scene. At the bottom of this script is a 'CallBackBeatFunction' that we are looking to have in another script 'a music script'.
// So the code at the bottom of this script is here temporarily and will be moved. This was done just so we can get a build out early for people to see.
namespace Managers
{
    public class SceneController : Singleton<SceneController>
    {
        #region Private and Constant variables
        
        [SerializeField] private GameObject _splashScreen;
        [SerializeField] private GameObject _warningScreen;
        [SerializeField] private GameObject _mainMenu;
        [SerializeField] private GameObject _optionsMenu;
        [SerializeField] private GameObject _gameplayMenu;
        [SerializeField] private GameObject _soundsMenu;
        [SerializeField] private GameObject _creditsScreen;
        [SerializeField] private int s_SplashDelay = 4;   //make const 4sec
        [SerializeField] private int s_warningDelay = 3; //make const 3sec
        private const string s_startGameSceneName = "scene1";
        private bool _isReferenced;           
        
        // TODO will be deleted, here for debug purposes
        [SerializeField] private TextMeshProUGUI beatUiValue;
        // TODO will be deleted, here for debug purposes
        [SerializeField] private TextMeshProUGUI spawnUiValue;

        private TileController tileController;
        
        #endregion

        #region Public and Protected variables
    
        public AudioManager audioManagerObj;
        public int CurrentSceneIndex { get; private set; }
    
        #endregion

        #region Contructors
        
        private new void Awake()
        {
            base.Awake();

            _warningScreen.transform.localScale = Vector3.zero;
            _mainMenu.transform.localScale = Vector3.zero;
            _optionsMenu.transform.localScale = Vector3.zero;
            _gameplayMenu.transform.localScale = Vector3.zero;
            _soundsMenu.transform.localScale = Vector3.zero;
            _creditsScreen.transform.localScale = Vector3.zero;
            audioManagerObj.ResetAudioStates();
        }

        private void Start()
        {
            SetSceneSettings();
        }
    
        #endregion
         
        #region Private Methods
    
        private void Update()
        {
            if (SceneManager.GetActiveScene().buildIndex == 1 && !_isReferenced)
            {
                tileController = GameObject.FindWithTag("TileController").GetComponent<TileController>();
                
                _isReferenced = true;
                var temp = GameObject.Find("BeatUITestValue (TMP)");
                
                beatUiValue = temp.GetComponent<TextMeshProUGUI>();

                var temp1 = GameObject.Find("ScoreUITest Value (TMP) (1)");
                spawnUiValue = temp1.GetComponent<TextMeshProUGUI>();
            }

            audioManagerObj.MusicLayering();
        }

        private void SetSceneSettings()
        {
            CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            switch (CurrentSceneIndex)
            {
                case 0:
                    audioManagerObj.SetIntroSceneSettings();
                    break;
                case 1:
                    audioManagerObj.SetGamePlaySceneSettings();
                    break;
            }

            // TODO will be deleted, here for debug purposes
            BeatManager.Instance.AddListenerToAll(UpdateBeatUI);
            // TODO will be deleted, here for debug purposes
            BeatManager.Instance.AddListenerToAll(UpdateSpawnCountUI);
        }
        
        private IEnumerator DelayOnSplashScreen()
        {
            yield return new WaitForSeconds(s_SplashDelay);
            _splashScreen.transform.localScale = Vector3.zero;
            LoadWarningScreen();
        }

        private void LoadWarningScreen()
        {
            _warningScreen.transform.localScale = Vector3.one;
            StartCoroutine(DelayOnWarningScreen());
        }

        private IEnumerator DelayOnWarningScreen()
        {
            yield return new WaitForSeconds(s_warningDelay);
            _warningScreen.transform.localScale = Vector3.zero;
            LoadMainMenu();
        }

        private void UpdateBeatUI() 
        {
            if (tileController.turnOffDevTileValues)
            {
                return;
            }
            
            var currentBeat = BeatManager.Instance.BeatIndex % 4;
            beatUiValue.text = currentBeat.ToString();
            
            if (currentBeat == 0)
            {
                currentBeat = 4;
                beatUiValue.text = currentBeat.ToString();
            }
        }

        private void UpdateSpawnCountUI()
        {
            if (tileController.turnOffDevTileValues)
            {
                return;
            }
            
            var currentBeat = BeatManager.Instance.BeatIndex % 8;
            spawnUiValue.text = currentBeat.ToString();
            
            if (currentBeat == 0)
            {
                currentBeat = 8;
                spawnUiValue.text = currentBeat.ToString();
            }
        }
        
        #endregion
        
        #region Public Methods
        
        public void DelaySplashScreen()
        {
            StartCoroutine(DelayOnSplashScreen());
        }
        
        public void LoadMainMenu()
        {
            _mainMenu.transform.localScale = Vector3.one;
            _optionsMenu.transform.localScale = Vector3.zero;
        }

        public void StartGame()
        {
            SceneManager.LoadScene(s_startGameSceneName);
            CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex+1;
            audioManagerObj.SetGamePlaySceneSettings();
        }

        public void LoadOptionsScreen()
        {
            _optionsMenu.transform.localScale = Vector3.one;
            _gameplayMenu.transform.localScale = Vector3.one;
            _mainMenu.transform.localScale = Vector3.zero;
        }

        public void LoadGameplayScreen()
        {
            _gameplayMenu.transform.localScale = Vector3.one;
            _soundsMenu.transform.localScale = Vector3.zero;
            _creditsScreen.transform.localScale = Vector3.zero;
        }
        
        public void LoadSoundsScreen()
        {
            _gameplayMenu.transform.localScale = Vector3.zero;
            _soundsMenu.transform.localScale = Vector3.one;
            _creditsScreen.transform.localScale = Vector3.zero;
        }

        public void LoadCreditsScreen()
        {
            _gameplayMenu.transform.localScale = Vector3.zero;
            _soundsMenu.transform.localScale = Vector3.zero;
            _creditsScreen.transform.localScale = Vector3.one;
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void RestartScene()
        {
            SceneManager.LoadScene(CurrentSceneIndex);
        }

        public void LoadNextScene()
        {
            SceneManager.LoadScene(CurrentSceneIndex + 1);
        }
        
        #endregion
    }
}
