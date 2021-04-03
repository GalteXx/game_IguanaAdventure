using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Start is called before the first frame update
    Transform target;
    Material mat;
    Vector2 offset;

    [SerializeField] float scale = 1.0f;

    void Start()
    {
        target = transform.root;
        mat = GetComponent<SpriteRenderer>().material;
        offset =  = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector2( target.position.x / 100f / scale, -3f);
        mat.mainTextureOffset = offset;
    }
}
