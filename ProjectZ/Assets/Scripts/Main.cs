using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CameraEffect.Instance.MoveTo(new Vector2(0, 0), 0f);
        CameraEffect.Instance.FadeIn(1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            CameraEffect.Instance.MoveTo(new Vector2(0, 0), 1f);
        }
        if (Input.GetKeyUp(KeyCode.T))
        {
            CameraEffect.Instance.MoveTo(new Vector2(0, -7.68f), 1f);
        }
    }
}
