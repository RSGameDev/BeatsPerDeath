using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    ObjectPool objectPoolScript;

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
            //do
            //{
            //    spawnPosRand = UnityEngine.Random.Range(0, 5);
            //} while (spawnPoint[spawnPosRand].GetComponentInChildren<SpawnDetectPlayer>().playerInFront);

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

    //Vector3 SpawnPosition()
    //{
    //    if (firstSpawn)
    //    {
    //        spawnPosRand = UnityEngine.Random.Range(0, 5);
    //        lastSpawnPos = spawnPosRand;
    //        firstSpawn = false;
    //    }
    //    else
    //    {
    //        spawnPosRand = NonDuplicatePosition(lastSpawnPos);
    //    }
    //
    //    switch (spawnPosRand)
    //    {
    //        case 0:
    //            startPos = spawnPoint[0].transform.position;
    //            break;
    //        case 1:
    //            startPos = spawnPoint[1].transform.position;
    //            break;
    //        case 2:
    //            startPos = spawnPoint[2].transform.position;
    //            break;
    //        case 3:
    //            startPos = spawnPoint[3].transform.position;
    //            break;
    //        case 4:
    //            startPos = spawnPoint[4].transform.position;
    //            break;
    //    }
    //    return startPos;
    //}

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

    //int NonDuplicatePosition(int lastPos)
    //{
    //    do
    //    {
    //        spawnPosRand = UnityEngine.Random.Range(0, 5);
    //    } while (spawnPosRand == lastSpawnPos);
    //
    //    lastSpawnPos = spawnPosRand;
    //
    //    return spawnPosRand;
    //}
}

//public class CoinSpawner : MonoBehaviour
//{
//    public GameObject coin;
//    public GameObject[] spawnPoint;
//
//    public float timer;
//    public float spawnInterval = 7f;
//    public int randomNumber;
//    //public int recentRandomNumber = 4;
//
//    // Start is called before the first frame update
//    void Start()
//    {
//        timer = Time.time;
//    }
//
//    // Update is called once per frame
//    void Update()
//    {
//        timer += Time.deltaTime;
//
//        if (timer > spawnInterval)
//        {
//            CoinSpawn();
//        }
//    }
//
//    void CoinSpawn()
//    {
//        List<GameObject> temp = new List<GameObject>();
//
//        randomNumber = Random.Range(0, 6);
//
//        foreach (GameObject go in spawnPoint)
//        {
//            if (go.transform.position.z >= 6.25)
//            {
//                temp.Add(go);
//            }
//        }
//
//        GameObject coinSpawnGO = temp[randomNumber];
//
//        coinSpawnGO.transform.GetChild(0).gameObject.SetActive(true);
//        //////       coinSpawnGO.GetComponent<TileProperties>().hasCoin = true;
//
//        timer = 0f;
//        //do
//        //{
//        //    randomNumber = Random.Range(0, spawnPoint.Length);
//        //
//        //} while (randomNumber == recentRandomNumber);
//        //
//        //recentRandomNumber = randomNumber;
//        //
//        //Vector3 pos = new Vector3(spawnPoint[randomNumber].position.x, spawnPoint[randomNumber].position.y+1, 6.25f);
//        //GameObject go = Instantiate(coin,pos, Quaternion.identity);
//        //go.SetActive(true);
//        //timer = 0f;
//    }
//
//
//}
