using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // 
    Transform target;
    Material mat;
    Vector2 offset;

    [SerializeField] float scale = 1.0f;

    void Start()
    {
        // Setting up the start up variables
        target = transform.root;
        mat = GetComponent<SpriteRenderer>().material;
        offset = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Parallax material move to an offset 
        offset = new Vector2( target.position.x / 100f / scale, -3f);
        mat.mainTextureOffset = offset;
    }
}
