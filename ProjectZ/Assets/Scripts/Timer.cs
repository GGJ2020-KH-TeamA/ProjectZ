using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Image TimerBar;
    public float Maxtime = 5f;
    float TImeLeft;
    


    // Start is called before the first frame update
    void Start()
    {
        TimerBar = GetComponent<Image>();
        TImeLeft = Maxtime;
    }

    // Update is called once per frame
    void Update()
    {
        if (TImeLeft > 0)
        {
            TImeLeft -= Time.deltaTime;
            TimerBar.fillAmount = TImeLeft / Maxtime;
        }

        else
        {
            Time.timeScale = 0;
        }
  
    }
}
