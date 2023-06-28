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


    void OLDHandleMove()
    {
        Vector3 accelerationVector = desiredDirection * acceleration;
        accelerationVector.y = 0.0f;
        if (JumpDesired && characterController.isGrounded){
            accelerationVector.y = jumpForce;
            JumpDesired = false;
        }
        else
        {
            accelerationVector.y = Physics.gravity.y;
        }
        velocity += accelerationVector * Time.fixedDeltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        velocity.y = Mathf.Clamp(velocity.y, -maxFallSpeed, maxSpeed);
        characterController.Move(velocity * Time.fixedDeltaTime);
    }

    void HandleMove()
    {
        Vector3 Delta = (desiredDirection * maxSpeed) - velocity;
        if( characterController.isGrounded)
        {
            Delta.y = 0.0f;
        }
        
        velocity += Vector3.ClampMagnitude(Delta,acceleration) * Time.deltaTime;
        velocity.y = Mathf.Clamp(velocity.y, -maxFallSpeed, jumpForce * 2);
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        if (JumpDesired && characterController.isGrounded)
        {
            velocity.y = jumpForce;
            JumpDesired = false;
        }
        else
        {
            velocity.y -= acceleration * Time.deltaTime;
        }
        characterController.Move(velocity * Time.deltaTime);
    }
}
