using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public abstract class Interactable : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string InteractSound;

    public StringVariable Name;
    public StringVariable NecessaryObject;

    protected bool interactedWith = false;

    private void Start() {
        gameObject.name = Name.Value;
    }

    public abstract bool Interact(string itemHeld);

    public virtual void PlaySound() {
        EventInstance interactSound = RuntimeManager.CreateInstance(InteractSound);
        interactSound.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
        interactSound.start();
    }
}
