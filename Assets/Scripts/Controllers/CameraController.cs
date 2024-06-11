using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 2f; // Speed at which the camera scrolls

    void FixedUpdate()
    {
        // Move the camera upwards over time
        var cameraPos = transform;
        Vector3 newPosition = cameraPos.position + Vector3.up * (scrollSpeed * Time.deltaTime);
        cameraPos.position = newPosition;
    }
}
