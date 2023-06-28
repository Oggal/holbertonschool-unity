using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector3 desiredDirection = Vector2.zero;

    [SerializeField] float acceleration = 5.0f, maxSpeed = 5.0f, jumpForce = 5.0f, maxFallSpeed = 4.0f;
    CharacterController characterController = null;
    bool JumpDesired = false;
    Vector3 velocity = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    // Fixed Update is called once per physics update
    void FixedUpdate()
    {
        HandleMove();
    }

    void GetInput()
    {
        desiredDirection = Vector3.zero;
        desiredDirection.x = Input.GetAxis("Horizontal");
        desiredDirection.z = Input.GetAxis("Vertical");
        desiredDirection.Normalize();
        JumpDesired = JumpDesired || Input.GetButtonDown("Jump");
    }

    void HandleMove()
    {
        Vector3 Delta = (desiredDirection * maxSpeed) - velocity;
        Delta.y = 0.0f;

        if (desiredDirection.magnitude > 0.1f)
        {
            Delta = Vector3.ClampMagnitude(Delta,acceleration) * Time.deltaTime;
        }
        velocity += Delta;
        velocity.y = Mathf.Clamp(velocity.y, -maxFallSpeed, jumpForce * 2);
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        if (JumpDesired && characterController.isGrounded)
        {
            velocity.y = jumpForce;
            JumpDesired = false;
        }
        else
        {
            if (!characterController.isGrounded)
                velocity.y -= acceleration * Time.deltaTime;
            else
                velocity.y = 0.0f;
        }

        
        characterController.Move((transform.rotation * velocity) * Time.deltaTime);
    }
}
