using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float swipeValue;

    public Transform upper;
    public Transform cubeSpawnPos;
    public Transform cubeRoot;
    public Transform playerModelRoot;
    public float maxDistanceX;
    private float _dirX;
    private float _mousePosX;

    public GameObject spawnCube;

    private void PlayerMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            float movePow = touch.deltaPosition.normalized.x;
            Debug.Log(touch.position);
        }
        
        Vector3 dir = transform.forward * Time.deltaTime * speed;
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 playerPos = playerModelRoot.localPosition;
            _dirX = playerPos.x;
            _mousePosX = GameManager.Cam.ScreenToViewportPoint(Input.mousePosition).x;
        }

        if (Input.GetMouseButton(0))
        {
            float newMousePosX = GameManager.Cam.ScreenToViewportPoint(Input.mousePosition).x;
            float distance = newMousePosX - _mousePosX;
            float posX = _dirX + (distance * swipeValue);
            Vector3 pos = playerModelRoot.localPosition;
            pos.x = posX;
            pos.x = Mathf.Clamp(pos.x, -maxDistanceX, maxDistanceX);
            playerModelRoot.localPosition = pos;
        }
        transform.Translate(dir);
    }

    private void Update()
    {
        switch (GameManager.manager.CurrentGameState)
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
        Vector3 pos = upper.localPosition;
        pos.y += 1;
        upper.localPosition = pos;
        Instantiate(spawnCube, cubeSpawnPos.position, transform.rotation, cubeRoot);
    }
}