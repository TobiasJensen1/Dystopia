using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestJournalController : MonoBehaviour
{
    public Image journalImage;
    public GameObject QuestJournalWindow;
    bool active;

    public void OnEnter()
    {
        journalImage.color = Color.yellow;
    }

    public void OnExit()
    {
        journalImage.color = Color.white;
    }

    public void OnClick()
    {
        if (active)
        {
            QuestJournalWindow.SetActive(false);
            active = false;
        } else { 
        QuestJournalWindow.SetActive(true);
        active = true;
        }
    }

    public void OnClose()
    {
        QuestJournalWindow.SetActive(false);
        active = false;
    }
}
