using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is not finished yet. Works but needs more done with it, the GameUI 'multiplier' section will relate to this script i believe.
public class ScoreManager : MonoBehaviour
{
    public int score;                       
    //float times1_2 = 1.2f;                // This is for when the combo metre changes, the scores change, multipliers.
    //float times1_6 = 1.6f;                // 
    //float times2_2 = 2.2f;                // 
    //float times3_0 = 3f;                  // 
    //float times4_0 = 4f;                  // 
    
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
