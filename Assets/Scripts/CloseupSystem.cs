using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UI;

public class CloseupSystem : MonoBehaviour
{
    public StringEvent EndCloseUpEvent;
    public List<CloseUp> CloseUps;

    public Image closeUpImage;

    private bool currentlyEngaged = false;
    private CloseUp currentCloseUp;

    // Start is called before the first frame update
    void Start()
    {
        closeUpImage.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentlyEngaged && Input.GetMouseButtonDown(1))
            StopCloseUp();
    }

    public void DoCloseUp(string name) {
        if (currentlyEngaged)
            return;

        foreach(CloseUp cu in CloseUps) {
            if (cu.Name.Value == name) {
                closeUpImage.enabled = true;
                closeUpImage.sprite = cu.MySprite;
                currentlyEngaged = true;
                currentCloseUp = cu;
                return;
            }
        }
    }

    public void StopCloseUp() {
        closeUpImage.enabled = false;
        currentlyEngaged = false;
        EndCloseUpEvent.Raise(currentCloseUp.Name.Value);
    }
}

[System.Serializable]
public class CloseUp {
    public StringVariable Name;
    public Sprite MySprite;
}
