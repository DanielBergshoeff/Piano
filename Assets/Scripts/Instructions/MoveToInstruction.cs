using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

[CreateAssetMenu]
public class MoveToInstruction : Instruction
{
    [FMODUnity.EventRef]
    public string HotColdEvent;

    [FMODUnity.EventRef]
    public string MovedTowardsSound;

    [FMODUnity.EventRef]
    public string MovedAwaySound;


    private Piano myPiano;
    private Transform target;
    private Vector3 startPosition;
    public float MinTimePerUpdate;
    public float MaxTimePerUpdate;
    public float MinMoveDistance;
    public float SuccesDistance;
    private float timer;

    private float startDistance;

    private EventInstance hotcold;

    public override void OnInitialize(Piano piano) {
        myPiano = piano;
    }

    public override void OnStart() {
        startPosition = myPiano.Player.transform.position;
        target = myPiano.GetTarget();
        Debug.Log(target.name);
        timer = 0f;
        startDistance = (myPiano.Player.transform.position - target.position).sqrMagnitude;

        hotcold = RuntimeManager.CreateInstance(HotColdEvent);
        hotcold.set3DAttributes(RuntimeUtils.To3DAttributes(myPiano.transform));
        hotcold.setParameterByName("Distance", 0f);
        hotcold.start();
    }

    public override void OnStop() {
        hotcold.setParameterByName("Complete", 1f);
    }

    public override void OnUpdate() {
        timer += Time.deltaTime;
        float currentDistance = (myPiano.Player.transform.position - target.position).sqrMagnitude;
        float speed = Mathf.Clamp(currentDistance / startDistance, 0f, 1f);
        hotcold.setParameterByName("Distance", 1f - speed);
        if (timer > Mathf.Lerp(MinTimePerUpdate, MaxTimePerUpdate, speed)) {
            CheckDirection();
            timer = 0f;
        }
    }

    public override bool CheckForCompletion() {
        float dist = (myPiano.Player.transform.position - target.position).sqrMagnitude;
        if(dist < SuccesDistance * SuccesDistance) {
            Debug.Log(dist);
            return true;
        }

        return false;
    }

    private void CheckDirection() {
        Vector3 dir = myPiano.Player.transform.position - startPosition;
        Vector3 targetDir = target.position - startPosition;
        float angle = Vector3.Angle(dir, targetDir);
        if (dir.sqrMagnitude > MinMoveDistance * MinMoveDistance) {
            if (angle < 90f || angle > 270f) { //Player has moved towards target
                PlayerMovedTowards();
            }
            else { //Player has moved away from target
                PlayerMovedAway();
            }
        }
        else {
            PlayerStayed();
        }

        startPosition = myPiano.Player.transform.position;
    }

    private void PlayerMovedTowards() {
        Debug.Log("Player moved towards target");
        EventInstance movedTowards = RuntimeManager.CreateInstance(MovedTowardsSound);
        movedTowards.set3DAttributes(RuntimeUtils.To3DAttributes(myPiano.transform));
        movedTowards.start();
    }

    private void PlayerMovedAway() {
        Debug.Log("Player moved away from target");
        EventInstance movedAway = RuntimeManager.CreateInstance(MovedAwaySound);
        movedAway.set3DAttributes(RuntimeUtils.To3DAttributes(myPiano.transform));
        movedAway.start();
    }

    private void PlayerStayed() {
        Debug.Log("Player didn't move");
    }
}

[System.Serializable]
public class AudioLayer
{
    public AudioClip Clip;
    public float StartPoint;
    [HideInInspector] public bool Off;
}
