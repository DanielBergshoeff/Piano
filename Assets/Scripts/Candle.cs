using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : Interactable
{
    public GameObject LightPart;

    public override bool Interact(string itemHeld) {
        if (interactedWith)
            return false;

        if(NecessaryObject != null) {
            if(NecessaryObject.Value == itemHeld) {
                Light();
                interactedWith = true;
                return true;
            }
        }

        return false;
    }

    public void Light() {
        LightPart.SetActive(true);
    }
}
