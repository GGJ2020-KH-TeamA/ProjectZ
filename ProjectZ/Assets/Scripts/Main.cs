﻿using System;
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
        GameRoundClear,
        GameFail
    }
    public int gameRound = 0;
    private Vector3 defaultPlayerPosition;

    [Header("Game Managers")]
    public ItemManager itemManager;
    public PlayerControl playerControl;
    public RobotDown robotDown;
    public ConveryorMover converyorMover;
    public AudioManager audioManager;
    public Timer timer;

    public float roundClearTimer = 0f;

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

                        robotDown.Init(new bool[] { true, false, true, true, true, true });

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
            case GameState.Gaming:
                {
                    GamingLogic();
                    break;
                }
            case GameState.GameRoundClear:
                {
                    if (roundClearTimer > 0)
                    {
                        roundClearTimer -= Time.deltaTime;
                        if (roundClearTimer <= 0)
                        {
                            CameraEffect.Instance.oncolorfinish = NextRoundPrepare;
                            CameraEffect.Instance.FadeOut(1.5f);
                            Debug.Log("Fade Out");
                        }
                    }
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

    private void NextRoundPrepare()
    {
        gameRound++;

        Vector3 tmpPosition = robotDown.transform.position;
        robotDown.transform.position = playerControl.transform.position;
        playerControl.transform.position = tmpPosition;

        playerControl.Remain();

        bool[] tmpState = robotDown.GetStateData();
        robotDown.Init(playerControl.MyStates);
        playerControl.Init(tmpState);

        timer.Reset();
        itemManager.enableSpawn = true;

        converyorMover.speed = 2;

        Debug.Log(gameRound + " Round");

        CameraEffect.Instance.oncolorfinish = GoNextRound;
        CameraEffect.Instance.FadeIn(1f);
    }

    private void GoNextRound()
    {
        playerControl.isPlaying = true;
        
        timer.StartTimer();

        gameState = GameState.Gaming;
    }

    private void GamingLogic()
    {
        // Check Is All Fix
        if (robotDown.IsAllFix)
        {
            playerControl.isPlaying = false;
            timer.StopTimer();
            roundClearTimer = 3f;
            gameState = GameState.GameRoundClear;
            
            return;
        }

        // Check Timesup
        if (!timer.active)
        {
            playerControl.isPlaying = false;

            if (robotDown.IsCanPlayNextRound)
            {
                roundClearTimer = 3f;
                gameState = GameState.GameRoundClear;
            }
            else
            {
                gameState = GameState.GameFail;
            }
            return;
        }
    }

    private void ReadyToGame()
    {
        gameState = GameState.GamePrepare;
        gameRound++;

        playerControl.isPlaying = true;
        itemManager.enableSpawn = true;

        timer.Reset();
        timer.StartTimer();

        converyorMover.speed = 2;

        Debug.Log("First Round");
    }

   
}
