using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Image TimerBar;
    public float Maxtime = 5f;
    float TimeLeft;
    
    public TimesUpEffect timesUpEffect;
    
    void Start()
    {
        TimerBar = GetComponent<Image>();
        TimeLeft = Maxtime;
    }
    
    void Update()
    {
        if (TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;
            TimerBar.fillAmount = TimeLeft / Maxtime;
        }
        else
        {
            timesUpEffect.PlayEffect();
        }
    }
}
