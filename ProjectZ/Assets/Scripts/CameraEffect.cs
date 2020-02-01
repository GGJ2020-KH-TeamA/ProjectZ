﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffect : MonoBehaviour
{

    public static CameraEffect Instance { get; private set; }

    public GameObject blockScreen;
    public SpriteRenderer fadeSprite;

    private Color from;
    private Color to;

    private float timer;
    private float duration;

    private Vector2 frompos;
    private Vector2 topos;
    private float timerpos;
    private float durationpos;

    void Awake()
    {
        fadeSprite.gameObject.SetActive(true);
        fadeSprite.color = Color.black;

        blockScreen.SetActive(true);

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = Mathf.Clamp01(timer / duration);
            fadeSprite.color = Color.Lerp(from, to, progress);
        }

        if (timerpos < durationpos)
        {
            timerpos += Time.deltaTime;
            float progress = Mathf.Clamp01(timerpos / durationpos);
            Vector2 pos = Vector2.Lerp(frompos, topos, easeInOutQuad(progress, 0, 1, 1));
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
    }

    public void FadeIn(float time)
    {
        timer = 0;
        duration = time;
        from = fadeSprite.color;
        to = Color.clear;
    }

    public void FadeOut(float time)
    {
        timer = 0;
        duration = time;
        from = fadeSprite.color;
        to = Color.black;
    }

    public void MoveTo(Vector2 pos, float time)
    {
        durationpos = time;
        timerpos = 0;
        frompos = new Vector2(transform.position.x, transform.position.y);
        topos = pos;
    }

    float easeInOutQuad(float t, float b, float c, float d) {
        if ((t/=d/2) < 1) return c/2*t* t + b;
        return -c/2 * ((--t)*(t-2) - 1) + b;
    }
}
