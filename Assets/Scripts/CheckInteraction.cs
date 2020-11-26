using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms.BaseAtoms;
using UnityEngine.UI;
using TMPro;

//this script checks for objects with tag 'interactable' using raycasts
//displays the corresponding prompt

public class CheckInteraction : MonoBehaviour
{
    [Tooltip("The range at which you can detect if objects are interactable")]
    public FloatVariable raycastRange;
    
    [Tooltip("Insert the TMP Text data type object")]
    public TextMeshProUGUI promptInteract;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, raycastRange.InitialValue)) { //if you hit something
            if (hit.transform.tag != "Interactable")
            {
                promptInteract.SetText("");
            }
            else
            {
                promptInteract.SetText("Press 'E' to interact");
            }
        }

        
    }
}
