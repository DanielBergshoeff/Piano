using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEventVector3Listener : MonoBehaviour
{
    [Tooltip("Event to register with.")]
    public GameEventVector3 Event;

    [Tooltip("Response to invoke when Event is raised.")]
    public Vector3Event Response;

    private void OnEnable() {
        Event.RegisterListener(this);
    }

    private void OnDisable() {
        Event.UnregisterListener(this);
    }

    public void OnEventRaised(Vector3 value) {
        Response.Invoke(value);
    }
}

[System.Serializable]
public class Vector3Event : UnityEvent<Vector3> { }
