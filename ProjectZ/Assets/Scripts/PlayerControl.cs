﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance { get; private set; }

    public GameObject[] Robots;
    private GameObject RobotDown;

    private bool[] MyStates;

    private float Speed = 3f;
    public int ItemID;
    public int Item1 = 8;
    public int Item2 = 8;
    public bool isBlind = false;
    private int HandCount = 0;
    public bool isMoving { get; private set; }
    public Vector2 direction { get; private set; }
    public bool isPlaying = true;
    private int BreakProbability = 50;

    private Rigidbody2D rigidbody;

    public delegate void PlayerDelegate();
    public static PlayerDelegate GameOverEvent;
    public static PlayerDelegate WalkEvent;
    public static PlayerDelegate RestEvent;

    private void RoundEnd()
    {
        RobotDown = GameObject.FindGameObjectWithTag("RobotDown");

        Vector3 tmpPosition = RobotDown.transform.position;
        RobotDown.transform.position = transform.position;
        transform.position = tmpPosition;

        bool[] tmpState = RobotDown.GetComponent<RobotDown>().GetStateData();
        RobotDown.GetComponent<RobotDown>().Init(MyStates);
        MyStates = tmpState;
    }

    private void Init(bool[] RobotDown)
    {
        isBlind = RobotDown[0];
        if (RobotDown[1]) HandCount++;
        if (RobotDown[2]) HandCount++;
        if (RobotDown[3]) Speed += 1f;
        if (RobotDown[4]) Speed += 1f;
        if (!RobotDown[5])
        {
            GameOverEvent();
            isPlaying = false;
        }
        GetComponent<PlayerRobot>().LoadLimbs(RobotDown);
        MyStates = RobotDown;
    }

    public bool[] Remain()
    {
        //BreakProbability = 5 * 關卡數;
        for(int i = 0; i < MyStates.Length; i++)
        {
            if (MyStates[i] && Random.Range(0, 100) < BreakProbability) MyStates[i] = false;
        }

        return MyStates;
    }

    void Awake()
    {
        Instance = this;
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(isPlaying) Walk();
    }
    
    private void Walk()
    {
        Vector2 movement = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) movement += Vector2.up;
        if (Input.GetKey(KeyCode.A)) movement += Vector2.left;
        if (Input.GetKey(KeyCode.S)) movement += Vector2.down;
        if (Input.GetKey(KeyCode.D)) movement += Vector2.right;
        
        if(movement == Vector2.zero)
        {
            if (isMoving)
            {
                // RestEvent();
                isMoving = false;
            }
            rigidbody.velocity = Vector2.zero;
        }
        else
        {
            if (!isMoving)
            {
                // WalkEvent();
                isMoving = true;
            }
            //transform.position += GetMovement(movement) * Speed * Time.deltaTime;
            rigidbody.velocity = GetMovement(movement) * Speed;
        }

        

        direction = movement;
    }

    private Vector2 GetMovement(Vector2 dir)
    {
        float angle = Vector2.SignedAngle(Vector2.right, dir);
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isPlaying)
        {
            if (collision.tag == "Item")
            {
                ItemController itemController = collision.gameObject.GetComponent<ItemController>();
                if (itemController)
                {
                    ItemID = itemController.ID;
                    if (Item1 == 8)
                    {
                        Item1 = ItemID;
                        ItemManager.Instance.PickItem(itemController);
                    }
                    else if (Item2 == 8)
                    {
                        Item2 = ItemID;
                        ItemManager.Instance.PickItem(itemController);
                    }
                }
            }

            if (collision.tag == "TrashCan")
            {
                Item1 = 8;
                Item2 = 8;
            }

            if (collision.tag == "RobotDown")
            {
                if (collision.GetComponent<RobotDown>().InterActByPlayer(Item1))
                {
                    Item1 = Item2;
                    Item2 = 8;
                }
                if (collision.GetComponent<RobotDown>().InterActByPlayer(Item2))
                {
                    Item2 = 8;
                }
            }
        }
    }
}
