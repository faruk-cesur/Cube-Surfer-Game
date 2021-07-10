using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCollect : MonoBehaviour
{
     private void OnTriggerEnter(Collider other)
        {
            CollectCube collectCube = other.GetComponentInParent<CollectCube>();
            if (collectCube)
            {
                GameManager.gameManager.goldScore++;
                GameManager.gameManager.goldText.text = GameManager.gameManager.goldScore.ToString();
                Destroy(gameObject);
            }
        }
}
