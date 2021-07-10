using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableCube : MonoBehaviour
{
    private bool _isTriggered;
    private void OnTriggerEnter(Collider other)
    {
        
        CollectCube collectCube = other.GetComponentInParent<CollectCube>();
        if (collectCube && !_isTriggered)
        {
            _isTriggered = true;
            GameManager.gameManager.player.CollectCube();
            Destroy(gameObject);
        }
    }
}