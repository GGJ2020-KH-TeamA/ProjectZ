using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimesUpEffect : MonoBehaviour
{
    public PlayerControl playerControl;
    public bool Test;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Test)
        {
            PlayEffect();
            Test = false;
        }
    }

    public void PlayEffect()
    {
        CameraEffect.Instance.FadeOut(1f);
        playerControl.isPlaying = false;
        playerControl.RoundEnd();
        playerControl.isPlaying = true;
        CameraEffect.Instance.FadeIn(1f);
    }
}
