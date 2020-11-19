using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    //float times1_2 = 1.2f;
    //float times1_6 = 1.6f;
    //float times2_2 = 2.2f;
    //float times3_0 = 3f;
    //float times4_0 = 4f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int EnemyScore(string enemyType)
    {
        switch (enemyType)
        {
            case "Shroom":
                score = 100;
                break;
            case "Rook":
                score = 200;
                break;
            default:
                break;
        }
        return score;
    }

    void PlayerDefeatEnemy()
    {

    }
}
