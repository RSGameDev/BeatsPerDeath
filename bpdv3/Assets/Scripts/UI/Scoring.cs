using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoring : MonoBehaviour
{
    #region Score
    // Add score code
    [Header("Score")]
    public int score = 0;
    public TextMeshProUGUI scoreUiValue;
    #endregion
    
    #region Multiplier
    [Header("Multiplier")]
    public GameObject multiplierGO;
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        scoreUiValue.text = score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // todo this code may not be needed, keep for now
        #region MultiplierStatus
        if (Input.GetKeyDown(KeyCode.H))
        {
            MultiplerStatus(1);
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            MultiplerStatus(2);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            MultiplerStatus(3);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            MultiplerStatus(4);
        }
        #endregion
    }
    
    public void ScoreUpdate(int value)
    {
        score += value;
        scoreUiValue.text = score.ToString();
    }
    
    void MultiplerStatus(int multiplier)
    {
        switch (multiplier)
        {
            case 1:
                multiplierGO.GetComponentInChildren<TextMeshProUGUI>().text = "Multiplier x2";
                break;
            case 2:
                multiplierGO.GetComponentInChildren<TextMeshProUGUI>().text = "Multiplier x4";
                break;
            case 3:
                multiplierGO.GetComponentInChildren<TextMeshProUGUI>().text = "Multiplier x6";
                break;
            case 4:
                multiplierGO.GetComponentInChildren<TextMeshProUGUI>().text = "Multiplier x8";
                break;
        }
    }  
}
