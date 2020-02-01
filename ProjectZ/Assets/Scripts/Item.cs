using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int ID;
    private int Probability;
    private int HeadProbability = 18;
    private int HandProbability = 36;
    private int LegProbability = 54;
    private int BatteryProbability = 72;
    private int WeldGunProbability = 81;
    private int WrenchProbability = 90;
    private int GlueProbability = 99;
    
    public Sprite[] sprites;

    void Awake()
    {
        GetID();
        GetComponent<SpriteRenderer>().sprite = sprites[ID];
    }

    private void GetID()
    {
        Probability = Random.Range(0, 100);
        if (Probability < HeadProbability) ID = 0;
        else if (Probability < HandProbability) ID = 1;
        else if (Probability < LegProbability) ID = 2;
        else if (Probability < BatteryProbability) ID = 3;
        else if (Probability < WeldGunProbability) ID = 4;
        else if (Probability < WrenchProbability) ID = 5;
        else if (Probability < GlueProbability) ID = 6;
        else ID = 7;
    }
}
