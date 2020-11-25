using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script spawns the coins. HAndles when it spawns as well.
public class CoinSpawner : MonoBehaviour
{
    ObjectPool objectPoolScript;

    bool startLevel = true;

    int spawnPosRand;
    public List<GameObject> spawnPoint;
    Vector3 startPos;

    bool firstSpawn = true;
    int lastSpawnPos;

    bool hasSpawned;

    private void Awake()
    {
        objectPoolScript = GetComponent<ObjectPool>();
    }

    private void Update()
    {      
        if (SceneController.instance.gameBeatCount == 4 && !hasSpawned)
        {
            hasSpawned = true;
            CoinPoolObject();
        }
    }

    public void ResetSpawning()
    {
        hasSpawned = false;

    }
    void CoinPoolObject()
    {
        GameObject obj = objectPoolScript.GetPooledCoinObject();
        obj.transform.position = SpawnPosition();
        obj.SetActive(true);
    }

    Vector3 SpawnPosition()
    {
        if (firstSpawn)
        {
            lastSpawnPos = SpawnRandom();
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

    int SpawnRandom()
    {
        do
        {
            spawnPosRand = UnityEngine.Random.Range(0, 5);
        } while (spawnPoint[spawnPosRand].GetComponentInChildren<SpawnDetectPlayer>().playerInFront);
        return spawnPosRand;
    }

    int NonDuplicatePosition(int lastPos)
    {
        int tempSpawnPos;
        do
        {
            tempSpawnPos = SpawnRandom();
        } while (tempSpawnPos == lastPos);

        lastSpawnPos = tempSpawnPos;

        return tempSpawnPos;
    }    
}
