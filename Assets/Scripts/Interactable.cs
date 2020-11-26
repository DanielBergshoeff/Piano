using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public StringVariable NecessaryObject;

    protected bool interactedWith = false;

    public abstract bool Interact(string itemHeld);
}
