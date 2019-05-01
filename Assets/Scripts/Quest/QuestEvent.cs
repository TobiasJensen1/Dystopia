using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestEvent
{
    public int EventId;
    public string EventName;
    public string eventInformation;
    public bool isActive;
    public bool isComplete;

    public QuestEvent(int EventId, string EventName, string eventInformation, bool isActive, bool isComplete)
    {
        this.EventId = EventId;
        this.EventName = EventName;
        this.eventInformation = eventInformation;
        this.isActive = isActive;
        this.isComplete = isComplete;
    }

}
