using UnityEngine;
using UnityEngine.UI;

// This is on the GameUI, the beat bar specifically. The area towards the bottom displayed by a thin rectangle. Where it detects if a beat has been hit in this time window.
namespace UI.Main
{
    public class CoreHitzone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            BeatBar.thresholdZone = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            BeatBar.thresholdZone = false;
        }
    }
}
