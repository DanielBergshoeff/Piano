using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class TriggerObject : MonoBehaviour
{
    public StringEvent TriggerEvent;
    public StringVariable MyString;

    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Player"))
            return;

        TriggerEvent.Raise(MyString.Value);
    }
}
