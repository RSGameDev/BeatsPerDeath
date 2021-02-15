using System;
using Core;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        //new script
        //private const int s_LevelTimeLimit = 60;
        //private const int s_StageLimit = 2;
        //
        //private float _levelTimer;
        private int _currentStage;
        //private int rookCountMinute2;
        //private int rookCountMinute3;
        
        #region Private & Const Variables
        
        [SerializeField] private TextMeshProUGUI _timerUi;
        private SceneController _sceneController;
        private Spawner _spawner;
        private bool _stage1;
        private bool _stage2; 
        private bool _stage3; 
        private bool _checkLevelStatus;

        #endregion

        #region Public & Protected Variables
        public float Timer { get; set; }
        
        #endregion

        #region Constructor

        private void Awake()
        {
            _sceneController = GameObject.FindWithTag("SceneController").GetComponent<SceneController>();
            _spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        }

        private void Start()
        {
            _currentStage = _sceneController.CurrentSceneIndex;
        }

        #endregion
        
        #region Private Methods

        private void Update()
        {
            GameTimer();

            StageOfLevel();
        }

        private void StageOfLevel()
        {
            if (Timer >= 0 && Timer <= 59 && !_stage1)
            {
                _stage1 = true;
                _checkLevelStatus = true;
            }

            if (Timer >= 60 && Timer <= 119 && !_stage2)
            {
                _stage2 = true;
                _checkLevelStatus = true;
            }

            if (Timer >= 120 && !_stage3)
            {
                _stage3 = true;
                _checkLevelStatus = true;
            }
            
            if (_checkLevelStatus)
            {
                _checkLevelStatus = false;
                CurrentGameLevel(_currentStage);
            }
        }

        private void GameTimer()
        {
            Timer += Time.deltaTime;

            _timerUi.text = Timer.ToString("00");
        }

        #endregion

        #region Public & Protected Methods

        public void CurrentGameLevel(int levelNumber)
        {
            switch (levelNumber)
            {
                case 1:
                    if (_stage1)
                    {
                        _spawner.SpawnPattern(levelNumber,1);
                    }
                    
                    if (_stage2)
                    {
                        _spawner.SpawnPattern(levelNumber,2);
                    }
                    
                    if (_stage3)
                    {
                        _spawner.SpawnPattern(levelNumber,3);
                    }
                    break;


                case 2:
                    //SpawnLevelThreeEnemies();
                    break;
            }

            return;

            ////void SpawnLevelOneEnemies()
            //{
            //    ShroomPoolObject();
            //}
//
            ////void SpawnLevelTwoEnemies()
            //{
            //    rookCountMinute2++;
            //    if (rookCountMinute2 == 3)
            //    {
            //        RookPoolObject();
            //        rookCountMinute2 = 0;
            //    }
            //    else
            //    {
            //        ShroomPoolObject();
            //    }
            //}
//
            ////void SpawnLevelThreeEnemies()
            //{
            //    rookCountMinute3++;
            //    if (rookCountMinute3 == 2)
            //    {
            //        RookPoolObject();
            //        rookCountMinute3 = 0;
            //    }
            //    else
            //    {
            //        ShroomPoolObject();
            //    }
            //}
        }
        
        #endregion
        
        
    }
}
