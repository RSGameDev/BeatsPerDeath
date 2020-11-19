using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AK.Wwise.Event menuEvent;
    public AK.Wwise.Event gplayEvent;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()

    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Stopped Menu Music");
            menuEvent.Stop(gameObject);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Stopped Menu Music");
            gplayEvent.Stop(gameObject);
        }
    }

    public void menuOnClick()
    {


        menuEvent.Post(gameObject, (uint)AkCallbackType.AK_MusicSyncBeat, CallBackBeatFunction);
        menuEvent.Post(gameObject, (uint)AkCallbackType.AK_MusicSyncBar, CallBackBarFunction);

    }

    public void gplayOnClick()
    {


        gplayEvent.Post(gameObject, (uint)AkCallbackType.AK_MusicSyncBeat, CallBackBeatFunction);
        gplayEvent.Post(gameObject, (uint)AkCallbackType.AK_MusicSyncBar, CallBackBarFunction);

    }

    void CallBackBeatFunction(object in_cookie, AkCallbackType in_type, object in_info)
    {
        Debug.Log("Beat detected");

    }

    void CallBackBarFunction(object in_cookie, AkCallbackType in_type, object in_info)
    {

        Debug.Log("New Bar detected");
    }


}
