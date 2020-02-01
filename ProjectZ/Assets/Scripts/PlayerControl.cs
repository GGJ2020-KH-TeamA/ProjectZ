using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public GameObject[] Robots;

    private float Speed = 3f;
    public int ItemID;
    public int Item1 = 8;
    public bool isBlind;
    private int HandCount = 0;
    private bool isMoving = false;
    private bool isPlaying = true;

    public delegate void PlayerDelegate();
    public static PlayerDelegate GameOverEvent;
    public static PlayerDelegate WalkEvent;
    public static PlayerDelegate RestEvent;

    private void Init(bool[] NoPowerGuyState)
    {
        isBlind = NoPowerGuyState[0];
        if (NoPowerGuyState[1]) HandCount++;
        if (NoPowerGuyState[2]) HandCount++;
        if (NoPowerGuyState[3]) Speed += 0.01f;
        if (NoPowerGuyState[4]) Speed += 0.01f;
        if (!NoPowerGuyState[5])
        {
            GameOverEvent();
            isPlaying = false;
        }
        GetComponent<PlayerRobot>().LoadLimbs(NoPowerGuyState);
    }

    void Update()
    {
        if(isPlaying) Walk();
    }
    
    private void Walk()
    {
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            movement += Vector2.up;
            for(int i = 0; i < 4; i++)
            {
                if (i == 0) Robots[i].SetActive(true);
                else Robots[i].SetActive(false);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement += Vector2.left;
            for (int i = 0; i < 4; i++)
            {
                if (i == 1) Robots[i].SetActive(true);
                else Robots[i].SetActive(false);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            movement += Vector2.down;
            for (int i = 0; i < 4; i++)
            {
                if (i == 2) Robots[i].SetActive(true);
                else Robots[i].SetActive(false);
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            movement += Vector2.right;
            for (int i = 0; i < 4; i++)
            {
                if (i == 3) Robots[i].SetActive(true);
                else Robots[i].SetActive(false);
            }
        }
        
        if(movement == Vector2.zero)
        {
            if (isMoving) // RestEvent();
            isMoving = false;
        }
        else
        {
            if (!isMoving) WalkEvent();
            isMoving = true;
            transform.position += new Vector3(movement.x,movement.y) * Speed * Time.deltaTime;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlaying)
        {
            if (collision.tag == "Item")
            {
                ItemID = collision.gameObject.GetComponent<Item>().ID;
                if (Item1 == 8)
                {
                    Item1 = ItemID;
                    Destroy(collision.gameObject);
                }
            }

            if (collision.tag == "TrashCan")
            {
                Item1 = 8;
            }

            if (collision.tag == "RobotDown")
            {
                collision.GetComponent<RobotDown>().InterActByPlayer(Item1);
                Item1 = 8;
            }
        }
    }
}
