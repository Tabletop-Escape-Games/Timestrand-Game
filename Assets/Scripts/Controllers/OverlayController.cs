using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayController : MonoBehaviour
{
    [SerializeField] private float scrollSpeed = 2f;    // Scroll speed should match the camera

    // This controller makes sure that the overlays move up with the camera.
    // TODO Look into merging this controller with the camera controller to deduplicate code
    void Update()
    {
        transform.Translate( Camera.main.transform.up * scrollSpeed * Time.deltaTime);
    }
}
