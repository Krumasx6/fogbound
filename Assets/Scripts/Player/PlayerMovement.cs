using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{       

    public CharacterController controller;

    [Header("Movement Settings")]
    public float walkSpeed = 6f;
    public float runSpeed = 14f;
    public float gravity = -60f;
    public float jumpHeight = 3f;

    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float currentStamina = 100f;
    public float staminaDrainRate = 20f;
    public float staminaRegenRate = 15f;
    public float staminaRegenDelay = 2.5f;

    private float regenTimer;

    [Header("Ground Checking")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public bool isGrounded;

    public Vector3 velocity;

    public bool IsSprinting { get; private set; }
 
    void Update()
    {
        
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveInput = new Vector3(x, 0f, z);
        bool hasMovementInput = moveInput.magnitude > 0.1f;

        bool sprintInput = Input.GetKey(KeyCode.LeftShift);
        bool canSprint = currentStamina > 0f;
        IsSprinting = sprintInput && hasMovementInput && canSprint;

        float speed = IsSprinting ? runSpeed : walkSpeed;

        Vector3 move = (transform.right * x + transform.forward * z);

        if (hasMovementInput)
        {
            controller.Move(move * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        if (IsSprinting)
        {
            currentStamina -= staminaDrainRate * Time.deltaTime;
            regenTimer = 0f;
        }
        else
        {
            regenTimer += Time.deltaTime;

            if (regenTimer >= staminaRegenDelay)
                currentStamina += staminaRegenRate * Time.deltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

        velocity.y += gravity * Time.deltaTime;
         controller.Move(new Vector3(0, velocity.y, 0) * Time.deltaTime);

    }

}

