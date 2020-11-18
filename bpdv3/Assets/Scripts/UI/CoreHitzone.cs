using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// note - im not keen on this scriptname, only had this name from something i was trying before. so needs changing down the line.
public class CoreHitzone : MonoBehaviour
{
    public bool onBeat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("onenter");
        onBeat = true;
        collision.GetComponent<Image>().color = new Color(0, 0, 255, 255);        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //print("out");
        onBeat = false;
        collision.GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }
}
