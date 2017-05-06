using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour {

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        HandleButtonPresses();
		
	}

    private void HandleButtonPresses()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            anim.SetBool(AnimationStates.ATTACK, true);
        }

        if (Input.GetKeyUp(KeyCode.I))
        {
            anim.SetBool(AnimationStates.ATTACK, false);
        }
    }
}
