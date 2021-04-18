using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Animator anim; // Declare fields and asks for animator component of Platform gameobject
    private string currentState; // Current state for ChangeAnimation function
    private bool isFlipping = false; // current state if current platform is already in flipping process

    // States for animations. IDLE states are for horizontal and vertical idling position and FLIP are for playing the frames
    const string FLIP_180 = "Platform_spin180";
    const string FLIP_360 = "Platform_spin360";
    const string FLIP_IDLE = "Platform_idle";
    const string FLIP_IDLE180 = "Platform_idle180";

    /// <summary>
    /// Change animation function, checks if current state of the platform is not already same to not change the animation.
    /// </summary>
    /// <param name="newState"></param>
    void ChangeAnimation(string newState)
    {
        if (currentState == newState) return; // prevent animation to interrupt itself

        anim.Play(newState); // play new state
        currentState = newState; // reassign currentState
    }
    /// <summary>
    /// Rotates platform to horizontal
    /// </summary>
    public void RotatePlatform()
    {
        if (!isFlipping)
        {
            // If platform was not flipping before the the function was invoked then start animation, play FLIP_180 and after .19f call RotateIdle
            isFlipping = true;
            ChangeAnimation(FLIP_180);
            Invoke("RotateIdle180", 0.19f);
            
        }
        
    }
    /// <summary>
    /// Lets it be horizontal for 3 seconds
    /// </summary>
    private void RotateIdle180()
    {
        // Flip idle places platform horiztally for 3 seconds Invokes RotateComplete
        ChangeAnimation(FLIP_IDLE180);
        Invoke("RotateComplete", 3f);
    }

    /// <summary>
    /// Finishes the animation and allows user to flip the platform again then calls RotateIdle function after the FLIP_360 animation finishes
    /// </summary>
    private void RotateComplete()
    {
        isFlipping = false;
        anim.Play(FLIP_360);
        Invoke("RotateIdle", .19f);
    }

    /// <summary>
    /// Call the last state of the platform that turns platform vertically
    /// </summary>
    private void RotateIdle()
    {
        anim.Play(FLIP_IDLE);
    }
}
