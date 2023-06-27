using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector2 desiredDirection = Vector2.zero;
    [SerializeField] bool JumpDesired = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    // Fixed Update is called once per physics update
    void FixedUpdate()
    {
        if (JumpDesired)
        {
            Jump();
        }
    }

    void GetInput()
    {
        desiredDirection.x = Input.GetAxis("Horizontal");
        desiredDirection.y = Input.GetAxis("Vertical");
        desiredDirection.Normalize();
        JumpDesired = JumpDesired || Input.GetButtonDown("Jump");
    }

    void Jump()
    {
        Debug.Log("Jump");
        JumpDesired = false;
    }
}
