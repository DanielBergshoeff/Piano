﻿using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

//Rotate self and cam using mouse position
//Mostly got the code from Brackeys

public class FPCameraController : MonoBehaviour
{
    public FloatVariable mouseSensitivity;
    public GameObject playerBody;
    public FloatVariable cameraXRotation;

    // Start is called before the first frame update
    void Start()
    {
        //hide and lock cursor to center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        cameraXRotation.Value = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity.Value * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity.Value * Time.deltaTime;

        //prevent over-rotate
        cameraXRotation.Value -= mouseY;
        cameraXRotation.Value = Mathf.Clamp(cameraXRotation.Value, -90f, 90f);

        //set angles and rotation
        transform.localRotation = Quaternion.Euler(cameraXRotation.Value, 0f, 0f);
        playerBody.transform.Rotate(Vector3.up*mouseX);
    }
}
