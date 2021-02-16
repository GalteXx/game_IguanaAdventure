using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    public Animator anim;
    public Vector2 force = new Vector2(0f, 5f);
    public Rigidbody2D rb;
    [SerializeField] Transform groundCheckCircle;
    [SerializeField] LayerMask groundLayer;

    const float groundCheckRadius = .2f;

    public float moveSpeed = 5f;
    public float hangTime = 2f;
    private float hangCounter = 0f;

    public bool isGrounded = false;
    public bool facingRight = true;
    
    void Update()
    {
        // Checking the side player is going, left or right
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f); // so its either -1 or 1
        transform.position += movement * Time.deltaTime * moveSpeed; // so the position of our player is -1 or 1 * time has passed and ms 
        GroundCheck();
        // ***=== Jumping section start ===*** \\
        HangManager(); // After player leaves platform timer

        if (Input.GetButtonDown("Jump") && hangCounter > 0)
        {
            Jump();
        } // Jump on spacebar
        // ***=== Jumping section end ===*** \\

        // Attack on LeftShift but can be changed. In work
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Attack();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Attack();
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
        
        // Flip if changed direction
        if ( (movement.x < 0 && facingRight) || (movement.x > 0 && !facingRight))
        {
            Flip();
        }

    }

    void HangManager()
    {
        if (isGrounded)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }
    }

    void GroundCheck()
    {
        // Check if the object is colliding with other 2d collider that has "Ground" Layer
        //
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCircle.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
        }
    }

    void Attack()
    {
        anim.SetTrigger("attack");
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .3f);
        } else if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = force;
        //gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Water")
        {
            Destroy(gameObject);
        }
    }
}
