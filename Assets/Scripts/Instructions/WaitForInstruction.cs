using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

[CreateAssetMenu]
public class WaitForInstruction : Instruction
{
    public StringEvent EndEvent;
    public StringVariable MyValue;

    private bool eventCalled = false;

    public override bool CheckForCompletion() {
        return eventCalled; 
    }

    public override void OnInitialize(Piano piano) {

    }

    public override void OnStart() {
        eventCalled = false;
        EndEvent.Register(WaitEnded);
    }

    public override void OnStop() {
        EndEvent.Unregister(WaitEnded);
    }

    public override void OnUpdate() {

    }

    public void WaitEnded(string val) {
        if(MyValue.Value == val) {
            eventCalled = true;
        }
    }
}
