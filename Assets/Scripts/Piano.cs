using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piano : MonoBehaviour
{
    public List<Instruction> Instructions;
    public Instruction TestInstruction;

    public GameObject Player;
    public Transform Target;

    [Header("MoveTo")]
    public float MinTimePerUpdate;
    public float MaxTimePerUpdate;
    public float MinMoveDistance;
    public float SuccesDistance;

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
            if (currentInstructionNr + 1 < Instructions.Count)
                SwitchInstructions(Instructions[currentInstructionNr + 1]);
            else
                SwitchInstructions(null);
        }
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
}
