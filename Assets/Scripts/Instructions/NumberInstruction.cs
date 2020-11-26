using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

[CreateAssetMenu]
public class NumberInstruction : Instruction
{
    public AudioClip NegativeSound;
    public AudioClip PositiveSound;
    public float SoundInterval = 0.3f;
    [Tooltip("True is positive, false is negative")]
    public List<bool> SoundTypes;
    
    public float UpdateTime = 5f;
    public StringVariable InteractionString;
    public StringEvent InteractionEvent;

    private Piano myPiano;
    private bool interacted = false;
    private float timer = 0f;

    private AudioSource myAudioSource;

    public override bool CheckForCompletion() {
        return interacted;
    }

    public override void OnInitialize(Piano piano) {
        myPiano = piano;
        myAudioSource = piano.gameObject.AddComponent<AudioSource>();
        myAudioSource.spatialBlend = 1f;
    }

    public override void OnStart() {
        interacted = false;
        InteractionEvent.Register(InteractionComplete);
        timer = UpdateTime;
    }

    public override void OnStop() {
        InteractionEvent.Unregister(InteractionComplete);
    }

    public override void OnUpdate() {
        timer += Time.deltaTime;
        if(timer >= UpdateTime) {
            float f = (timer - UpdateTime) % SoundInterval;
            float f2 = (timer - Time.deltaTime - UpdateTime) % SoundInterval;

            if (f <= Time.deltaTime && f2 > SoundInterval - Time.deltaTime) {
                int t = (int)((timer - UpdateTime) / SoundInterval);
                RepeatSound(t);
            }
        }
    }

    private void InteractionComplete(string s) {
        if(s == InteractionString.Value) {
            interacted = true;
        }
    }

    private void RepeatSound(int s) {
        if(s < SoundTypes.Count) {
            myAudioSource.PlayOneShot(SoundTypes[s] ? PositiveSound : NegativeSound);
        }
        else {
            timer = 0f;
        }
    }
}
