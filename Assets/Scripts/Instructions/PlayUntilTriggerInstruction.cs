using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

[CreateAssetMenu]
public class PlayUntilTriggerInstruction : Instruction
{
    [FMODUnity.EventRef]
    public string SoundToPlay;
    public StringVariable MyTrigger;
    public StringEvent MyStringEvent;

    private Piano myPiano;
    private bool triggerHit = false;
    private EventInstance musicToPlay;

    public override bool CheckForCompletion() {
        return triggerHit;
    }

    public override void OnInitialize(Piano piano) {
        myPiano = piano;
    }

    public override void OnStart() {
        triggerHit = false;
        MyStringEvent.Register(TriggerHit);

        musicToPlay = RuntimeManager.CreateInstance(SoundToPlay);
        musicToPlay.set3DAttributes(RuntimeUtils.To3DAttributes(myPiano.transform));
        musicToPlay.start();
    }

    public override void OnStop() {
        MyStringEvent.Unregister(TriggerHit);

        musicToPlay.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public override void OnUpdate() {

    }

    public void TriggerHit(string trigger) {
        if(trigger == MyTrigger.Value) {
            triggerHit = true;
        }
    }
}
