using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

[CreateAssetMenu]
public class MoveToInstruction : Instruction
{
    [FMODUnity.EventRef]
    public string HotColdEvent;

    public AudioClip PositiveSound;
    public AudioClip NeutralSound;
    public AudioClip NegativeSound;
    public AudioClip CompletedSound;

    public List<AudioLayer> AudioLayers;

    private Piano myPiano;
    private Transform target;
    private Vector3 startPosition;
    public float MinTimePerUpdate;
    public float MaxTimePerUpdate;
    public float MinMoveDistance;
    public float SuccesDistance;
    private float timer;

    private float startDistance;
    private List<AudioSource> myAudioSources;

    private EventInstance hotcold;

    public override void OnInitialize(Piano piano) {
        myPiano = piano;
        myAudioSources = new List<AudioSource>();
        foreach(AudioLayer al in AudioLayers) {
            AudioSource audioSource = piano.gameObject.AddComponent<AudioSource>();
            audioSource.clip = al.Clip;
            audioSource.loop = true;
            audioSource.spatialBlend = 1f;
            audioSource.volume = 0f;
            myAudioSources.Add(audioSource);
        }
    }

    public override void OnStart() {
        startPosition = myPiano.Player.transform.position;
        target = myPiano.GetTarget(this);
        timer = 0f;
        startDistance = (myPiano.Player.transform.position - target.position).sqrMagnitude;

        hotcold = RuntimeManager.CreateInstance(HotColdEvent);
        hotcold.set3DAttributes(RuntimeUtils.To3DAttributes(myPiano.transform));
        hotcold.setParameterByName("Distance", 0f);
        hotcold.start();

        foreach (AudioSource audioSource in myAudioSources) {
            audioSource.Play();
        }
    }

    public override void OnStop() {
        hotcold.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        foreach(AudioSource audioSource in myAudioSources) {
            audioSource.Stop();
        }
        myPiano.MyAudioSource.PlayOneShot(CompletedSound);
    }

    public override void OnUpdate() {
        timer += Time.deltaTime;
        float currentDistance = (myPiano.Player.transform.position - target.position).sqrMagnitude;
        float speed = Mathf.Clamp(currentDistance / startDistance, 0f, 1f);
        hotcold.setParameterByName("Distance", 1f - speed);
        if (timer > Mathf.Lerp(MinTimePerUpdate, MaxTimePerUpdate, speed)) {
            CheckDirection();
            timer = 0f;
        }

        for (int i = 0; i < AudioLayers.Count; i++) {
            if(AudioLayers[i].Off && AudioLayers[i].StartPoint < 1f - speed) {
                myAudioSources[i].volume = 1f;
                AudioLayers[i].Off = false;
            }
            else if(!AudioLayers[i].Off && AudioLayers[i].StartPoint > 1f - speed) {
                myAudioSources[i].volume = 0f;
                AudioLayers[i].Off = true;
            }
        }
    }

    public override bool CheckForCompletion() {
        float dist = (myPiano.Player.transform.position - target.position).sqrMagnitude;
        if(dist < SuccesDistance * SuccesDistance) {
            return true;
        }

        return false;
    }

    private void CheckDirection() {
        Vector3 dir = myPiano.Player.transform.position - startPosition;
        Vector3 targetDir = target.position - startPosition;
        float angle = Vector3.Angle(dir, targetDir);
        if (dir.sqrMagnitude > MinMoveDistance * MinMoveDistance) {
            if (angle < 90f || angle > 270f) { //Player has moved towards target
                PlayerMovedTowards();
            }
            else { //Player has moved away from target
                PlayerMovedAway();
            }
        }
        else {
            PlayerStayed();
        }

        startPosition = myPiano.Player.transform.position;
    }

    private void PlayerMovedTowards() {
        Debug.Log("Player moved towards target");
        myPiano.MyAudioSource.PlayOneShot(PositiveSound);
    }

    private void PlayerMovedAway() {
        Debug.Log("Player moved away from target");
        myPiano.MyAudioSource.PlayOneShot(NegativeSound);
    }

    private void PlayerStayed() {
        Debug.Log("Player didn't move");
        myPiano.MyAudioSource.PlayOneShot(NeutralSound);
    }
}

[System.Serializable]
public class AudioLayer
{
    public AudioClip Clip;
    public float StartPoint;
    [HideInInspector] public bool Off;
}
