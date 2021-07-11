using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // All Variables
    
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
    [HideInInspector] public bool finishCam;


    // GetMouseButtonDown = TouchPhase.Began
    // GetMouseButton = TouchPhase.Moved
    // GetMouseButtonUp = TouchPhase.Ended
    
    // Making player movement with touch inputs (Swerve Movement)
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

    // Player moves while main game state.
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
            case GameManager.GameState.GameOver:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    // Player positions upper when collecting a new cube
    public void CollectCube()
    {
        Vector3 upwardsPosition = upwards.localPosition;
        upwardsPosition.y += 0.7f;
        upwards.localPosition = upwardsPosition;
        Instantiate(spawnedCube, cubeSpawner.position, transform.rotation, cubeRoot);
    }
    
    // Player's speed down slowly when reach at finish line
    public void PlayerSpeedDown()
    {
        StartCoroutine(FinishGame());
    }

    // IEnumerator Coroutine to get slow effect
    IEnumerator FinishGame()
    {
        float timer = 0;
        float fixSpeed = playerSpeed;
        while (true)
        {
            timer += Time.deltaTime;
            playerSpeed = Mathf.Lerp(fixSpeed, 0, timer);
            if (timer >= 1f)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
    }
}