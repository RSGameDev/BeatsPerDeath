using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    ObjectPool objectPoolScript;

    bool startLevel = true;

    int spawnPosRand;
    public List<GameObject> spawnPoint;
    Vector3 startPos;

    bool firstSpawn = true;
    int lastSpawnPos;

    bool hasSpawned;

    public float timer;

    public enum LevelStage
    {
        Minute1, Minute2, Minute3
    }
    public LevelStage currentLevelStageType;

    bool triggerMin1;
    bool triggerMin2;

    int rookCountMinute2;
    int rookCountMinute3;

    private void Awake()
    {
        objectPoolScript = GetComponent<ObjectPool>();
        currentLevelStageType = LevelStage.Minute1;
    }
        
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 60 && !triggerMin1)
        {
            triggerMin1 = true;
            currentLevelStageType = LevelStage.Minute2;
        }

        if (timer >= 120 && !triggerMin2)
        {
            triggerMin2 = true;
            currentLevelStageType = LevelStage.Minute3;
        }

        if (SceneController.instance.spawnBeatCount == 1)
        {
            ResetSpawning();
        }

        if (startLevel)
        {
            if (SceneController.instance.spawnBeatCount == 4)
            {
                startLevel = false;
                ShroomPoolObject();
            }
        }

        switch (currentLevelStageType)
        {
            case LevelStage.Minute1:
                if (SceneController.instance.spawnBeatCount == 8 && !hasSpawned)
                {
                    ShroomPoolObject();
                    hasSpawned = true;
                }
                break;
            case LevelStage.Minute2:
                if (SceneController.instance.spawnBeatCount == 8 && !hasSpawned)
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
            case LevelStage.Minute3:
                if (SceneController.instance.spawnBeatCount == 8 && !hasSpawned)
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

        //if (SceneController.instance.spawnBeatCount == 8 && !hasSpawned)
        //{
        //    ShroomPoolObject();
        //    //RookPoolObject();
        //    hasSpawned = true;
        //}
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

    int NonDuplicatePosition(int lastPos)
    {
        do
        {
            spawnPosRand = UnityEngine.Random.Range(0, 5);
        } while (spawnPosRand == lastSpawnPos);

        lastSpawnPos = spawnPosRand;

        return spawnPosRand;
    }
}
