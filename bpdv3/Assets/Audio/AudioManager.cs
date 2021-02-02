using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Assets.Scripts.Core;

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
        BeatManager.Instance.UpdateBeat();
        Debug.Log("Beat detected");
    }

    public void MusicLayering()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AkSoundEngine.SetState("C_State", "Play");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AkSoundEngine.SetState("B_State", "Play");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AkSoundEngine.SetState("A_State", "Play");
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ResetAudioStates();
        }
    }

    public void ResetAudioStates()
    {

        AkSoundEngine.SetState("A_State", "Stop");
        AkSoundEngine.SetState("B_State", "Stop");
        AkSoundEngine.SetState("C_State", "Stop");
    }


    // ------------ SFX UI SOUND SECTION ------------- //


    //public void OnHoverSound() {

    //    AkSoundEngine.PostEvent("Hover", gameObject);
    //}

    //public void OnClickSounds() {  

    //} 

}

