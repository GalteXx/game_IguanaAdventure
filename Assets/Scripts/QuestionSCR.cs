using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionSCR : MonoBehaviour
{
    [SerializeField] Sprite breakBox;
    [SerializeField] SpriteRenderer sp;
    [SerializeField] BoxCollider2D bc;                                       //questions box collider
    [SerializeField] int hp;                                          //questions hp
    [SerializeField] Lizard player;                                   //Iguana
    [SerializeField] bool isPressed;                                  //attack button condition

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
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
                {
                    sp.sprite = breakBox;
                    bc.isTrigger = true;                    //ciguana can walk thrue though the question block
                }                                
                                                                                     

            }
        }
    }
}
