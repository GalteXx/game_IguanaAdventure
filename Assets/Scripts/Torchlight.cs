using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System;

public class Torchlight : MonoBehaviour
{
    [SerializeField] Light2D torchlight;
    public float fadeoutSpeed;
    void Start()
    {
        
        fadeoutSpeed = 0.05f;
    }

    void Update()
    {
        if (torchlight.intensity >= 0)
        {
            torchlight.intensity -= fadeoutSpeed * Time.deltaTime;
        }
        
    }
}
