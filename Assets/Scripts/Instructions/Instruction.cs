using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Instruction : ScriptableObject
{
    public abstract void OnInitialize(Piano piano);
    public abstract void OnStart();
    public abstract void OnUpdate();
    public abstract void OnStop();
    public abstract bool CheckForCompletion();
}
