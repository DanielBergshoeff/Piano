using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

[CreateAssetMenu]
public class PlayUntilTriggerInstruction : Instruction
{
    public AudioClip SoundToPlay;
    public StringVariable MyTrigger;
    public StringEvent MyStringEvent;

    private Piano myPiano;
    private bool triggerHit = false;
    private AudioSource myAudioSource;

    public override bool CheckForCompletion() {
        return triggerHit;
    }

    public override void OnInitialize(Piano piano) {
        myPiano = piano;
    }

    public override void OnStart() {
        triggerHit = false;
        MyStringEvent.Register(TriggerHit);
        myAudioSource = myPiano.gameObject.AddComponent<AudioSource>();
        myAudioSource.loop = true;
        myAudioSource.clip = SoundToPlay;
        myAudioSource.spatialBlend = 1f;
        myAudioSource.Play();
    }

    public override void OnStop() {
        MyStringEvent.Unregister(TriggerHit);
        myAudioSource.Stop();
    }

    public override void OnUpdate() {

    }

    public void TriggerHit(string trigger) {
        if(trigger == MyTrigger.Value) {
            triggerHit = true;
        }
    }
}
