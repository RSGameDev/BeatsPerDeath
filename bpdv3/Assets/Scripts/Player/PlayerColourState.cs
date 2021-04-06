using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColourState : MonoBehaviour
{
    [SerializeField] private Material[] materials;
    private float time;
    [SerializeField] private Color defaultColour;
    [SerializeField] private Color hurtColour;
    [SerializeField] private Color warningColour;
    private bool done;
    private bool normal;
    //private float num = 0f;
    float timer = 0f;
    
    private void Update()
    {
        if (done)
        {
            PlayerHurt(0.5f);
        }

        if (normal)
        {
            StartCoroutine(PlayerNormal(1f));
        }
    }

    public void PlayerHit()
    {
        StartCoroutine(PlayerHit(1f));
    }

    IEnumerator PlayerHit(float delay)
    {
        float timer = 0;
        while (timer < delay)
        {
            timer += Time.deltaTime;
            var color = Color.Lerp(defaultColour, hurtColour, timer / delay);
            materials[0].SetColor("Color_5F33D9E9", color);
            materials[1].SetColor("Color_5F33D9E9", color);
            materials[2].SetColor("Color_5F33D9E9", color);
            yield return null;
        }
        done = true;
    }

    void PlayerHurt(float delay)
    {
        timer += Time.deltaTime;
        if (timer > 2f)
        {
            done = false;
            normal = true;
            timer = 0f;
            return;
        }

        var color = Color.Lerp(hurtColour, warningColour, Mathf.PingPong(Time.time, delay));
        materials[0].SetColor("Color_5F33D9E9", color);
        materials[1].SetColor("Color_5F33D9E9", color);
        materials[2].SetColor("Color_5F33D9E9", color);
    }

    IEnumerator PlayerNormal(float delay)
    {
        float timer = 0;
        while (timer < delay)
        {
            timer += Time.deltaTime;
            var color = Color.Lerp(hurtColour, defaultColour, timer / delay);
            materials[0].SetColor("Color_5F33D9E9", color);
            materials[1].SetColor("Color_5F33D9E9", color);
            materials[2].SetColor("Color_5F33D9E9", color);
            yield return null;
        }
        normal = false;
    }
}