using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector3 desiredDirection = Vector2.zero;

    [SerializeField] float acceleration = 5.0f, maxSpeed = 5.0f, jumpForce = 5.0f, maxFallSpeed = 4.0f;
    CharacterController characterController = null;
    bool JumpDesired = false;
    Vector3 velocity = Vector3.zero;
    [SerializeField] Vector3 startPos;
    [SerializeField] float deathHeight = -10.0f, respawnHeight = 5.0f;
    public UnityEvent MenuToggle;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        startPos = transform.position;
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
        if (IsDead())
            Respawn();
    }

    void GetInput()
    {
        if (Input.GetButtonDown("Cancel"))
            MenuToggle.Invoke();
        desiredDirection = Vector3.zero;
        desiredDirection.x = Input.GetAxis("Horizontal");
        desiredDirection.z = Input.GetAxis("Vertical");
        desiredDirection.Normalize();
        JumpDesired = JumpDesired || (Input.GetButton("Jump") && characterController.isGrounded);
    }

    void HandleMove()
    {
        Vector3 Delta = (desiredDirection * maxSpeed) - velocity;
        Delta.y = 0.0f;
        velocity += Delta * acceleration * Time.deltaTime;
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

    bool IsDead()
    {
        return transform.position.y < deathHeight;
    }

    void Respawn()
    {
        transform.position = startPos + Vector3.up * respawnHeight;
        transform.rotation = Quaternion.identity;
    }
}
