using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public PlayerController player;
    public Text goldText;
    public Text tapToStart;
    public GameObject gold;
    [HideInInspector]public int goldScore;
    private GameState _currentGameState;
    
    
    public enum GameState
    {
        Prepare,
        MainGame,
        FinishGame,
    }
    
    // State geçişlerinde sadece 1 kez çalışan yapı
    public GameState CurrentGameState
    {
        get { return _currentGameState; }
        set
        {
            switch (value)
            {
                case GameState.Prepare:
                    break;
                case GameState.MainGame:
                    break;
                case GameState.FinishGame:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            _currentGameState = value;
        }
    }

    private void Awake()
    {
        gameManager = this;
    }
    

    private void Update()
    {
        switch (CurrentGameState)
        {
            case GameState.Prepare:
                goldScore = 0;
                tapToStart.enabled = true;
                if (Input.GetMouseButtonDown(0))
                {
                    CurrentGameState = GameState.MainGame;
                }
                break;
            case GameState.MainGame:
                tapToStart.enabled = false;
                break;
            case GameState.FinishGame:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}