using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public StringVariable Name;
    public StringVariable NecessaryObject;

    protected bool interactedWith = false;

    private void Start() {
        gameObject.name = Name.Value;
    }

    public abstract bool Interact(string itemHeld);
}
