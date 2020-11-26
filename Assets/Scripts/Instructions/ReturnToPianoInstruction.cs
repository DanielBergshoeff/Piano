using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

[CreateAssetMenu]
public class ReturnToPianoInstruction : Instruction
{
    [FMODUnity.EventRef]
    public string PianoBeckon;

    [FMODUnity.EventRef]
    public string Final;

    public float TimePerUpdate = 3f;
    public StringVariable MyTrigger;
    public StringEvent MyTriggerEvent;

    private bool triggerHit = false;
    private Piano myPiano;
    private float timer;

    private EventInstance musicToPlay;
    private EventInstance finalMusic;


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

        musicToPlay = RuntimeManager.CreateInstance(PianoBeckon);
        musicToPlay.set3DAttributes(RuntimeUtils.To3DAttributes(myPiano.transform));
    }

    public override void OnStop() {
        MyTriggerEvent.Unregister(TriggerHit);

        finalMusic = RuntimeManager.CreateInstance(Final);
        finalMusic.set3DAttributes(RuntimeUtils.To3DAttributes(myPiano.transform));
        finalMusic.start();
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
            triggerHit = true;
        }
    }

    public void PlayComeHereSound() {
        musicToPlay.start();
    }
}
