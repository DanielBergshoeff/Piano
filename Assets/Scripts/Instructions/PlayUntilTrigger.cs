using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayUntilTriggerInstruction : Instruction
{
    public AudioClip SoundToPlay;

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
        myAudioSource = myPiano.gameObject.AddComponent<AudioSource>();
        myAudioSource.loop = true;
        myAudioSource.clip = SoundToPlay;
        myAudioSource.spatialBlend = 1f;
        myAudioSource.Play();
    }

    public override void OnStop() {
        myAudioSource.Stop();
    }

    public override void OnUpdate() {

    }
}
