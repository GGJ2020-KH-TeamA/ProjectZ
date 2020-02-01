using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject[] gameObjects;
    private float Speed = 0.02f;
    public int ItemID;
    public int Item1 = 8;
    public bool isBlind;
    private int HandCount = 0;

    public delegate void PlayerDelegate();
    public static PlayerDelegate GameOverEvent;
    public static PlayerDelegate WalkEvent;
    public static PlayerDelegate RestEvent;
    /*
    private void Init(bool[] NoPowerGuyState)
    {
        for(int i = 0; i < 6; i ++) gameObjects[i].SetActive(NoPowerGuyState[i]);
        isBlind = NoPowerGuyState[0];
        if (NoPowerGuyState[1]) HandCount++;
        if (NoPowerGuyState[2]) HandCount++;
        if (NoPowerGuyState[3]) Speed += 0.01f;
        if (NoPowerGuyState[4]) Speed += 0.01f;
        if (NoPowerGuyState[5]) GameOverEvent();
    }
    */
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up * Speed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * Speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down * Speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * Speed);
        }
        //Walk();
    }
    /*
    private void Walk()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) WalkEvent();
    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Item")
        {
            ItemID = collision.gameObject.GetComponent<Item>().ID;
            if (Item1 == 8)
            {
                Item1 = ItemID;
                Destroy(collision.gameObject);
            }
        }

        if(collision.tag == "TrashCan")
        {
            Item1 = 8;
        }

        if(collision.tag == "NoPowerGuy")
        {
            //collision.GetComponent<NoPowerGuy>().InterActByPlayer(Item1);
            Item1 = 8;
        }
    }
}
