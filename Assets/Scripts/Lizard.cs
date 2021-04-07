using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lizard : MonoBehaviour
{
    public Animator anim;
    public Transform checkpoint;
    public Vector2 force;
    public Rigidbody2D rb;
    //public GameObject go;
    [SerializeField] Transform groundCheckCircle;
    [SerializeField] LayerMask groundLayer;
    public Platform platform;
    

    const float groundCheckRadius = .2f;

    public float currentSpeed;
    float moveSpeed, hangTime, hangCounter;

    public bool facingRight;

    bool isGrounded, isAttacking, isDead, isAttackPressed;

    string currentState;
    private bool isJumping = false;
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_ATTACK = "Attack";
    const string PLAYER_FLYING = "IguannaInAir";
    const string PLAYER_LANDING = "IguannaOnLand";
    const string PLAYER_JUMPING = "IguannaOnJump";

    void Start()
    {
        force = new Vector2(0f, 5f);
        moveSpeed = 3.9f;
        hangTime = 2f;
        hangCounter = 0f;
        facingRight = true;
        isGrounded = false;
        isAttacking = false;
        isDead = false;
        isAttackPressed = false;
}
    void Update()
    {

        /*if (rb.velocity.y > maxspeed)
        {
        maxspeed = 0, minspeed = 0
            maxspeed = rb.velocity.y;
            Debug.Log(maxspeed);
        } else if (rb.velocity.y < minspeed)
        {
            minspeed = rb.velocity.y;
            Debug.Log(minspeed);
        }*/

        //Debug.Log(rb.velocity.y);
        // 5, -11
        if (!isDead && !isAttacking)
        {
            if (Math.Abs(rb.velocity.y) == 0)
                ChangeAnimation(PLAYER_IDLE);
            else if (Math.Abs(rb.velocity.y) > 1)
                ChangeAnimation(PLAYER_FLYING);
            else if (currentSpeed > 0)
                ChangeAnimation(PLAYER_RUN);
        }


            

        // Jump on spacebar
        if (Input.GetButtonDown("Jump") && hangCounter > 0)
            Jump();
        

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
        if (collision.collider.tag == "Platform")
        {
            //Debug.Log("¿Õœ–»Ã");
            //anim = collision.gameObject.GetComponent<Animator>();
            platform.RotatePlatform();
        }

    }

    /*void JumpStart()
    {
        isJumping = true;
        JumpFinish();
    }

    void JumpFinish()
    {
        isJumping = false;
    }*/

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
        //anim.Play(PLAYER_JUMPING);
        //Invoke("JumpStart", 0.2f);
        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0 && !isDead)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .3f);
        } else if (Input.GetButtonDown("Jump") && !isDead)
        {
            rb.velocity = force;
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
