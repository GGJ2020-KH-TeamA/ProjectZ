using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl Instance { get; private set; }

    public AudioManager audioManager;
    public ItemManager itemManager;
    public PlayerSpriteController playerSpriteController;
    private GameObject RobotDown;

    public bool[] MyStates;

    private float Speed = 3f;
    public int ItemID;
    public int Item1 = 8;
    public int Item2 = 8;
    public bool isBlind = false;
    public int HandCount = 0;
    public bool isMoving { get; private set; }
    public Vector2 direction { get; private set; }
    public bool isPlaying = true;
    private int BreakProbability = 50;

    private Rigidbody2D rigidbody;

    public delegate void PlayerDelegate();
    public static PlayerDelegate GameOverEvent;

    void Awake()
    {
        Instance = this;
        rigidbody = GetComponent<Rigidbody2D>();
        MyStates = new bool[] { true, true, true, true, true, true };
    }

    public void RoundEnd()
    {
        RobotDown = GameObject.FindGameObjectWithTag("RobotDown");

        Remain();

        Vector3 tmpPosition = RobotDown.transform.position;
        RobotDown.transform.position = transform.position;
        transform.position = tmpPosition;

        bool[] tmpState = RobotDown.GetComponent<RobotDown>().GetStateData();
        RobotDown.GetComponent<RobotDown>().Init(MyStates);
        MyStates = tmpState;

        Init(MyStates);
    }


    public void Init(bool[] parts)
    {
        Speed = 3f;
        HandCount = 0;

        isBlind = parts[0];
        if (parts[1]) HandCount++;
        if (parts[2]) HandCount++;
        if (parts[3]) Speed += 1f;
        if (parts[4]) Speed += 1f;
        if (!parts[5]) isPlaying = false;

        playerSpriteController.SetPart(parts);

        MyStates = parts;
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

    void Update()
    {
        Walk();
    }

    private void Walk()
    {
        Vector2 movement = Vector2.zero;

        if (isPlaying)
        {
            if (Input.GetKey(KeyCode.W)) movement += Vector2.up;
            if (Input.GetKey(KeyCode.A)) movement += Vector2.left;
            if (Input.GetKey(KeyCode.S)) movement += Vector2.down;
            if (Input.GetKey(KeyCode.D)) movement += Vector2.right;
        }

        if(movement == Vector2.zero)
        {
            if (isMoving)
            {
                isMoving = false;
                audioManager.Pause("walk");
            }
            rigidbody.velocity = Vector2.zero;
        }
        else
        {
            if (!isMoving)
            {
                isMoving = true;
                audioManager.Play("walk", true);
            }
            rigidbody.velocity = GetMovement(movement) * Speed;
        }



        direction = movement;
    }

    private Vector2 GetMovement(Vector2 dir)
    {
        float angle = Vector2.SignedAngle(Vector2.right, dir);
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isPlaying)
        {
            if (collision.tag == "Item")
            {
                ItemController itemController = collision.gameObject.GetComponent<ItemController>();
                if (itemController)
                {
                    ItemID = itemController.ID;
                    if (Item1 == 8 && HandCount >= 1)
                    {
                        Item1 = ItemID;
                        itemManager.PickItem(itemController);
                        audioManager.Play("pick");
                    }
                    else if (Item2 == 8 && HandCount >= 2)
                    {
                        Item2 = ItemID;
                        itemManager.PickItem(itemController);
                        audioManager.Play("pick");
                    }
                }
            }

            if (collision.tag == "TrashCan")
            {
                Item1 = 8;
                Item2 = 8;
                audioManager.Play("trash");
            }

            if (collision.tag == "RobotDown")
            {
                bool gived = false;
                if (collision.GetComponent<RobotDown>().InterActByPlayer(Item1))
                {
                    Item1 = Item2;
                    Item2 = 8;
                    gived = true;
                }
                if (collision.GetComponent<RobotDown>().InterActByPlayer(Item2))
                {
                    Item2 = 8;
                    gived = true;
                }

                if (gived)
                    audioManager.Play("give");
            }
        }
    }
}
