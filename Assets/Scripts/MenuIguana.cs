using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuIguana : MonoBehaviour
{
    public bool menuWalk;
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
        if (menuWalk)
        {
            transform.position += new Vector3(0.15f, 0, 0);
        }
        if (transform.position.x >= 11)
        {
            menuWalk = false;
            canvas.GetComponent<Buttons>().Play();
        }
    }
}
