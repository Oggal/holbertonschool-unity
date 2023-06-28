using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector3 desiredDirection = Vector2.zero;

    [SerializeField] float Speed = 5.0f;
    CharacterController characterController = null;
    bool JumpDesired = false;


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
        Vector3 JumpForce = Vector3.zero;
        if (JumpDesired)
        {
            JumpForce = Vector3.up * 5.0f;
            JumpDesired = false;
        }
        characterController.SimpleMove(speed: (desiredDirection * Speed) + JumpForce);
    }
}
