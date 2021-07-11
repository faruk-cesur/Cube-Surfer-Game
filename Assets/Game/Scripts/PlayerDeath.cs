using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public PlayerController player;
    
    // When the player collides the ground with bare feet, he falls to the ground
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            player.finishCam = true;
            player.PlayerSpeedDown();
            AudioSource.PlayClipAtPoint(GameManager.gameManager.gameoverSound,player.transform.position);
            GameManager.gameManager.CurrentGameState = GameManager.GameState.GameOver;
        }
    }
}
