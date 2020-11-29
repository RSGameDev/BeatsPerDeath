using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

// This can spawn all enemy types, two currently; shroom , rook.
public class EnemySpawner : MonoBehaviour
{
    #region Variables
    ObjectPool objectPoolScript;

    bool startLevel = true;

    int spawnPosRand;
    public List<GameObject> spawnPoint;
    Vector3 startPos;

    bool firstSpawn = true;
    int lastSpawnPos;

    bool hasSpawned;

    public float timer;

    // How to spawn enemy types at different stages of the level
    public enum LevelStage                       
    {
        Minute1, Minute2, Minute3
    }
    public LevelStage currentLevelStageType;

    bool triggerMin1;
    bool triggerMin2;

    int rookCountMinute2;
    int rookCountMinute3;

    #endregion

    private void Awake()
    {
        objectPoolScript = GetComponent<ObjectPool>();
        currentLevelStageType = LevelStage.Minute1;
    }

    // The demo was for a 3minute life span so I recently made this to carry out the course of the 3 minutes. Currently it has 3 stages but the game continues past 3 minutes. This hasn't been coded yet to end at 3 minutes.
    private void Update()                                       
    {
        timer += Time.deltaTime;

        // 0 to 1 minute mark
        if (timer >= 60 && !triggerMin1)
        {
            triggerMin1 = true;
            currentLevelStageType = LevelStage.Minute2;
        }

        // 1 to 2 minute mark
        if (timer >= 120 && !triggerMin2)
        {
            triggerMin2 = true;
            currentLevelStageType = LevelStage.Minute3;
        }

        if (SceneController.Instance.spawnBeatCount == 1)
        {
            ResetSpawning();
        }

        if (startLevel)
        {
            if (SceneController.Instance.spawnBeatCount == 4)
            {
                startLevel = false;
                ShroomPoolObject();
            }
        }

        // This orchestrates how the enemies appear in the level, created for the build that was done recently.
        switch (currentLevelStageType)
        {
            // 0 to 1 minute there are just shrooms spawning.
            case LevelStage.Minute1:
                if (SceneController.Instance.spawnBeatCount == 8 && !hasSpawned)
                {
                    ShroomPoolObject();
                    hasSpawned = true;
                }
                break;
            // 1 to 2 minute there are shrooms spawning but with a rook spawning every 24 beat (8 beat * 3 rookcount).
            case LevelStage.Minute2:
                if (SceneController.Instance.spawnBeatCount == 8 && !hasSpawned)
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
                    hasSpawned = true;
                }
                break;
            // 2 to ongoing, there are shrooms spawning but with a rook spawning every 16 beat (8 beat * 2 rookcount).
            case LevelStage.Minute3:
                if (SceneController.Instance.spawnBeatCount == 8 && !hasSpawned)
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
                    hasSpawned = true;
                }
                break;
            default:
                break;
        }        
    }

    void ResetSpawning()
    {
        hasSpawned = false;
    }

    void ShroomPoolObject()
    {
        GameObject obj = objectPoolScript.GetPooledShroomObject();
        obj.transform.position = SpawnPosition();
        obj.SetActive(true);
    }

    void RookPoolObject()
    {
        GameObject obj = objectPoolScript.GetPooledRookObject();
        obj.transform.position = SpawnPosition();
        obj.SetActive(true);
    }

    Vector3 SpawnPosition()
    {
        if (firstSpawn)
        {
            spawnPosRand = UnityEngine.Random.Range(0, 5);
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
    int NonDuplicatePosition(int lastPos)
    {
        do
        {
            spawnPosRand = UnityEngine.Random.Range(0, 5);
        } while (spawnPosRand == lastPos);

        lastSpawnPos = spawnPosRand;

        return spawnPosRand;
    }
}
