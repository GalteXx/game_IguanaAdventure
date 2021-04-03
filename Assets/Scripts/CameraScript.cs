using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform target;
    public float speed = 0.125f;
    public Vector3 offset;

    void LateUpdate()
    {
        //Vector3 desiredPosition = target.position + offset;
        Vector3 desiredPosition = new Vector3(target.position.x + offset.x, transform.position.y, -11);
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, speed);
        transform.position = smoothedPosition;
    }
}
