using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public float moveSpeed = 5f;
    public bool isGrounded = false;
    public bool facingRight = true;
    public Vector2 force = new Vector2(0f, 5f);
    private bool attack;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Jump();

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * moveSpeed;
        if (movement.x > 0 || movement.x < 0)
        {
            anim.SetBool("isRunning", true);
        } else
        {
            anim.SetBool("isRunning", false);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
            Attack();
        }

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
        if (Input.GetButtonDown("Jump") && isGrounded == true) { 
            gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
