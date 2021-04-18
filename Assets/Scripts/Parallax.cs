using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    Transform target;
    Material mat;
    Vector2 offset;

    [SerializeField] float scale = 1.0f;

    void Start()
    {
        // Setting up the start up variables
        target = transform.root; // takes the target of iguanna
        mat = GetComponent<SpriteRenderer>().material; // takes the component 
        offset = Vector2.zero; // sets offset to 0
    }

    // Update is called once per frame
    void Update()
    {
        // Parallax material move to an offset. Scale is used to change the speed of different parallax layers
        offset = new Vector2( target.position.x / 100f / scale, -3f);
        mat.mainTextureOffset = offset;
    }
}
