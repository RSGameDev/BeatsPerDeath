using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Sourced from https://learn.unity.com/tutorial/introduction-to-object-pooling#5cf1fc18edbc2a4c9daf6993
// For optimising performance.

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjectsCoin;
    public int amountToPoolCoin;
    public GameObject objectToPoolCoin;

    public List<GameObject> pooledObjectsShroom;
    public int amountToPoolShroom;
    public GameObject objectToPoolShroom;

    public List<GameObject> pooledObjectsRook;    
    public int amountToPoolRook;    
    public GameObject objectToPoolRook;    

    // Start is called before the first frame update
    void Start()
    {
        pooledObjectsCoin = new List<GameObject>();
        for (int i = 0; i < amountToPoolCoin; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPoolCoin);
            obj.SetActive(false);
            pooledObjectsCoin.Add(obj);
        }

        pooledObjectsShroom = new List<GameObject>();
        for (int i = 0; i < amountToPoolShroom; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPoolShroom);
            obj.SetActive(false);
            pooledObjectsShroom.Add(obj);
        }

        pooledObjectsRook = new List<GameObject>();
        for (int i = 0; i < amountToPoolRook; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPoolRook);
            obj.SetActive(false);
            pooledObjectsRook.Add(obj);
        }
    }

    public GameObject GetPooledCoinObject()
    {
        for (int i = 0; i < pooledObjectsCoin.Count; i++)
        {
            if (!pooledObjectsCoin[i].activeInHierarchy)
            {
                return pooledObjectsCoin[i];
            }
        }
        return null;
    }

    public GameObject GetPooledShroomObject()
    {
        for (int i = 0; i < pooledObjectsShroom.Count; i++)
        {
            if (!pooledObjectsShroom[i].activeInHierarchy)
            {
                return pooledObjectsShroom[i];
            }
        }
        return null;
    }

    public GameObject GetPooledRookObject()
    {
        for (int i = 0; i < pooledObjectsRook.Count; i++)
        {
            if (!pooledObjectsRook[i].activeInHierarchy)
            {
                return pooledObjectsRook[i];
            }
        }
        return null;
    }
}
