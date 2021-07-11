using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
    // Singleton
    public static GameManager gameManager;
    
    // All Variables in GameManager
    
    public PlayerController player;
    public Text goldText;
    public Text tapToStart;
    public Text winGoldText;
    [HideInInspector]public int goldScore;
    private GameState _currentGameState;
    public GameObject deathScreen;
    public GameObject winScreen;
    public Animator animator;
    public AudioClip startSound;
    public AudioClip coinSound;
    public AudioClip jumpSound;
    public AudioClip fallSound;
    public AudioClip fireSound;
    public AudioClip gameoverSound;
    public AudioClip winSound;
    
    // Using Game State For Functionality
    public enum GameState
    {
        Prepare,
        MainGame,
        FinishGame,
        GameOver
    }
    
    // Using extra switch for game state to run one time codes.
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
                    AudioSource.PlayClipAtPoint(startSound,player.transform.position);
                    break;
                case GameState.FinishGame:
                    break;
                case GameState.GameOver:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(value), value, null);
            }

            _currentGameState = value;
        }
    }
    
    // Singleton
    private void Awake()
    {
        gameManager = this;
    }
    
    // Doing things in update when game state changes
    private void Update()
    
    {
        switch (CurrentGameState)
        {
            case GameState.Prepare:
                winScreen.SetActive(false);
                deathScreen.SetActive(false);
                goldScore = 0;
                tapToStart.enabled = true;
                if (Input.GetMouseButtonDown(0))
                {
                    CurrentGameState = GameState.MainGame;
                }
                break;
            case GameState.MainGame:
                animator.SetBool("MainGame",true);
                tapToStart.enabled = false;
                break;
            case GameState.FinishGame:
                animator.SetBool("MainGame",false);
                animator.SetBool("FinishGame",true);
                winScreen.SetActive(true);
                break;
            case GameState.GameOver:
                animator.SetBool("MainGame",false);
                animator.SetBool("Death",true);
                deathScreen.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    // Reloads the same scene. Using with button
    public void Retry()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    // Loads the next scene. Using with button
    public void NextLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        
        if (SceneManager.sceneCountInBuildSettings > currentScene.buildIndex+1)
        {
            SceneManager.LoadScene(currentScene.buildIndex+1);
        }
        else if (SceneManager.sceneCountInBuildSettings <= currentScene.buildIndex+1)
        {
            return;
        }
    }
}