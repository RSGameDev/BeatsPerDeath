using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Assets.Scripts.Core;
using Managers;

public class AudioManager : Singleton<AudioManager>
{
    // ---------- WV MUSIC SECTION ----------- //

    public SceneController controllerObj;

    public AK.Wwise.Event menuEvent;
    public AK.Wwise.Event gPlayEvent;
    public AK.Wwise.Event UISoundsEvent;

    public bool isMenuPlaying = true;
    public bool isGPlayPlaying = false;

    public void SetIntroSceneSettings()
    {
        menuEvent.Post(gameObject);
        isMenuPlaying = true;
        controllerObj.DelaySplashScreen();
    }

    public void SetGamePlaySceneSettings()
    {
        
        menuEvent.Stop(gameObject);
        gPlayEvent.Post(gameObject, (uint)AkCallbackType.AK_MusicSyncBeat, CallBackBeatFunction);
        
    }

    public void CallBackBeatFunction(object in_cookie, AkCallbackType in_type, object in_info)
    {
        if (SceneController.Instance.activeScene != "GameOver")
        {
            BeatManager.Instance.UpdateBeat();
        }
    }

    public void CstateMusic()
    {
        AkSoundEngine.SetState("C_State", "Play");
    }

    //This approach with variable declared works with pressing keys, not yet with the letter changing 
    public void MusicLayering(string status)
    {
        switch (status)
        {
            case "D":
                AkSoundEngine.SetState("D_State", "Play");               
                break;

            case "C":
                AkSoundEngine.SetState("C_State", "Play");
                break;

            case "B":
                AkSoundEngine.SetState("B_State", "Play");
                break;

            case "A":
                AkSoundEngine.SetState("A_State", "Play");
                break;

            case "S":
                AkSoundEngine.SetState("S_State", "Play");
                break;

            case " ":
                ResetAudioStates();
                break;
        }
    }

    public void TestLayering()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MusicLayering("D");
            //AkSoundEngine.SetState("C_State", "Play");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MusicLayering("B");
            //AkSoundEngine.SetState("B_State", "Play");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ResetAudioStates();
            //AkSoundEngine.SetState("A_State", "Play");
        }

        //if (Input.GetKeyDown(KeyCode.Alpha0))
        //{
        //    ResetAudioStates();
        //}
    }

    public void ResetAudioStates()
    {

        AkSoundEngine.SetState("A_State", "Stop");
        AkSoundEngine.SetState("B_State", "Stop");
        AkSoundEngine.SetState("C_State", "Stop");
        AkSoundEngine.SetState("D_State", "Stop");
        AkSoundEngine.SetState("S_State", "Stop");
    }


    // ------------ SFX UI SOUND SECTION ------------- //


    //public void OnHoverSound() {

    //    AkSoundEngine.PostEvent("Hover", gameObject);
    //}

    //public void OnClickSounds() {  

    //} 

}