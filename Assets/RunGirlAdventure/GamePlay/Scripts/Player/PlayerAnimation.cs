using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    const string RUN = "Run";

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void JumpA(bool jumpA)
    {
        anim.SetBool("JumpA", jumpA);
    }

    internal void PlayerRun(float movementSpeed)
    {
        anim.SetFloat(RUN, movementSpeed);
    }

    internal void SetGrounded(bool isGrounded)
    {
        anim.SetBool("Ground", isGrounded);
    }
}
