using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterJump : MonoBehaviour
{
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    Rigidbody2D rb;

    void Awake()
    {
        // Gets RB component of script owner
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Changes player velocity if player is holding jump or not
        if (rb.velocity.y < 0)
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier-1) * Time.deltaTime;
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
    }
}
