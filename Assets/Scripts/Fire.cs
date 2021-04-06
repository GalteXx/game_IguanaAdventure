using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System;

public class Fire : MonoBehaviour
{
    [SerializeField] Light2D torchlight;
    float speed;

    private void Awake()
    {
        speed = 1f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (torchlight.intensity < 0.8)
            {
                torchlight.intensity += speed * Time.deltaTime;
            }
            
        }
    }
}
