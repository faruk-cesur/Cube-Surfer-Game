using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollect : MonoBehaviour
{
    
    // Using OnTriggerEnter to collect all coins.
     private void OnTriggerEnter(Collider other)
        {
            CollectCube collectCube = other.GetComponentInParent<CollectCube>();
            if (collectCube)
            {
                GameManager.gameManager.goldScore++;
                AudioSource.PlayClipAtPoint(GameManager.gameManager.coinSound,GameManager.gameManager.player.transform.position);
                GameManager.gameManager.goldText.text = GameManager.gameManager.goldScore.ToString();
                Destroy(gameObject);
            }
        }
}
