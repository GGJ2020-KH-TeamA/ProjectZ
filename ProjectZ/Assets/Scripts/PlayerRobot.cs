using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRobot : MonoBehaviour
{
    public GameObject[] Limbs;
    
    public void LoadLimbs(bool[] States)
    {
        for(int i = 0; i < 6; i++)
        {
            Limbs[i].SetActive(States[i]);
        }
    }
}
