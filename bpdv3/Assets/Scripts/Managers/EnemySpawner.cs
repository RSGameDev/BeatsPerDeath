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

    private void Awake()
    {
        objectPoolScript = GetComponent<ObjectPool>();
    }
        
    private void Update()
    {
        //////////// because of old wwise taken out
        ///////////if (MusicScript.spawnBeatCount == 1)
        {
            ResetSpawning();
        }

        if (startLevel)
        {
            //////////// because of old wwise taken out
            /////////if (MusicScript.spawnBeatCount == 4)
            {
                startLevel = false;
                ShroomPoolObject();
                //RookPoolObject();
            }
        }

        //////////// because of old wwise taken out
        //////////if (MusicScript.spawnBeatCount == 8 && !hasSpawned)
        {
            ShroomPoolObject();
            //RookPoolObject();
            hasSpawned = true;
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
