using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFill : MonoBehaviour
{
    public Timer Timer;
    
    void Update()
    {
        transform.localPosition = new Vector3(-4.5f + 5.6f * (Timer.TimeLeft / Timer.Maxtime), 0.15f, 0);
        transform.localScale = new Vector3(Timer.TimeLeft / Timer.Maxtime, 1, 1);
    }
}
