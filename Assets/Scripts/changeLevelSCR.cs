using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeLevelSCR : MonoBehaviour
{
    [SerializeField] GameObject activeLevel;                          //the level at which iguana is located

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))                 //check connection levels collider object with iguana
            activeLevel.SetActive(true);                    //if iguana enter the levels box collider, we make this level active
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))                 //check connection levels collider object with iguana
            activeLevel.SetActive(false);                   //if iguana exit the levels box collider, we make this level not active
        
    }
    
    
}
