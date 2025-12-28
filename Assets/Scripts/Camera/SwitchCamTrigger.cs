using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class SwitchCamTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera _camera;
    public CinemachineVirtualCamera _mainCamera;

    public UnityEvent onPlayerEnter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (onPlayerEnter != null)
            {
                onPlayerEnter.Invoke();
            }
            CameraManager.SwitchCamera(_camera);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraManager.SwitchCamera(_mainCamera);
        }
    }
}
