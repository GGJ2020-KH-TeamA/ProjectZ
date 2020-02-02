using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PobotDownNeedDialog : MonoBehaviour
{
    public RobotDown RobotDown;
    public GameObject Need1;
    private Sprite Need1Sprite;
    private bool[] NeedItem;
    private float Timer;
    public Sprite[] sprites;
    private int Count;
    private int MaxCount;

    private void Start()
    {
        Need1Sprite = Need1.GetComponent<SpriteRenderer>().sprite;
    }
    
    void Update()
    {
        //Timer += Time.deltaTime;
        NeedItem = RobotDown.GetEatable();
        ReflashSprite();
    }

    private void ReflashSprite()
    {
        for (int i = 0; i <= NeedItem.Length; i++)
        {
            if (NeedItem[i])
            {
                Need1Sprite = sprites[i];
                break;
            }
        }
        
    }
}
