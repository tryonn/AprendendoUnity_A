using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveKeyboard : MonoBehaviour {

    private Animator anim;

    public MovementMotor motor;
    public Transform playerTransform;

    private Quaternion screenMovementSpace;
    private Vector3 screenMovementForward;
    private Vector3 screenMovementRight;

    private string AXIS_X = "Horizontal";
    private string AXIS_Y = "Vertical";

    private void Awake()
    {
        anim = GetComponent<Animator>();
        motor = GetComponent<MovementMotor>();

        motor.movementDirection = Vector2.zero;
    }

    // Use this for initialization
    void Start () {

        screenMovementSpace = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        screenMovementForward = screenMovementSpace * Vector3.forward;
        screenMovementRight = screenMovementSpace * Vector3.right;
	}
	
	// Update is called once per frame
	void Update () {

        motor.movementDirection = Input.GetAxis(AXIS_X) * screenMovementRight + Input.GetAxis(AXIS_Y) * screenMovementForward;

        if (Input.GetAxis(AXIS_X) != 0 || Input.GetAxis(AXIS_Y) != 0)
        {
            anim.SetBool(AnimationStates.ANIMATION_WALK, true);
        } else
        {
            anim.SetBool(AnimationStates.ANIMATION_WALK, false);
        }

        if (motor.movementDirection.sqrMagnitude > 1)
            motor.movementDirection.Normalize();
		
	}
}
