using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinProperties : MonoBehaviour
{
    Anchor anchorScript;

    private void Awake()
    {
        anchorScript = GetComponentInChildren<Anchor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("coincollision");
            anchorScript.tileObj.GetComponent<TileProperties>().OccupiedDecreased();            
            gameObject.SetActive(false);            
        }
    }
}
