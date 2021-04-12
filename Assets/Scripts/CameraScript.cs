using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Target of following
    public Transform target;
    // Speed of teleporting 
    public float speed = 0.125f;
    // Offset of the camera
    public Vector3 offset;

    void LateUpdate()
    {
        // Camera follow script with a smooth offset and speed
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, transform.position.y, -11);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed);
        transform.position = smoothedPosition;
    }
}
