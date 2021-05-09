using System;
using System.Collections;
using System.Collections.Generic;
using EnemyNS;
using Managers;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    private bool hasFacedDirection;
    private bool hasMoved;
    private bool hasResetValuesOne;
    private bool hasResetValuesTwo;

    private void Update()
    {
        if ((BeatManager.Instance.BeatIndex == 1 || BeatManager.Instance.BeatIndex == 5) && !hasResetValuesOne)
        {
            hasResetValuesOne = true;
            hasFacedDirection = false;
        }
        
        if ((BeatManager.Instance.BeatIndex == 2 || BeatManager.Instance.BeatIndex == 6) && !hasFacedDirection)
        {
            hasResetValuesOne = false;
            hasFacedDirection = true;
            FaceDirection();
        }

        if ((BeatManager.Instance.BeatIndex == 3 || BeatManager.Instance.BeatIndex == 7) && !hasResetValuesTwo)
        {
            hasResetValuesTwo = true;
            hasMoved = false;
        }
        
        if ((BeatManager.Instance.BeatIndex == 4 || BeatManager.Instance.BeatIndex == 0) && !hasMoved)
        {
            hasResetValuesTwo = false;
            hasMoved = true;
            Move();
        }
    }

    private void FaceDirection()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<EnemyDirection>().FaceDirection();
        }
    }

    private void Move()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<EnemyMovement>()._canMove = true;
        }
    }
}
