using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move self forwards, backwards, and strafe to the sides

public class FPPlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w")){
            transform.Translate(Vector3.forward * Time.deltaTime);
        }

        if (Input.GetKey("s")) {
            transform.Translate(Vector3.back * Time.deltaTime);
        }

        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * Time.deltaTime);
        }

        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * Time.deltaTime);
        }
    }
}
