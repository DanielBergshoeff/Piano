using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityAtoms.MonoHooks;
using UnityEngine;

public class Piano : MonoBehaviour
{
    public List<InstructionToHints> Instructions;

    public GameObject Player;
    public StringEvent DialogueEvent;

    [HideInInspector]
    public AudioSource MyAudioSource;

    private Instruction currentInstruction;
    private List<Hint> currentHints;
    private int currentInstructionNr;
    private float currentInstructionTimer;

    // Start is called before the first frame update
    void Start()
    {
        MyAudioSource = gameObject.AddComponent<AudioSource>();
        MyAudioSource.spatialBlend = 1f;

        foreach(InstructionToHints ith in Instructions) {
            ith.MyInstruction.OnInitialize(this);
        }

        if(Instructions.Count > 0)
            SwitchInstructions(Instructions[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentInstruction == null)
            return;

        currentInstruction.OnUpdate();
        currentInstructionTimer += Time.deltaTime;

        for (int i = currentHints.Count -1; i >= 0; i--) {
            if (currentInstructionTimer > currentHints[i].StartHint) {
                DialogueEvent.Raise(currentHints[i].Text);
                currentHints.Remove(currentHints[i]);
            }
        }

        if (currentInstruction.CheckForCompletion()) {
            NextInstruction();
        }
    }

    public Transform GetTarget(Instruction i) {
        foreach(InstructionToHints ith in Instructions) {
            if(ith.MyInstruction == i) {
                return ith.Target;
            }
        }

        return null;
    }

    private void NextInstruction() {
        if (currentInstructionNr < Instructions.Count)
            SwitchInstructions(Instructions[currentInstructionNr]);
        else
            SwitchInstructions(null);
    }

    private void SwitchInstructions(InstructionToHints newInstruction) {
        if(currentInstruction != null) {
            currentInstruction.OnStop();
        }

        if (newInstruction == null) {
            currentInstruction = null;
        }
        else {
            currentInstruction = newInstruction.MyInstruction;
            currentHints = newInstruction.MyHints;
            currentInstructionNr++;
            Debug.Log(newInstruction.MyInstruction.name);
            currentInstructionTimer = 0f;

            if (currentInstruction != null)
                currentInstruction.OnStart();
        }
    }
}

[System.Serializable]
public class InstructionToHints
{
    public Instruction MyInstruction;
    public List<Hint> MyHints;
    public Transform Target;
}

[System.Serializable]
public class Hint
{
    public string Text;
    public float StartHint;
}
