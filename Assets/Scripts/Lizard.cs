using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lizard : MonoBehaviour
{
    // References for components of Iguanna GameObject
    public Animator anim;
    public Transform checkpoint;
    public Vector2 force;
    public Rigidbody2D rb;
    public Platform platform;

    // The object that checks iguanna's if iguanna is on the ground or not
    [SerializeField] Transform groundCheckCircle;
    // Layer that makes isgrounded boolean true
    [SerializeField] LayerMask groundLayer;
    
    
    // Ground checker circle under the player (groundCheckCircle variable). This circle must have radius
    const float groundCheckRadius = .2f;
 
    public float currentSpeed, Yspeed, Xspeed;
    float moveSpeed, hangTime, hangCounter;

    // Boolean states of Iguanna
    public bool isGrounded;
    bool isAttacking, isDead, isAttackPressed, facingRight;

    // States for animations
    string currentState;
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_ATTACK = "Attack";
    const string PLAYER_FLYING = "IguannaInAir";

    void Start()
    {
        // On start settings and declares
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

        Yspeed = rb.velocity.y;
        Xspeed = rb.velocity.x;

        // Animation handler IDLE, FLYING, RUNNING
        if (!isDead && !isAttacking)
        {
            if (rb.velocity.y == 0 && rb.velocity.x == 0)
                ChangeAnimation(PLAYER_IDLE);
            else if (Math.Abs(rb.velocity.y) > 1)
                ChangeAnimation(PLAYER_FLYING);
            else if (rb.velocity.x > 0 && isGrounded)
                ChangeAnimation(PLAYER_RUN);
        }


            

        // Checks if the Jump is pressed and there is time on timer
        if (Input.GetButtonDown("Jump") && hangCounter > 0)
            Jump();
        
        // Checks for attack button
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isAttackPressed = true;
            Attack();
        }

        // Checks for respawn button
        if (Input.GetKeyDown(KeyCode.E))
            Respawn();
        
        // Temporary rotates platform on O button
       if (Input.GetKeyDown(KeyCode.O))
           platform.RotatePlatform();
        

    }

    private void FixedUpdate()
    {
        // Checks if there is a ground layer under feet and takes care of jump timer
        GroundCheck();
        HangManager();

        // Movement calculations and math
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f); // so its either -1 or 1
        transform.position += movement * Time.deltaTime * moveSpeed; // so the position of our player is -1 or 1 * time has passed and ms 
        currentSpeed = Mathf.Abs(movement.x * moveSpeed);

        // Flip the player if he moves to the another side
        if ((movement.x < 0 && facingRight) || (movement.x > 0 && !facingRight))
            Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Dies if our player touches the water. Wasser macht die Leguan tot! Das Wasser ist schlecht! Das Wasser war nie gut!
        if (collision.collider.tag == "Water")
            Die();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // the platform turns when it is attacked by Iguana
        if (collision.collider.tag == "Platform" && Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Attack Platform");
            platform.RotatePlatform();
        }
    }

    void ChangeAnimation(string newState)
    {
        if (currentState == newState) return; // prevent animation to interrupt itself

        anim.Play(newState); // play new state
        currentState = newState; // reassign currentState
    }

    void HangManager()
    {
        // If the player is grounden, resets timer else (is in the air) timer is ticking
        if (isGrounded)
            hangCounter = hangTime;
        else
            hangCounter -= Time.deltaTime;
    }

    void GroundCheck()
    {
        // Check if the object is colliding with other 2d collider that has "Ground" Layer
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCircle.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
            isGrounded = true;
    }

    void Attack()
    {
        // If attack is pressed
        if (isAttackPressed)
        {
            // Set it to false and if he is not attacking already and is not dead atm
            isAttackPressed = false;
            if (!isAttacking && !isDead)
            {
                // Set attacking to true and change animation, after .2 seconds call AttackComplete wich will return state to normal
                isAttacking = true;
                ChangeAnimation(PLAYER_ATTACK);
                Invoke("AttackComplete", .2f);
            }
        }
    }
    // switching the flag to false
    void AttackComplete()
    {
        isAttacking = false;
    }

    void Jump()
    {
        // If the jump button is pressed for long time, jump higher else lower
        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0 && !isDead)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .3f);
        else if (Input.GetButtonDown("Jump") && !isDead)
            rb.velocity = force;
        
    }

    void Flip()
    {
        // Flip the character if he is not dead on 180* left or right
        if (!isDead) { 
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    void Die()
    {
        // Changes anim to player_idle, sets the isDead statement and sets MS to zero
        ChangeAnimation(PLAYER_IDLE);
        isDead = true;
        moveSpeed = 0;
    }

    void Respawn()
    {
        // If the player dead, then reset movespeed to 5 as it was and teleport the player to the checkpoint
        if (isDead) { 
            isDead = false;
            moveSpeed = 5f;
            transform.position = new Vector3(checkpoint.position.x, checkpoint.position.y);
        }
    }
}
