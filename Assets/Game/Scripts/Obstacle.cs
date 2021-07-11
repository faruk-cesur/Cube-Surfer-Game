using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public PlayerController player;
    private bool _isTriggered;

    // Using enum state for obstacles
    public enum ObstacleType
    {
        Cube,
        Pool
    }

    public ObstacleType currentObstacleType;

    // Doing things when we trigger with an obstacle according to its type
    private void OnTriggerEnter(Collider other)
    {
        CollectCube collectCube = other.gameObject.GetComponentInParent<CollectCube>();

        if (currentObstacleType == ObstacleType.Pool)
        {
            if (collectCube)
            {
                switch (currentObstacleType)
                {
                    case ObstacleType.Cube:
                        break;
                    case ObstacleType.Pool:
                        AudioSource.PlayClipAtPoint(GameManager.gameManager.fireSound,GameManager.gameManager.player.transform.position);
                        Destroy(collectCube.gameObject);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        if (currentObstacleType == ObstacleType.Cube)
        {
            if (collectCube && !_isTriggered)
            {
                _isTriggered = true;
                switch (currentObstacleType)
                {
                    case ObstacleType.Cube:
                        AudioSource.PlayClipAtPoint(GameManager.gameManager.fallSound,player.transform.position);
                        Destroy(collectCube.gameObject);
                        break;
                    case ObstacleType.Pool:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}