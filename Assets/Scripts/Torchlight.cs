using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System;

public class Torchlight : MonoBehaviour
{
    [SerializeField] Light2D globalLight;                             
    [SerializeField] Light2D torchlight;                              //Iguanas light
    [SerializeField] float fadeoutSpeed;                              // burning speed of iguanas light
    [SerializeField] float globalfadeoutSpeed;                        // burning speed of global light
    void Start()
    {
        globalfadeoutSpeed = 0.03f;
        fadeoutSpeed = 0.2f;
    }

    void Update()
    {
        if (torchlight.intensity < 0.25)                            //down the brightness of global light when torchlight itensity comes lower than 0.25
        {
            while (globalLight.intensity > 0)
            {
                globalLight.intensity -= Time.deltaTime * globalfadeoutSpeed;
            }
        }
        if (torchlight.intensity > 0.25)                            //make the brightness of global light back when torchlight itensity more than 0.25
        {
            globalLight.intensity = 0.1f;
        }
        if (torchlight.intensity >= 0)                                //check iguanas fires intensity (if it is more than 0, light will fade out)
        {
            torchlight.intensity -= fadeoutSpeed * Time.deltaTime;    //iguanas light intensity burning
        }
        
    }
}
