using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lizard : MonoBehaviour
{
    // References for components of Iguanna GameObject
    [SerializeField] Animator anim;
    [SerializeField] Transform checkpoint;
    [SerializeField] Vector2 force;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Platform platform; // Platform script
    [SerializeField] Transform groundCheckCircle; // The object that checks iguanna's if iguanna is on the ground or not
    [SerializeField] LayerMask groundLayer; // Layer that makes isgrounded boolean true
    [SerializeField] float currentSpeed;

    const float groundCheckRadius = .2f; // Ground checker circle under the player (groundCheckCircle variable). This circle must have radius
    float moveSpeed, hangTime, hangCounter;
    
    bool isGrounded, isAttacking, isDead, isAttackPressed, facingRight; // Boolean states of Iguanna

    // States for animations
    string currentState;
    const string PLAYER_IDLE = "Idle";
    const string PLAYER_RUN = "Run";
    const string PLAYER_ATTACK = "Attack";
    const string PLAYER_FLYING = "IguannaInAir";

    /// <summary>
    /// On game start methods, sets all start function
    /// </summary>
    void Start()
    {
        // On start settings and declares
        force = new Vector2(0f, 5f);
        moveSpeed = 3.9f;
        hangTime = .2f;
        hangCounter = 0f;
        facingRight = true;
        isGrounded = false; 
        isAttacking = false;
        isDead = false; 
        isAttackPressed = false;
}
    /// <summary>
    /// Updates every frame (can vary because different PCs)
    /// </summary>
    void Update()
    {

        // Animation handler IDLE, FLYING, RUNNING
        if (!isDead && !isAttacking)
        {
            if (rb.velocity.y == 0 && rb.velocity.x == 0)
                ChangeAnimation(PLAYER_IDLE); 
            else if (Math.Abs(rb.velocity.y) > 1)
                ChangeAnimation(PLAYER_FLYING); 
            else //if (Math.Abs(rb.velocity.x) > 0.1f && isGrounded) 
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
    /// <summary>
    /// Updates every fixed amount of frames. Here updates physics and movements
    /// </summary>
    private void FixedUpdate()
    {
        // Checks if there is a ground layer under feet and takes care of jump timer
        GroundCheck();
        HangManager();

        // Movement calculations and math
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f); // so its either -1 or 1
        transform.position += movement * Time.deltaTime * moveSpeed; // so the position of our player is -1 or 1 * time has passed and ms 
        currentSpeed = Mathf.Abs(movement.x * moveSpeed); // changing walking speed
        
        // Flip the player if he moves to the another side
        if ((movement.x < 0 && facingRight) || (movement.x > 0 && !facingRight))
            Flip(); // MONKEY FLIP
    }
    /// <summary>
    /// Executes on script owner enters another collision 
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Dies if our player touches the water. Wasser macht die Leguan tot! Das Wasser ist schlecht! Das Wasser war nie gut!
        if (collision.collider.tag == "Water")
            Die(); // Iguana isn't Kurt Cobain :<
    }
    /// <summary>
    /// Calls for every collider2d elements touching script owner's collision
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        // the platform turns when it is attacked by Iguana
        if (collision.CompareTag("Platform") && Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Attack Platform"); // logging
            platform.RotatePlatform(); // rotating platform
        }
    }

    /// <summary>
    /// This sunction changes animation to newState param
    /// </summary>
    /// <param name="newState"></param>
    void ChangeAnimation(string newState)
    {
        if (currentState == newState) return; // prevent animation to interrupt itself

        anim.Play(newState); // play new state
        currentState = newState; // reassign currentState
    }
    /// <summary>
    /// Handles extra time player to jump after he has nothing under his lizard's paws (.2f sec)
    /// </summary>
    void HangManager()
    {
        // If the player is grounden, resets timer else (is in the air) timer is ticking
        if (isGrounded)
            hangCounter = hangTime;
        else
            hangCounter -= Time.deltaTime;
    }
    /// <summary>
    /// Checks if there is a collider with groundLayer and if true sets isGround to TRUE else FALSE
    /// </summary>
    void GroundCheck()
    {
        // Check if the object is colliding with other 2d collider that has "Ground" Layer
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCircle.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
            isGrounded = true;
    }
    /// <summary>
    /// Attack animation handler
    /// </summary>
    void Attack()
    {
        // If attack is pressed
        if (isAttackPressed)
        {
            // Set it to false and if he is not attacking already and is not dead atm
            isAttackPressed = false; // switching the flag to false
            if (!isAttacking && !isDead)
            {
                // Set attacking to true and change animation, after .2 seconds call AttackComplete wich will return state to normal
                isAttacking = true; // switching the flag to true
                ChangeAnimation(PLAYER_ATTACK); // Iguana's attack
                Invoke("AttackComplete", .2f);
            }
        }
    }
    /// <summary>
    /// A part of attack animation handler
    /// </summary>
    void AttackComplete()
    {
        isAttacking = false;
    }
    /// <summary>
    /// Jump executer handler
    /// </summary>
    void Jump()
    {
        // If the jump button is pressed for long time, jump higher else lower
        if (Input.GetButtonDown("Jump") && rb.velocity.y > 0 && !isDead)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .3f);
        else if (Input.GetButtonDown("Jump") && !isDead)
            rb.velocity = force;
        
    }
    /// <summary>
    /// Flips iguana current animation frame 180* depends on what velocity.x it has
    /// </summary>
    void Flip()
    {
        // Flip the character if he is not dead on 180* left or right
        if (!isDead) { 
            facingRight = !facingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    /// <summary>
    /// Changes animation, sets boolean and ms when player dies
    /// </summary>
    void Die()
    {
        // Changes anim to player_idle, sets the isDead statement and sets MS to zero
        ChangeAnimation(PLAYER_IDLE);
        isDead = true;
        moveSpeed = 0;
    }
    /// <summary>
    /// Teleports iguana to checkpoint and resets all variables
    /// </summary>
    void Respawn()
    {
        // If the player dead, then reset movespeed to 5 as it was and teleport the player to the checkpoint
        if (isDead) { 
            isDead = false;
            moveSpeed = 3.9f;
            transform.position = new Vector3(checkpoint.position.x, checkpoint.position.y);
        }
    }
}
