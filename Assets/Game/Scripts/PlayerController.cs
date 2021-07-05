using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float swipeValue;
    public Transform playerModelRoot;
    public float maxDistanceX;
    private float _dirX;
    private float _mousePosX;

    private void PlayerMovement()
    {
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
}