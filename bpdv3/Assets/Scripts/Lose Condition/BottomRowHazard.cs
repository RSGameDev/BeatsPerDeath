using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The last row represented with the flame/laser was the intention from the GDD but we couldnt find laser placeholder for time being.
public class BottomRowHazard : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("FireHazard"))
        {
            other.transform.parent.gameObject.SetActive(false);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().OnPlayerDie();
        }
    }
}
