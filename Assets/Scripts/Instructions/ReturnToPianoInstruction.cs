using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

[CreateAssetMenu]
public class ReturnToPianoInstruction : Instruction
{
    public AudioClip ReturnToPianoPart;
    public AudioClip ReturnToPianoFull;

    public float TimePerUpdate = 3f;
    public StringVariable MyTrigger;
    public StringEvent MyTriggerEvent;

    private bool triggerHit = false;
    private Piano myPiano;
    private float timer;

    public override bool CheckForCompletion() {
        return triggerHit;
    }

    public override void OnInitialize(Piano piano) {
        myPiano = piano;
    }

    public override void OnStart() {
        MyTriggerEvent.Register(TriggerHit);
        timer = 0f;
        triggerHit = false;
    }

    public override void OnStop() {
        MyTriggerEvent.Unregister(TriggerHit);
        myPiano.MyAudioSource.PlayOneShot(ReturnToPianoFull);
    }

    public override void OnUpdate() {
        timer += Time.deltaTime;
        if(timer >= TimePerUpdate) {
            timer = 0f;
            PlayComeHereSound();
        }
    }

    public void TriggerHit(string trigger) {
        if (trigger == MyTrigger.Value) {
            Debug.Log("Done");
            triggerHit = true;
        }
    }

    public void PlayComeHereSound() {
        Debug.Log("Come here!");
        myPiano.MyAudioSource.PlayOneShot(ReturnToPianoPart);
    }
}
