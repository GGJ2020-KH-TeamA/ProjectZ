using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public static int active = 0;

    public GameState gameState = GameState.Init;
    public enum GameState
    {
        Init,
        Title,
        TitleToGame,
        GamePrepare,
        Gaming,
        GameTimeup,
        GameFail
    }
    public int gameRound = 0;
    private Vector3 defaultPlayerPosition;

    [Header("Game Managers")]
    public ItemManager itemManager;
    public PlayerControl playerControl;
    public RobotDown robotDown;
    public Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        playerControl.isPlaying = false;
        defaultPlayerPosition = playerControl.transform.position;

        timer.Reset();

        CameraEffect.Instance.MoveTo(new Vector2(0, 0), 0f);
        CameraEffect.Instance.oncolorfinish = ReadyToTitle;
        CameraEffect.Instance.FadeIn(1f);
    }

    private void ReadyToTitle()
    {
        gameState = GameState.Title;
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Title:
                {
                    if (Input.anyKey)
                    {
                        itemManager.enableSpawn = false;
                        itemManager.RemoveAll();

                        gameRound = 0;

                        timer.Reset();

                        playerControl.isPlaying = false;
                        playerControl.Init(new bool[] { true, true, true, true, true, true });
                        playerControl.transform.position = defaultPlayerPosition;

                        gameState = GameState.TitleToGame;
                        CameraEffect.Instance.onposfinish = ReadyToGame;
                        CameraEffect.Instance.MoveTo(new Vector2(0, 0), 1f);
                    }
                    break;
                }
            case GameState.GamePrepare:
                {
                    gameState = GameState.Gaming;
                    break;
                }
            default:
                break;
        }
        
        //if (Input.GetKeyUp(KeyCode.T))
        //{
        //    CameraEffect.Instance.MoveTo(new Vector2(0, -7.68f), 1f);
        //}
    }

    private void ReadyToGame()
    {
        gameState = GameState.GamePrepare;
        gameRound++;

        playerControl.isPlaying = true;
        itemManager.enableSpawn = true;

        timer.Reset();
        timer.StartTimer();
    }

   
}
