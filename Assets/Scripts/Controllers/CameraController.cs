using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Tooltip("The speed with which the camera will move upward")]
    [SerializeField] private float _scrollSpeed = 2f; // Speed at which the camera scrolls

    private void Start()
    {
        Settings.scrollSpeed = _scrollSpeed;
    }

    void FixedUpdate()
    {
        // Move the camera upwards over time
        var cameraPos = transform;
        Vector3 newPosition = cameraPos.position + Vector3.up * (Settings.scrollSpeed * Time.deltaTime);
        cameraPos.position = newPosition;
    }
}
