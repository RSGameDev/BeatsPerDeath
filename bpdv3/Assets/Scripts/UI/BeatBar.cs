using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class BeatBar : MonoBehaviour
{
    #region Beat Bar
    [Header("Beat bar")]
    // Beatbar
    public GameObject beatBar;
    public Transform startBar;
    public Transform endBar;
    public GameObject beatMark1;
    public GameObject beatMark2;
    public GameObject beatMark3;
    public GameObject beatMark4;
    private bool newBeats;
    [Range(0f, 3f)] public float totalTime;
    Vector3 direction;
    public float distance;
    public GameObject theCore;
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        direction = startBar.position - endBar.position;
        distance = direction.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        BeatBarBehaviour();
    }
    
    void BeatBarBehaviour()
    {
        if (!BeatManager.Instance.AreBeatsStarted)
        {
            return;
        }

        // The dots in the beat bar scroll along. 
        beatMark1.transform.Translate(direction.normalized * (Time.deltaTime * (distance / totalTime)));
        beatMark2.transform.Translate(direction.normalized * (Time.deltaTime * (distance / totalTime)));
        beatMark3.transform.Translate(direction.normalized * (Time.deltaTime * (distance / totalTime)));
        beatMark4.transform.Translate(direction.normalized * (Time.deltaTime * (distance / totalTime)));

        // When they reach the end they go back to the start position and repeat scrolling.
        if (beatMark1.GetComponent<RectTransform>().anchoredPosition.x >= 230)
        {
            beatMark1.GetComponent<RectTransform>().anchoredPosition = new Vector3(-230, 0.5f, 0);
        }

        if (beatMark2.GetComponent<RectTransform>().anchoredPosition.x >= 230)
        {
            beatMark2.GetComponent<RectTransform>().anchoredPosition = new Vector3(-230, 0.5f, 0);
        }

        if (beatMark3.GetComponent<RectTransform>().anchoredPosition.x >= 230)
        {
            beatMark3.GetComponent<RectTransform>().anchoredPosition = new Vector3(-230, 0.5f, 0);
        }

        if (beatMark4.GetComponent<RectTransform>().anchoredPosition.x >= 230)
        {
            beatMark4.GetComponent<RectTransform>().anchoredPosition = new Vector3(-230, 0.5f, 0);
        }
    }
}
