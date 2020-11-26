using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    public AudioClip PickupSound;
    public StringVariable InventoryObject;

    private Vector3 startPosition;
    private Quaternion startRotation;
    private AudioSource myAudioSource;

    private void Awake() {
        myAudioSource = gameObject.AddComponent<AudioSource>();
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void PickedUp() {
        if (PickupSound != null)
            myAudioSource.PlayOneShot(PickupSound);
    }

    public void Dropped() {
        if (PickupSound != null)
            myAudioSource.PlayOneShot(PickupSound);

        transform.position = startPosition;
        transform.rotation = startRotation;
    }
}
