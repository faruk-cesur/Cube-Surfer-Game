using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCube : MonoBehaviour
{
    private bool _isTriggered;
    
    // Using OnTriggerEnter to collect all cubes.
    private void OnTriggerEnter(Collider other)
    {
        
        CollectCube collectCube = other.GetComponentInParent<CollectCube>();
        //I'm using an if check with a bool variable to make sure one cube don't trigger twice.
        if (collectCube && !_isTriggered)
        {
            _isTriggered = true;
            AudioSource.PlayClipAtPoint(GameManager.gameManager.jumpSound,GameManager.gameManager.player.transform.position);
            GameManager.gameManager.player.CollectCube();
            Destroy(gameObject);
        }
    }
}