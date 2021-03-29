using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lizard : MonoBehaviour
{
    public Animator anim;
    public Transform checkpoint;
    public Vector2 force = new Vector2(0f, 5f);
    public Rigidbody2D rb;
    //public GameObject go;
    [SerializeField] Transform groundCheckCircle;
    [SerializeField] LayerMask groundLayer;
    

    const float groundCheckRadius = .2f;

    private float moveSpeed = 3.9f;
    private float hangTime = 2f;
    public float currentSpeed;
    private float hangCounter = 0f;

    public bool facingRight = true;

    private bool isGrounded = false;
    private bool isAttacking = false;
    private bool isDead = false;
    private bool isAttackPressed = false;

    private string currentState;

    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_ATTACK = "Attack";

    void Update()
    {
        // Jump on spacebar
        if (Input.GetButtonDown("Jump") && hangCounter > 0)
        {
            Jump();
        } 

        // Attack on LeftShift but can be changed. In work
        if (!isAttacking && !isDead) { 
            if (currentSpeed == 0)
                ChangeAnimation(PLAYER_IDLE);
            else if (currentSpeed > 0)
                ChangeAnimation(PLAYER_RUN);
            
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isAttackPressed = true;
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Respawn();
        }

    }

    private void FixedUpdate()
    {
        GroundCheck();
        HangManager();

        // movement
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f); // so its either -1 or 1
        transform.position += movement * Time.deltaTime * moveSpeed; // so the position of our player is -1 or 1 * time has passed and ms 
        currentSpeed = Mathf.Abs(movement.x * moveSpeed);

        // Flip if changed direction
        if ((movement.x < 0 && facingRight) || (movement.x > 0 && !facingRight))
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Water")
            Die();
        
    }

    void ChangeAnimation(string newState)
    {
        if (currentState == newState) return; // prevent animation to interrupt itself

        anim.Play(newState); // play new state
        currentState = newState; // reassign currentState
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
        if (isAttackPressed)
        {
            isAttackPressed = false;
            if (!isAttacking && !isDead)
            {
                isAttacking = true;
                ChangeAnimation(PLAYER_ATTACK);
                Invoke("AttackComplete", .2f);
            }
        }
    }

    void AttackComplete()
    {
        isAttacking = false;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0 && !isDead)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .3f);
        } else if (Input.GetButtonDown("Jump") && !isDead)
        {
            rb.velocity = force;
        //gameObject.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }
    }

    void Flip()
    {
        if (!isDead) { 
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    void Die()
    {
        ChangeAnimation(PLAYER_IDLE);
        isDead = true;
        moveSpeed = 0;
        //go.SetActive(false);
    }

    void Respawn()
    {
        if (isDead) { 
            isDead = false;
            //transform.position = new Vector3(checkpoint.position.x, checkpoint.position.y);
            moveSpeed = 5f;
            transform.position = new Vector3(checkpoint.position.x, checkpoint.position.y);
        }
    }
}
