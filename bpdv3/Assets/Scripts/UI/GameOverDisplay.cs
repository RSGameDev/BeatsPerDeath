using System.Collections;
using System.Collections.Generic;
using Scripts.A;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Managers;

// WIP - Had been worked on recently. Has not been included in the game yet though.
public class GameOverDisplay : MonoBehaviour
{
    [SerializeField] SceneController sceneController;
    public void Restart()
    {
        sceneController.StartGame();
        Debug.Log("Click");


    }
    public void Quit()
    {
        Application.Quit();
    }

}
