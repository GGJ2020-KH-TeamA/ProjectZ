using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    public Sprite[] sprites;

    void Update()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[GetComponentInParent<PlayerControl>().Item1];
    }
}
