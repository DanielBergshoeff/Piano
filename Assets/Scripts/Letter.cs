using FMOD.Studio;
using FMODUnity;
using System.Collections; 
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class Letter : Interactable
{
    public StringEvent CloseUpEvent; 
    public StringVariable LetterCloseUp;

    public override bool Interact(string itemHeld) {
        PlaySound();
        CloseUpEvent.Raise(LetterCloseUp.Value);
        return true;
    }
}
