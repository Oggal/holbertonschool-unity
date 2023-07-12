using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera myCamera;
    [SerializeField] float ySpeed = -0.01f, xSpeed = 1;
    Vector3 Offset, rotOffset;
    float theta = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (myCamera == null)
            myCamera = Camera.main;
        Offset = myCamera.transform.position - transform.position;
        rotOffset = myCamera.transform.rotation.eulerAngles - transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        theta += Input.GetAxis("Mouse Y") * ySpeed;
        theta = Mathf.Clamp(theta, 0, 90f);
        transform.Rotate(0, Input.GetAxis("Mouse X") * xSpeed, 0);
        myCamera.transform.position =  (transform.position + (transform.rotation * Quaternion.Euler(theta, 0, 0) * Offset));
        myCamera.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotOffset + (Vector3.right * theta));
        
    }
}
