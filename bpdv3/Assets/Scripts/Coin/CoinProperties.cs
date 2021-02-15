using System.Collections;
using System.Collections.Generic;
using Core;
using Floor;
using Mechanics;
using UnityEngine;

// This script is attached to a coin prefab.
public class CoinProperties : MonoBehaviour
{
    GameObject gameUiGo;
    Anchor anchorScript;

    int coinValue = 50;

    private void Awake()
    {
        anchorScript = GetComponentInChildren<Anchor>();
        gameUiGo = GameObject.FindGameObjectWithTag("GameUI");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("coincollision");
            anchorScript.anchorTileObject.GetComponent<TileProperties>().OccupiedDecreased();
            gameUiGo.GetComponent<GameUI>().Scoring(coinValue);
            Spawner.HasCoinSpawned = false;
            gameObject.SetActive(false);            
        }
    }
}
