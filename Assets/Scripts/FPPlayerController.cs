using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;

//Move self forwards, backwards, and strafe to the sides

public class FPPlayerController : MonoBehaviour
{
    public FloatVariable walkSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w")){
            transform.Translate(Vector3.forward * Time.deltaTime * walkSpeed.InitialValue);
        }

        if (Input.GetKey("s")) {
            transform.Translate(Vector3.back * Time.deltaTime * walkSpeed.InitialValue);
        }

        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * Time.deltaTime * walkSpeed.InitialValue);
        }

        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * Time.deltaTime * walkSpeed.InitialValue);
        }
    }
}
