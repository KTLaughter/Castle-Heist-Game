using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public Transform orientation;
    float hInput;
    float vInput;
    Vector3 direction;
    Rigidbody _rb;
    [Header("Ground Check")]
    public float height;
    public LayerMask groundLayer;
    bool grounded;
    private CapsuleCollider _col;
    public float groundDrag;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public float sprintMultiplier;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    private GameBehavior _gameManager;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        readyToJump = true;
        _gameManager = GameObject.Find("GameManager").GetComponent<GameBehavior>();
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

     void Update()
    {
        //To check if touching the ground layer mask
        grounded = Physics.Raycast(transform.position, Vector3.down, height * 0.5f + 0.2f, groundLayer);
        PlayerInput();
        SpeedCap();

        if (grounded)
            _rb.drag = groundDrag;
        else
            _rb.drag = 0;

    }

    private void PlayerInput()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
           

            readyToJump = false;

                FindObjectOfType<AudioManager>().Play("JumpSound");


            Jump();

          

            Invoke(nameof(JumpAgain), jumpCooldown);
        }

        if (Input.GetKey(sprintKey))
        {
            Sprint();
        }
    }

    private void PlayerMove()
    {
        direction = orientation.forward * vInput + orientation.right * hInput;

        if (grounded)
            _rb.AddForce(direction.normalized * moveSpeed * 10f, ForceMode.Force);

        else if (!grounded)
            _rb.AddForce(direction.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedCap()
    {
        Vector3 flatVel = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
        if(flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            _rb.velocity = new Vector3(limitedVel.x, _rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);
        _rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    private void JumpAgain()
    {
        readyToJump = true;
    }

    private void Sprint()
    {
        if (grounded)
            _rb.AddForce(direction.normalized * moveSpeed * sprintMultiplier, ForceMode.Force);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Skeleton")
        {
            FindObjectOfType<AudioManager>().Play("PlayerHurt");
            _gameManager.HP = _gameManager.HP - 5; 
        }

        if (collision.gameObject.name == "SkeletonBall(Clone)")
        {
            FindObjectOfType<AudioManager>().Play("PlayerHurt");
            _gameManager.HP = _gameManager.HP - 10;
        }
    }
}
