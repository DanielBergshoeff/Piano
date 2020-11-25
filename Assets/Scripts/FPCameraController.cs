using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rotate self using mouse position

public class FPCameraController : MonoBehaviour
{
    public float mouseSensitivity;
    public GameObject playerBody;
    float cameraXRotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.mousePosition);
    }
}
