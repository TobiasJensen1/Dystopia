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

        //Dont move player when journal button is pressed
        GameObject.Find("Player").GetComponent<PlayerMovement>().moveTo = GameObject.Find("Player").transform.position;
        //If open, reclicking journal closes
        if (active)
        {
            QuestJournalWindow.SetActive(false);
            active = false;
            Camera.main.GetComponent<CameraBehaviour>().cutScene = false;
        } else { 
            //Open Journal
        QuestJournalWindow.SetActive(true);
        active = true;
            Camera.main.GetComponent<CameraBehaviour>().cutScene = true;
        }
    }

    public void OnClose()
    {
        //Close Journal
        QuestJournalWindow.SetActive(false);
        active = false;
        Camera.main.GetComponent<CameraBehaviour>().cutScene = false;
    }
}
