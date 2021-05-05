using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System;

public class Fire : MonoBehaviour
{
    [SerializeField] Light2D torchlight;                        //Iguanas light
    private float speed;                                                //Speed of ignitation of iguanas light

    private void Awake()
    {
        speed = 1f;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")                //check connection fire object with iguana
        {
            if (torchlight.intensity < 1.5)                      //check iguanas fires intensity
            {
                torchlight.intensity += speed * Time.deltaTime;  //iguanas light intensity raising
            }
            
        }
    }
}
