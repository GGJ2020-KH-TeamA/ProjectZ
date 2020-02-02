using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float Maxtime = 20f;
    public float TimeLeft;
    
    public TimesUpEffect timesUpEffect;
    
    void Start()
    {
        TimeLeft = Maxtime;
    }
    
    void Update()
    {
        if (TimeLeft > 0)
        {
            TimeLeft -= Time.deltaTime;
        }
        else
        {
            //timesUpEffect.PlayEffect();
        }
    }
}
