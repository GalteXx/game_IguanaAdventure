using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSCR : MonoBehaviour
{
    BoxCollider2D bc;
    public int hp;
    public Lizard player;
    public bool isPressed;

    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isPressed = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (isPressed)
            {
                isPressed = false;
                Debug.Log("Hit");
                hp--;
                if (hp == 0)
                    bc.isTrigger = true;
            }
        }
    }
}
