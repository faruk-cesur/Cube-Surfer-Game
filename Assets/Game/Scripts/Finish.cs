using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    
    // Using OnTriggerEnter to change game state when player arrives finish line
    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        if (player)
        {
            player.finishCam = true;
            player.PlayerSpeedDown();
            AudioSource.PlayClipAtPoint(GameManager.gameManager.winSound,GameManager.gameManager.player.transform.position,0.5f);
            GameManager.gameManager.winGoldText.text = GameManager.gameManager.goldText.text;
            GameManager.gameManager.CurrentGameState = GameManager.GameState.FinishGame;
        }
    }
}
