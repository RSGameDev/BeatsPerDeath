using System.Collections.Generic;
using UnityEngine;

// This can spawn all enemy types, two currently; shroom , rook.
public class EnemySpawner : MonoBehaviour
{
    #region Variables

    private const int s_LevelTimeLimit = 60;
    private const int s_StageLimit = 2;

    private ObjectPool objectPoolScript;
    private Vector3 startPos;
    private float _levelTimer;
    private bool firstSpawn = true;
    private int lastSpawnPos;
    private int _currentStage;
    private int rookCountMinute2;
    private int rookCountMinute3;
    private int spawnPosRand;

    public List<GameObject> spawnPoint;

    #endregion

    private void Awake()
    {
        objectPoolScript = GetComponent<ObjectPool>();
    }

    private void Start()
    {
        BeatManager.Instance.AddListener(2, SpawnEnemy);
    }

    /// <summary>
    /// The demo was for a 3minute life span so I recently made this to carry out the course of the 3 minutes. 
    /// Currently it has 3 stages but the game continues past 3 minutes. 
    /// Game continues as it is still on stage 3 after 3 minutes
    /// </summary>
    private void Update()                                       
    {
        UpdateLevelTimer();
    }

    private void UpdateLevelTimer()
    {
        if (_currentStage == s_StageLimit) 
        {
            return;
        }

        _levelTimer += Time.deltaTime;

        if (_levelTimer >= s_LevelTimeLimit)
        {
            _levelTimer = 0;
            _currentStage++;
        }
    }

    private void SpawnEnemy()
    {
        switch (_currentStage)
        {
            // 0 to 1 minute there are just shrooms spawning.
            case 0:
                SpawnLevelOneEnemies();
                break;
            // 1 to 2 minute there are shrooms spawning but with a rook spawning every 24 beat (8 beat * 3 rookcount).
            case 1:
                SpawnLevelTwoEnemies();
                break;
            // 2 to ongoing, there are shrooms spawning but with a rook spawning every 16 beat (8 beat * 2 rookcount).
            case 2:
                SpawnLevelThreeEnemies();
                break;
        }

        return;

        void SpawnLevelOneEnemies()
        {
            ShroomPoolObject();
        }

        void SpawnLevelTwoEnemies()
        {
            rookCountMinute2++;
            if (rookCountMinute2 == 3)
            {
                RookPoolObject();
                rookCountMinute2 = 0;
            }
            else
            {
                ShroomPoolObject();
            }
        }

        void SpawnLevelThreeEnemies()
        {
            rookCountMinute3++;
            if (rookCountMinute3 == 2)
            {
                RookPoolObject();
                rookCountMinute3 = 0;
            }
            else
            {
                ShroomPoolObject();
            }
        }
    }

    private void ShroomPoolObject()
    {
        GameObject obj = objectPoolScript.GetPooledShroomObject();
        obj.transform.position = SpawnPosition();
        obj.SetActive(true);
    }

    private void RookPoolObject()
    {
        GameObject obj = objectPoolScript.GetPooledRookObject();
        obj.transform.position = SpawnPosition();
        obj.SetActive(true);
    }

    private Vector3 SpawnPosition()
    {
        if (firstSpawn)
        {
            spawnPosRand = Random.Range(0, 5);
            lastSpawnPos = spawnPosRand;
            firstSpawn = false;
        }
        else
        {
            // So the enemy will appear on a new column to previously. To add variety.
            spawnPosRand = NonDuplicatePosition(lastSpawnPos);      
        }        

        switch (spawnPosRand)
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
        return startPos;
    }

    // So the enemy will appear on a new column to previously. To add variety.
    private int NonDuplicatePosition(int lastPos)
    {
        do
        {
            spawnPosRand = Random.Range(0, 5);
        } while (spawnPosRand == lastPos);

        lastSpawnPos = spawnPosRand;

        return spawnPosRand;
    }
}
