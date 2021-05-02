using System;
using System.Collections;
using System.Collections.Generic;
using EnemyNS;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();
    private bool hasFacedDirection;

    private void Update()
    {
        if (BeatManager.Instance.BeatIndex == 1 || BeatManager.Instance.BeatIndex == 5 && !hasResetValues)
        {
            hasResetValues = true;
            hasAssignedTile = false;
        }
        
        if (BeatManager.Instance.BeatIndex == 2 || BeatManager.Instance.BeatIndex == 6 && !hasFacedDirection)
        {
            hasFacedDirection = true;
            FaceDirection();
        }

        if ((BeatManager.Instance.BeatIndex == 3 || BeatManager.Instance.BeatIndex == 7) && !hasAssignedTile)
        {
            hasAssignedTile = true;
            hasResetValues = false;
            _tileController.TilePermissionCheck();
        }
        
        if (BeatManager.Instance.BeatIndex == 4 || BeatManager.Instance.BeatIndex == 0 && !hasMoved)
        {
            hasMoved = true;
            Move();
            FaceDirection();
        }
    }

    private void FaceDirection()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[0].GetComponent<EnemyDirection>().FaceDirection();
        }
    }

    private void Move()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[0].GetComponent<EnemyMovement>().Movement();
        }
    }
}
