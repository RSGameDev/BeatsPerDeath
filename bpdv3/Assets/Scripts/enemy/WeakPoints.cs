using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakPoints : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //GetComponentInParent<Enemy>().isWeak = true;
            //GetComponentInParent<Enemy>().playerObj.isEnemyWeak = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //GetComponentInParent<Enemy>().isWeak = false;
        }
    }
}
