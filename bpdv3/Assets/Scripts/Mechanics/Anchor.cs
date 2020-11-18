using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    public GameObject tileObj;

    private void OnTriggerEnter(Collider other)
    {
        if ((gameObject.transform.parent.CompareTag("enemy") || gameObject.transform.parent.CompareTag("coin")) && other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            tileObj = other.gameObject;
            Vector3 newPos = other.GetComponent<Renderer>().bounds.center;
            transform.parent.position = new Vector3(newPos.x, newPos.y+1f, newPos.z);
            transform.parent.SetParent(other.transform);
        }        

        if (gameObject.transform.parent.CompareTag("Player") && other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            tileObj = other.gameObject;
            Vector3 newPos = other.GetComponent<Renderer>().bounds.center;
            transform.parent.position = new Vector3(newPos.x, newPos.y + 1f, newPos.z);
            transform.parent.SetParent(other.transform);

            if (!other.gameObject.GetComponent<TileProperties>().hasEnemy)
            {
                print("anchormsg");
                gameObject.transform.parent.GetComponent<Player>().isPushBack = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Floor"))
        {
            transform.parent.SetParent(transform);
        }
    }    
}
