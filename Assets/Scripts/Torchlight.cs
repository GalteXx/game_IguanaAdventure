using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System;

public class Torchlight : MonoBehaviour
{
    [SerializeField] Light2D torchlight;                              //Iguanas light
    [SerializeField] float fadeoutSpeed;                                        // burning speed of iguanas light
    void Start()
    {
        
        fadeoutSpeed = 0.05f;
    }

    void Update()
    {
        if (torchlight.intensity >= 0)                                //check iguanas fires intensity (if it is more than 0, light will fade out)
        {
            torchlight.intensity -= fadeoutSpeed * Time.deltaTime;    //iguanas light intensity burning
        }
        
    }
}
