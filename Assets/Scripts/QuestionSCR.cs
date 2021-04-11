using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSCR : MonoBehaviour
{
    BoxCollider2D bc;                                       //questions box collider
    public int hp;                                          //questions hp
    public Lizard player;                                   //Iguana
    public bool isPressed;                                  //attack button condition

    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();                 //finding question box collider
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))            //check attack button pressing
        {
            isPressed = true;                               //attack button condition == true
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")           //check connection question block object with iguana
        {
            if (isPressed)                                  //checking attack button condition (if it is pressed)
            {
                isPressed = false;                          //attack button condition == false
                Debug.Log("Hit");
                hp--;                                       //one questions hp is taken away
                if (hp == 0)                                //check if questions hp == 0
                    bc.isTrigger = true;                    //ciguana can walk thrue though the question block
            }
        }
    }
}
