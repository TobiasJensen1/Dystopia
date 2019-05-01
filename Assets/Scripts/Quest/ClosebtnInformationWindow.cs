using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClosebtnInformationWindow : MonoBehaviour
{
    public Text closebtnText;
    public GameObject informationWindow;
    bool firstQuestAccepted;
    public bool closebtn;

    private void Start()
    {
        firstQuestAccepted = GameObject.Find("QuestController").GetComponent<QuestHandler>().firstQuestAccepted;
    }

    public void OnEnter()
    {
        closebtnText.color = Color.white;
    }

    public void OnExit()
    {
        closebtnText.color = Color.black;
    }


    public void close()
    {
        //bool to make sure first quest is only run on initial enable
        if (!firstQuestAccepted)
        {
            firstQuestAccepted = true;
        }
        informationWindow.SetActive(false);
        GameObject.Find("Controllers").transform.Find("QuestController").GetComponent<QuestHandler>().informationWindowActive = false;
        closebtn = true;
    }
    
}
