using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

[CreateAssetMenu]
public class WaitForInstruction : Instruction
{
    [FMODUnity.EventRef]
    public string DoSomethingSound;
    public float TimePerUpdate = 3f;

    public StringEvent EndEvent;
    public StringVariable MyValue;

    private bool eventCalled = false;
    private float timer;
    private Piano myPiano;

    public override bool CheckForCompletion() {
        return eventCalled; 
    }

    public override void OnInitialize(Piano piano) {
        myPiano = piano;
    }

    public override void OnStart() {
        eventCalled = false;
        timer = 0f;
        EndEvent.Register(WaitEnded);
    }

    public override void OnStop() {
        EndEvent.Unregister(WaitEnded);
    }

    public override void OnUpdate() {
        timer += Time.deltaTime;
        if (timer >= TimePerUpdate) {
            timer = 0f;
            PlayDoSomethingSound();
        }
    }

    public void WaitEnded(string val) {
        if(MyValue.Value == val) {
            eventCalled = true;
        }
    }

    public void PlayDoSomethingSound() {
        EventInstance soundToPlay = RuntimeManager.CreateInstance(DoSomethingSound);
        soundToPlay.set3DAttributes(RuntimeUtils.To3DAttributes(myPiano.transform));
        soundToPlay.start();
    }
}
