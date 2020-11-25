using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityAtoms.MonoHooks;
using UnityEngine;

public class Piano : MonoBehaviour
{
    public List<Instruction> Instructions;
    public Instruction TestInstruction;

    public GameObject Player;

    [Header("MoveTo")]
    public float MinTimePerUpdate;
    public float MaxTimePerUpdate;
    public float MinMoveDistance;
    public float SuccesDistance;
    public Transform Target;

    [Header("Play until trigger")]
    public GameObject Trigger;


    [HideInInspector]
    public AudioSource MyAudioSource;

    private Instruction currentInstruction;
    private int currentInstructionNr;

    // Start is called before the first frame update
    void Start()
    {
        MyAudioSource = gameObject.AddComponent<AudioSource>();
        MyAudioSource.spatialBlend = 1f;

        TestInstruction.OnInitialize(this);
        SwitchInstructions(TestInstruction);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentInstruction == null)
            return;

        currentInstruction.OnUpdate();

        if (currentInstruction.CheckForCompletion()) {
            NextInstruction();
        }
    }

    private void NextInstruction() {
        if (currentInstructionNr + 1 < Instructions.Count)
            SwitchInstructions(Instructions[currentInstructionNr + 1]);
        else
            SwitchInstructions(null);
    }

    private void SwitchInstructions(Instruction newInstruction) {
        if(currentInstruction != null) {
            currentInstruction.OnStop();
        }

        currentInstruction = newInstruction;
        currentInstructionNr++;

        if(currentInstruction != null)
            currentInstruction.OnStart();
    }

    public void OnObjectTrigger(ColliderGameObject cgo) {
        if (cgo.GameObject == Trigger) {
            NextInstruction();
        }
    }
}
