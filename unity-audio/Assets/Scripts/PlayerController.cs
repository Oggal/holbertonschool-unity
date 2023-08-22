using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] Vector3 desiredDirection = Vector2.zero;
    [SerializeField] CameraController forwardDirection;
    [SerializeField] float acceleration = 5.0f, maxSpeed = 5.0f, jumpForce = 5.0f, maxFallSpeed = 4.0f;
    CharacterController characterController = null;
    bool JumpDesired = false;
    Vector3 velocity = Vector3.zero;
    [SerializeField] Vector3 startPos;
    [SerializeField] float deathHeight = -10.0f, respawnHeight = 5.0f;
    public UnityEvent MenuToggle;

    [SerializeField] Animator myAnimator;
    [SerializeField] AudioSource audioSource;
    public AudioClip StoneSound;
    public AudioClip GrassSound;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        startPos = transform.position;
        if(audioSource == null)
            audioSource = GetComponent<AudioSource>();
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
        HandleAnimation();
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
            if(myAnimator != null)
                myAnimator.SetTrigger("Jump");
        }
        velocity.y -= acceleration * Time.deltaTime;

        Quaternion CameraRotation = Quaternion.Euler(forwardDirection.GetDirection());

        if (desiredDirection.magnitude > 0.1f)
            transform.rotation = Quaternion.SlerpUnclamped(transform.rotation, Quaternion.LookRotation(CameraRotation * desiredDirection), Time.deltaTime * velocity.magnitude);

        characterController.Move((CameraRotation * velocity) * Time.deltaTime);
    }

    bool IsDead()
    {
        return transform.position.y < deathHeight;
    }

    void HandleAnimation()
    {
        if (myAnimator == null)
            return;
        Vector3 planearVelocity = new Vector3(velocity.x, 0, velocity.z);
        myAnimator.SetFloat("Speed", (desiredDirection.magnitude));
        if( desiredDirection.magnitude > 0.01 && !audioSource.isPlaying) 
            audioSource.Play();
        if (desiredDirection.magnitude == 0)
            audioSource.Stop();
        myAnimator.SetBool("Grounded", characterController.isGrounded);
        
    }
    void Respawn()
    {
        transform.position = startPos + Vector3.up * respawnHeight;
        transform.rotation = Quaternion.identity;
    }

    bool isStone(MeshRenderer renderer)
    {
        foreach(Material mat in renderer.materials)
        {
            if(mat.name.ToLower().Contains("stone"))
                return true;
        }
        return false;
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        MeshRenderer targetVisual = hit.gameObject.GetComponent<MeshRenderer>();
        if(targetVisual && isStone(targetVisual))
            audioSource.clip = StoneSound;
        else
            audioSource.clip = GrassSound;
    }
}
