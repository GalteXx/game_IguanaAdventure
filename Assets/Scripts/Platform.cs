using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Animator anim;
    private string currentState;

    private bool isFlipping = false;

    const string FLIP_180 = "Platform_spin180";
    const string FLIP_360 = "Platform_spin360";
    const string FLIP_IDLE = "Platform_idle";
    const string FLIP_IDLE180 = "Platform_idle180";

    void ChangeAnimation(string newState)
    {
        if (currentState == newState) return; // prevent animation to interrupt itself

        anim.Play(newState); // play new state
        currentState = newState; // reassign currentState
    }

    public void RotatePlatform()
    {
        if (!isFlipping)
        {
            Debug.Log("Flipping");
            isFlipping = true;
            ChangeAnimation(FLIP_180);
            Invoke("RotateIdle180", 0.19f);
            
        }
        
    }

    private void RotateIdle180()
    {
        ChangeAnimation(FLIP_IDLE180);
        Invoke("RotateComplete", 3f);
    }

    private void RotateComplete()
    {
        Debug.Log("Flip complete");
        isFlipping = false;
        anim.Play(FLIP_360);
        Invoke("RotateIdle", .19f);
    }

    private void RotateIdle()
    {
        Debug.Log("idle");
        anim.Play(FLIP_IDLE);
    }
}
