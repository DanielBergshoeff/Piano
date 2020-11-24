using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEventVector3 : ScriptableObject
{
    /// <summary>
    /// The list of listeners that this event will notify if it is raised.
    /// </summary>
    private readonly List<GameEventVector3Listener> eventListeners =
        new List<GameEventVector3Listener>();

    public void Raise(Vector3 value) {
        for (int i = eventListeners.Count - 1; i >= 0; i--)
            eventListeners[i].OnEventRaised(value);
    }

    public void RegisterListener(GameEventVector3Listener listener) {
        if (!eventListeners.Contains(listener))
            eventListeners.Add(listener);
    }

    public void UnregisterListener(GameEventVector3Listener listener) {
        if (eventListeners.Contains(listener))
            eventListeners.Remove(listener);
    }
}
