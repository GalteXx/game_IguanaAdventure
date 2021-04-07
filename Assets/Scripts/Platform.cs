using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] Animator anim;
    private string currentState;

    private bool isFlipping = false;

    const string flip = "Rotate90";
    const string unflip = "Rotate270";

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
            ChangeAnimation(flip);
            Invoke("RotateComplete", .19f);
        }
        
    }

    public void RotateComplete()
    {
        Debug.Log("Flip complete");
        isFlipping = false;
        anim.Play(unflip);
        Invoke("RotateStop", .22f);
    }

    public void RotateStop()
    {
        Debug.Log("Stop");
        anim.Play("idle");
    }
}
