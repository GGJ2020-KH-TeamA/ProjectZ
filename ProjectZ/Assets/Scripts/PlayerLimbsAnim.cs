using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLimbsAnim : MonoBehaviour
{
    private Animator animator;

    private void OnEnable()
    {
        PlayerControl.WalkEvent += Walk;
        PlayerControl.WalkEvent += Rest;
    }
    private void OnDisable()
    {
        PlayerControl.WalkEvent -= Walk;
        PlayerControl.WalkEvent -= Rest;
    }

    private void Start()
    {
        //animator = GetComponent<Animator>();
    }

    private void Walk()
    {

    }

    private void Rest()
    {

    }
}
