using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GameManager : MonoBehaviour
{
   public static Camera Cam;
   public static GameManager manager;

   public enum GameState
   {
      Prepare,
      MainGame,
      FinishGame,
   }

   private GameState _currentGameState;
   

   // State geçişlerinde sadece 1 kez çalışan yapı
   public GameState CurrentGameState
   {
      get { return _currentGameState;}
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
      Cam = Camera.main;
      manager = this;
   }

   private void Update()
   {
      switch (CurrentGameState)
      {
         case GameState.Prepare:
            CurrentGameState = GameState.MainGame;
            break;
         case GameState.MainGame:
            CurrentGameState = GameState.FinishGame;
            break;
         case GameState.FinishGame:
            break;
         default:
            throw new ArgumentOutOfRangeException();
      }
   }
}
