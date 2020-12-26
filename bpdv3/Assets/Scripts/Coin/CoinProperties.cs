using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is attached to a coin prefab.
public class CoinProperties : MonoBehaviour
{
    GameObject gameUiGo;
    CoinSpawner coinSpawnerScript;
    Anchor anchorScript;

    int coinValue = 50;

    private void Awake()
    {
        anchorScript = GetComponentInChildren<Anchor>();
        coinSpawnerScript = FindObjectOfType<CoinSpawner>();
        gameUiGo = GameObject.FindGameObjectWithTag("GameUI");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("coincollision");
            anchorScript.AnchorTileObject.GetComponent<TileProperties>().OccupiedDecreased();
            gameUiGo.GetComponent<GameUI>().Scoring(coinValue);
            coinSpawnerScript.ResetSpawning();
            gameObject.SetActive(false);            
        }
    }
}
