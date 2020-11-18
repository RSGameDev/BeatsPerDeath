using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            other.GetComponent<Player>().StartPosition();
        }
    }
}
