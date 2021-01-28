using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    public Animator anim;
    public Vector2 force = new Vector2(0f, 5f);

    public float moveSpeed = 5f;

    public bool isGrounded = false;
    public bool facingRight = true;
    private bool attack;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;

        // Jump on spacebar
        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            Jump();
        }
       
        // Set animation to walking if x axis has difference >0 or its not walking, then turn off walking animation
        if (movement.x > 0 || movement.x < 0)
        {
            anim.SetBool("isRunning", true);
        } 
        else 
        {
            anim.SetBool("isRunning", false);
        }
        
        // Attack on LeftShift but can be changed. In work
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
            Attack();
        }

        // Flip if changed direction
        if ( (movement.x < 0 && facingRight) || (movement.x > 0 && !facingRight))
        {
            Flip();
        }
    }

    void Attack()
    {
        if (attack)
        {
            anim.SetTrigger("attack");
            attack = false;
        }
        anim.SetTrigger("attack");

    }

    void Jump()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
