using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string PickupSound;

    [FMODUnity.EventRef]
    public string DroppedSound;

    public string Name;

    public StringVariable InventoryObject;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private AudioSource myAudioSource;

    private void Awake() {
        myAudioSource = gameObject.AddComponent<AudioSource>();
        startPosition = transform.position;
        startRotation = transform.rotation;
        gameObject.name = Name;
    }

    public void PickedUp() {
        EventInstance pickupSound = RuntimeManager.CreateInstance(PickupSound);
        pickupSound.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
        pickupSound.start();
    }

    public void Dropped() {
        EventInstance droppedSound = RuntimeManager.CreateInstance(DroppedSound);
        droppedSound.set3DAttributes(RuntimeUtils.To3DAttributes(transform));
        droppedSound.start();

        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
