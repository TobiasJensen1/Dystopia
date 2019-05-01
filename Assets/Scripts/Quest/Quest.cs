using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{

    public int QuestId;

    public string QuestName;
    public string QuestInformation;

    public bool isActive;
    public bool isComplete;

    public List<QuestEvent> questEvents;

    public enum status {Undiscovered, Started, Completed }
    public enum type {MainQuest, Event, SideQuest }

    public Quest(int QuestId, string QuestName, string QuestInformation, bool isActive, bool isComplete, List<QuestEvent> questEvents)
    {
        this.QuestId = QuestId;
        this.QuestName = QuestName;
        this.QuestInformation = QuestInformation;
        this.isActive = isActive;
        this.isComplete = isComplete;
        this.questEvents = questEvents;
        
    }

    public int getQuestId()
    {
        return QuestId;
    }

    public string getQuestName()
    {
        return QuestName;
    }

    public string getQuestInformation()
    {
        return QuestInformation;
    }

    public bool getisActive()
    {
        return isActive;
    }

    public bool getisComplete()
    {
        return isComplete;
    }

    public void setisActive(bool status)
    {
        this.isActive = status;
    }

    public void setisComplete(bool status)
    {
        this.isComplete = status;
    }

}
