using System;
using System.Collections.Generic;
using EnemyNS;
using Managers;
using UI.Main;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core
{
    [RequireComponent(typeof(ObjectPool))]
    public class Spawner : MonoBehaviour
    {
        #region Private & Constant variables
    
        [SerializeField] private List<GameObject> spawnPoint;
        private GameObject objectPool;
        private Vector3 startPos;
        private bool _hasFunctionExecutedBefore;
        private int _createSpawnPosition;
        private int _lastSpawnPosition;
        private int _currentLevel;
        private int _currentStageOfLevel;
        private bool cancel;

        #endregion

        #region Public & Protected variables
        public static bool HasCoinSpawned { get; set; }
    
        #endregion

        #region Constructors

        private void Start()
        {
            ////BeatManager.Instance.AddListener(0, SpawnObject);
            //BeatManager.Instance.AddListener(3, SpawnObject);
            //BeatManager.Instance.AddListener(5, SpawnObject);
        }

        #endregion
    
        #region Private methods

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O) && !cancel)
            {
                cancel = true;
                //BeatManager.Instance.RemoveListener(0, SpawnObject);
                BeatManager.Instance.RemoveListener(4, SpawnObject);
            }
            else if(Input.GetKeyDown(KeyCode.O) && cancel)
            {
                cancel = false;
                //BeatManager.Instance.AddListener(0, SpawnObject);
                BeatManager.Instance.AddListener(4, SpawnObject);
            }
        }

        public void ClearListeners()
        {
            //BeatManager.Instance.RemoveListener(0, SpawnObject);
            BeatManager.Instance.RemoveListener(4, SpawnObject);
        }

        public void SpawnPattern(int level, int stageOfLevel)
        {
            _currentLevel = level;
            _currentStageOfLevel = stageOfLevel;
            
            switch (_currentLevel)
            {
                case 1:
                    switch (stageOfLevel)
                    {
                        case 1:
                            if (!cancel)
                            {
                                //BeatManager.Instance.AddListener(0, SpawnObject);
                                BeatManager.Instance.AddListener(4, SpawnObject);
                            }
                            break;
                        //case 2:
                        //    BeatManager.Instance.AddListener(2, SpawnObject);
                        //    break;
                        //case 3:
                        //    BeatManager.Instance.AddListener(4, SpawnObject);
                        //    break;
                    }
                    break;
                case 2:
                    break;
            }
        }
        
        private void SpawnObject()
        {
            switch (_currentLevel)
            {
                case 1:
                    switch (_currentStageOfLevel)
                    {
                        case 1:
                            objectPool = GetComponent<ObjectPool>().GetPooledShroomObject();
                            break;
                        //case 3:
                        //    objectPool = GetComponent<ObjectPool>().GetPooledRookObject();
                        //    break;
                        //case 5:
                        //    if (HasCoinSpawned) return;
                        //    objectPool = GetComponent<ObjectPool>().GetPooledCoinObject();
                        //    HasCoinSpawned = true;
                        //    break;
                    }
                    break;
                case 2:
                        break;
            }
            objectPool.transform.position = SpawnPosition();
            objectPool.SetActive(true);
            //objectPool.GetComponent<Enemy>().hasSpawned = true;
        }

        private Vector3 SpawnPosition()
        {
            _createSpawnPosition = Random.Range(0, 5);
        
            if (_hasFunctionExecutedBefore)
            {
                _createSpawnPosition = NonDuplicatePosition(_createSpawnPosition); 
            }
        
            _lastSpawnPosition = _createSpawnPosition;
        
            switch (_createSpawnPosition)
            {
                case 0:
                    startPos = spawnPoint[0].transform.position;
                    break;
                case 1:
                    startPos = spawnPoint[1].transform.position;
                    break;
                case 2:
                    startPos = spawnPoint[2].transform.position;
                    break;
                case 3:
                    startPos = spawnPoint[3].transform.position;
                    break;
                case 4:
                    startPos = spawnPoint[4].transform.position;
                    break;
            }
            
            _hasFunctionExecutedBefore = true;
            
            return startPos;
        }

        private int NonDuplicatePosition(int lastPos)
        {
            while (_lastSpawnPosition == lastPos)
            {
                lastPos = Random.Range(0, 5);
            }
            return lastPos;
        }

        #endregion
    
    
    }
}
