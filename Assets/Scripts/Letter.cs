using System.Collections; 
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

public class Letter : Interactable
{
    public StringEvent CloseUpEvent; 
    public StringVariable LetterCloseUp;

    public override bool Interact(string itemHeld) {
        CloseUpEvent.Raise(LetterCloseUp.Value);
        return true;
    }
}
