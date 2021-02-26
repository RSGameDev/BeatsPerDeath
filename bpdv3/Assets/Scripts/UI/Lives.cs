using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Lives : MonoBehaviour
{
    #region Life
    [Header("Life Count")]
    public Image[] lifeCount;
    #endregion
    
    
    public void PlayerLoseLife(int lifecount)
    {
        switch (lifecount)
        {
            case 2:
                lifeCount[2].GetComponent<Image>().color = new Color32(0, 0, 0, 100);
                break;
            case 1:
                lifeCount[1].GetComponent<Image>().color = new Color32(0, 0, 0, 100);
                break;
            case 0:
                lifeCount[0].GetComponent<Image>().color = new Color32(0, 0, 0, 100);
                break;
        }
    }
}
