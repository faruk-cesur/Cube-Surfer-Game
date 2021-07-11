using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CameraFollow : MonoBehaviour
{
    
    // Variables
    private Camera _camera;
    public PlayerController player;
    public Transform camPos;
    public Transform followerCam;
    public Transform finishCam;
    public Transform playerModel;

    // _camera will keep the reference for Camera.main
    private void Awake()
    {
        _camera = Camera.main;
    }

    
    // Camera is following frames per second
    private void Update()
    {
        CamFollow();
    }

    // Camera is following the player while in MainGame State
    private void CamFollow()
    {
        camPos.position = Vector3.Lerp(camPos.position, player.transform.position, Time.deltaTime*2);

        if (!player.finishCam)
        {
            if (_camera.transform.parent != followerCam)
            {
                _camera.transform.SetParent(followerCam);
            }
            _camera.transform.localRotation = Quaternion.Lerp(_camera.transform.localRotation, Quaternion.identity, Time.deltaTime*2);
        }
        // Camera is changed to finished cam position
        else
        {
            if (_camera.transform.parent != finishCam)
            {
                _camera.transform.SetParent(finishCam);
            }

            _camera.transform.localRotation = Quaternion.Euler(Vector3.zero);
            finishCam.transform.LookAt(playerModel.transform);
        }

        _camera.transform.localPosition = Vector3.Lerp(_camera.transform.localPosition, Vector3.zero, Time.deltaTime*2);
        
        
    }
}