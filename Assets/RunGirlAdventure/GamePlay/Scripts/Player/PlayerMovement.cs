using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float movementSpeed = 5f;
    public float jumperPower = 10f;
    public float secondJumpPower = 10f;

    public Transform groundCheckPosition;
    public float radius = 0.3f;
    public LayerMask layerGround;

    private Rigidbody _rigidbody;
    private bool isGrounded;
    private bool playerJumped = false;
    private bool canDoubleJump = false;

    private PlayerAnimation playerAnim;

    [SerializeField]
    private bool gameStarted;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        playerAnim = GetComponent<PlayerAnimation>();
    }

    private void Start()
    {
        StartCoroutine(StarGame());
    }

    private void FixedUpdate()
    {
        if (gameStarted)
        {
            PlayerMove();
            PlayerGrounded();
            PlayerJump();
        }

    }

    void PlayerMove()
    {
        _rigidbody.velocity = new Vector3(movementSpeed, _rigidbody.velocity.y, 0f);

        playerAnim.PlayerRun(movementSpeed);
        playerAnim.JumpA(playerJumped);

    }

    void PlayerGrounded()
    {
        isGrounded = Physics.OverlapSphere(groundCheckPosition.position, radius, layerGround).Length > 0;
        playerAnim.SetGrounded(isGrounded);
        if (isGrounded && playerJumped)
        {
            playerJumped = false;
        }
    }

    void PlayerJump()
    {
        /*if (Input.GetKeyDown(KeyCode.Space) && !isGrounded && canDoubleJump)
        {
            canDoubleJump = false;
            _rigidbody.AddForce(new Vector3(0, secondJumpPower, 0));

        } else 
        */

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            _rigidbody.AddForce(new Vector3(0, jumperPower, 0));
            playerJumped = true;
        }
    }

    IEnumerator StarGame()
    {
        yield return new WaitForSeconds(2f);
        gameStarted = true;
        //playerAnim.PlayerRun(movementSpeed);

    }
}
