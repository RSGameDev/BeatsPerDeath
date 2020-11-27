using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This is on the GameUI, the beat bar specifically. The area towards the bottom displayed by a thin rectangle. Where it detects if a beat has been hit in this time window.
public class CoreHitzone : MonoBehaviour
{
    public bool onBeat;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        onBeat = true;
        collision.GetComponent<Image>().color = new Color(0, 0, 255, 255);        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        onBeat = false;
        collision.GetComponent<Image>().color = new Color(255, 255, 255, 255);
    }
}
