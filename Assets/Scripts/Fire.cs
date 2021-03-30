using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System;

public class Fire : MonoBehaviour
{
    [SerializeField] Light2D torchlight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            torchlight.intensity = 1;
        }
    }
}
