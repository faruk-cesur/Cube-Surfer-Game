using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Camera _camera;
    public PlayerController player;
    public Transform camPos;
    public Transform followerCam;
    public Transform finishCam;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        CamFollow();
    }

    private void CamFollow()
    {
        camPos.position = Vector3.Lerp(camPos.position, player.transform.position, Time.deltaTime*2);

        if (!player.finishCam)
        {
            if (_camera.transform.parent != followerCam)
            {
                _camera.transform.SetParent(followerCam);
            }
        }
        else
        {
            if (_camera.transform.parent != finishCam)
            {
                _camera.transform.SetParent(finishCam);
            }
        }

        _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, Vector3.zero, Time.deltaTime*2);
        _camera.transform.localRotation = Quaternion.Lerp(_camera.transform.localRotation, Quaternion.identity, Time.deltaTime*2);
    }
}