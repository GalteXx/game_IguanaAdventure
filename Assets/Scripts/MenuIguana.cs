using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuIguana : MonoBehaviour
{
    public bool menuWalk; // flag
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    { 
        transform.position = new Vector3(-5.5f, -3, 1);
        menuWalk = false;
    }

    // Update is called once per frame
    void Update()
    {
        // checking 2 conditions for character movement
        if (menuWalk)
        {
            transform.position += new Vector3(0.15f, 0, 0); // character movement
        }
        if (transform.position.x >= 11)
        {
            menuWalk = false; // turning off flag
            canvas.GetComponent<Buttons>().Play(); // func call to switch scene
        }
    }
}
