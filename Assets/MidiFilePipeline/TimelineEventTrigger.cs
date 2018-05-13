using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimelineEventTrigger : MonoBehaviour {

    public TimelineEvent m_event;

    void OnEnable () {
        m_event.Invoke();
    }

}

[System.Serializable]
public class TimelineEvent : UnityEvent
{
}