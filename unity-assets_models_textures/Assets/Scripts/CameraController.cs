using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Camera myCamera;
    Vector3 Offset, rotOffset;
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
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);
        myCamera.transform.position = transform.position + (transform.rotation * Offset);
        myCamera.transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotOffset);
        
    }
}
