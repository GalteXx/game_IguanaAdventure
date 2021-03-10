using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeLevelSCR : MonoBehaviour
{

    

    public GameObject activeLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activeLevel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            activeLevel.SetActive(false);
        }
    }
    
    
}
