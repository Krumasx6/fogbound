using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{       

    public CharacterController controller;

    [Header("Movement Settings")]
    public float currentSpeed;
    public float walkSpeed = 8f;
    public float gravity = -78.48f;
    public float jumpHeight = 8f;

    [Header("Ground Checking")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    public Vector3 velocity;


    void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * walkSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y =  Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
}
