using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MoveToInstruction : Instruction
{
    public AudioClip PositiveSound;
    public AudioClip NeutralSound;
    public AudioClip NegativeSound;
    public AudioClip CompletedSound;

    private Piano myPiano;
    private Transform target;
    private Vector3 startPosition;
    private float minTimePerUpdate;
    private float maxTimePerUpdate;
    private float minMoveDistance;
    private float succesDistance;
    private float timer;

    private float startDistance;

    public override void OnInitialize(Piano piano) {
        myPiano = piano;
    }

    public override void OnStart() {
        startPosition = myPiano.Player.transform.position;
        minTimePerUpdate = myPiano.MinTimePerUpdate;
        maxTimePerUpdate = myPiano.MaxTimePerUpdate;
        minMoveDistance = myPiano.MinMoveDistance;
        succesDistance = myPiano.SuccesDistance;
        target = myPiano.Target;
        timer = 0f;
        startDistance = (myPiano.Player.transform.position - target.position).sqrMagnitude;
    }

    public override void OnStop() {
        myPiano.MyAudioSource.PlayOneShot(CompletedSound);
    }

    public override void OnUpdate() {
        timer += Time.deltaTime;
        float currentDistance = (myPiano.Player.transform.position - target.position).sqrMagnitude;
        float speed = Mathf.Clamp(currentDistance / startDistance, 0f, 1f);
        if (timer > Mathf.Lerp(minTimePerUpdate, maxTimePerUpdate, speed)) {
            CheckDirection();
            timer = 0f;
        }
    }

    public override bool CheckForCompletion() {
        float dist = (myPiano.Player.transform.position - target.position).sqrMagnitude;
        if(dist < succesDistance * succesDistance) {
            return true;
        }

        return false;
    }

    private void CheckDirection() {
        Vector3 dir = myPiano.Player.transform.position - startPosition;
        Vector3 targetDir = target.position - startPosition;
        float angle = Vector3.Angle(dir, targetDir);
        if (dir.sqrMagnitude > minMoveDistance * minMoveDistance) {
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
        myPiano.MyAudioSource.PlayOneShot(PositiveSound);
    }

    private void PlayerMovedAway() {
        Debug.Log("Player moved away from target");
        myPiano.MyAudioSource.PlayOneShot(NegativeSound);
    }

    private void PlayerStayed() {
        Debug.Log("Player didn't move");
        myPiano.MyAudioSource.PlayOneShot(NeutralSound);
    }
}
