using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float swerveValue;
    public float clampDistanceX;
    public Transform upwards;
    public Transform cubeSpawner;
    public Transform cubeRoot;
    public Transform playerModelRoot;
    public GameObject spawnedCube;
    private Touch touch;
    private Vector2 myTouchPosition;


    // GetMouseButtonDown = TouchPhase.Began
    // GetMouseButton = TouchPhase.Moved
    // GetMouseButtonUp = TouchPhase.Ended
    private void PlayerMovement()
    {
        Vector3 runForward = transform.forward * Time.deltaTime * playerSpeed;

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                myTouchPosition = touch.deltaPosition;

                if (myTouchPosition.x > 0)
                {
                    playerModelRoot.localPosition += new Vector3(swerveValue, 0, 0);
                }
                else
                {
                    playerModelRoot.localPosition += new Vector3(-swerveValue, 0, 0);
                }

                Vector3 playerPosition = playerModelRoot.localPosition;
                playerPosition.x = Mathf.Clamp(playerPosition.x, -clampDistanceX, clampDistanceX);
                playerModelRoot.localPosition = playerPosition;
            }
        }

        transform.Translate(runForward);
    }

    private void Update()
    {
        switch (GameManager.gameManager.CurrentGameState)
        {
            case GameManager.GameState.Prepare:
                break;
            case GameManager.GameState.MainGame:
                PlayerMovement();
                break;
            case GameManager.GameState.FinishGame:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void CollectCube()
    {
        Vector3 upwardsPosition = upwards.localPosition;
        upwardsPosition.y += 1;
        upwards.localPosition = upwardsPosition;
        Instantiate(spawnedCube, cubeSpawner.position, transform.rotation, cubeRoot);
    }
}