using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestHandler : MonoBehaviour
{
    public GameObject player;
    //informationwindow
    public GameObject title;
    public GameObject information;
    public GameObject informationWindow;

    //QuestJournal
    public GameObject journalTitle;
    public GameObject journalInformation;
    public GameObject objList;

    //For unlocking abilities
    public GameObject qObj;
    public GameObject wObj;
    public GameObject eObj;
    public GameObject rObj;
    public bool q;
    public bool w;
    public bool e;
    public bool r;

    public GameObject worldLight;
    public bool firstQuestAccepted;

    public List<Quest> quests;
    public List<QuestEvent> questEvents;
    bool eventActive;
    public bool informationWindowActive;

    //Used for killcount events
    public GameObject killCounter;
    public int currKills = 0;
    int totalKills;
    string enemyType;

    //Used for bossevents triggered after informationwindow close
    public GameObject closeButton;
    GameObject boss;

    //Used to control which event object are being interacted with
    float distance;
    GameObject questChar;


    void OnEnable()
    {
        //forestTest, reverse if needed
        worldLight.GetComponent<Light>().enabled = false;
        //q = true;
        //qObj.SetActive(true);

        //ForestSpiderTest, reverse if needed
        //worldLight.GetComponent<Light>().enabled = false;
        //q = true;
        //qObj.SetActive(true);
        //w = true;
        //wObj.SetActive(true);

        //Make sure the first quest doesnt launch at every enable
        if (!firstQuestAccepted)
        {
            quests = GetComponent<QuestList>().quests;
            questEvents = quests[0].questEvents;
            //Initial quest setup
            informationWindowActive = true;
            title.GetComponent<Text>().text = quests[0].QuestName;
            information.GetComponent<Text>().text = quests[0].QuestInformation;
            quests[0].isActive = true;

            //Initial journal setup
            journalTitle.GetComponent<Text>().text = quests[0].QuestName;
            journalInformation.GetComponent<Text>().text = quests[0].QuestInformation;
            objList.GetComponent<Text>().text += "\n • " + questEvents[0].EventName;


        }
    }

    public void Update()
    {
        questController();
    }

    public void questController()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //First event, If isshina is clicked on, move until distance is less than 2 and open informationwindow
            if (questChar != null)
            {
                distance = Vector3.Distance(questChar.transform.position, player.transform.position);
            }

            //If player clicks isshina
            if (Input.GetMouseButtonDown(0) && hit.transform.name == "Isshina")
            {
                questChar = hit.transform.gameObject;
                eventActive = true;
            }

            //If player is close enough to isshina to open informationWindow
            if (questChar != null)
            {
                if (distance <= 3 && questChar.name == "Isshina")
                {
                    if (eventActive)
                    {
                        if (!questEvents[0].isComplete)
                        {
                            objList.GetComponent<Text>().text += "\n • " + questEvents[1].EventName;
                            //Event enables q ability
                            q = true;
                            qObj.SetActive(true);
                        }
                        openInformationwindow(questEvents[0]);
                        eventActive = false;
                    }
                }
            }

            //If tavern stairs are pressed
            if (Input.GetMouseButtonDown(0) && hit.transform.name == "Stairs")
            {
                questChar = hit.transform.gameObject;
                eventActive = true;
            }

            if (questChar != null)
            {
                if (distance <= 3 && questChar.name == "Stairs" && questEvents[0].isComplete)
                {
                    if (eventActive)
                    {
                        //Cannot rest multiple times
                        if (!questEvents[1].isComplete)
                        {
                            //Habour blockade removed, fury and health maxed, worldlight enabled
                            transform.Find("PirateBlockade").transform.gameObject.SetActive(false);
                            player.GetComponent<PlayerStats>().health = player.GetComponent<PlayerStats>().maxHealth;
                            player.GetComponent<PlayerStats>().fury = player.GetComponent<PlayerStats>().maxFury;
                            worldLight.GetComponent<Light>().enabled = true;
                        }
                        openInformationwindow(questEvents[1]);
                        eventActive = false;
                    }
                }
            }

            //If thalien is pressed
            if (Input.GetMouseButtonDown(0) && hit.transform.name == "Thalien")
            {
                questChar = hit.transform.gameObject;
                eventActive = true;
            }

            if (questChar != null)
            {
                if (distance <= 3 && questChar.name == "Thalien")
                {
                    if (eventActive)
                    {
                        if (!questEvents[2].isComplete)
                        {
                            objList.GetComponent<Text>().text += "\n • " + questEvents[2].EventName;
                            transform.Find("ForestBlockade").transform.gameObject.SetActive(false);
                            //Event enables w ability
                            w = true;
                            wObj.SetActive(true);
                        }
                        openInformationwindow(questEvents[2]);
                        eventActive = false;
                    }
                }
            }

            //FarmerEvent
            if (player.GetComponent<CutScene>().farmerEvent)
            {
                eventActive = true;

                if (eventActive)
                {
                    totalKills = GameObject.Find("FarmerEvent").transform.childCount - 1;
                    enemyType = "Trolls";

                    if (!questEvents[3].isComplete)
                    {
                        objList.GetComponent<Text>().text += "\n • " + questEvents[3].EventName;
                        openInformationwindow(questEvents[3]);
                        killCounter.SetActive(true);
                    }
                }
                eventActive = false;
                killCounter.GetComponent<Text>().text = currKills + " / " + totalKills + " " + enemyType + " killed";

                //if all trolls are killed, start troll boss event
                if (currKills == totalKills)
                {
                    if (!questEvents[4].isComplete)
                    {
                        closeButton.GetComponent<ClosebtnInformationWindow>().closebtn = false;
                        objList.GetComponent<Text>().text += "\n • " + questEvents[4].EventName;
                        openInformationwindow(questEvents[4]);
                        killCounter.SetActive(false);
                    }
                    //When informationwindow is closed, show troll general spawn cutscene
                    if (closeButton.GetComponent<ClosebtnInformationWindow>().closebtn && !questEvents[5].isActive)
                    {
                        boss = GameObject.Find("FarmerEvent").transform.Find("TrollGeneral").gameObject;
                        boss.SetActive(true);
                        player.GetComponent<CutScene>().StartCoroutine(player.GetComponent<CutScene>().trollgeneral(boss, player.transform.position));
                        closeButton.GetComponent<ClosebtnInformationWindow>().closebtn = false;

                    }
                    //When cutscene is over, teach final abilites
                    if (player.GetComponent<CutScene>().lastAbilities)
                    {
                        if (!questEvents[5].isComplete)
                        {
                            objList.GetComponent<Text>().text += "\n • " + questEvents[5].EventName;
                            openInformationwindow(questEvents[5]);
                            //Event enables e & r ability
                            e = true;
                            eObj.SetActive(true);
                            r = true;
                            rObj.SetActive(true);
                        }
                    }
                }
            }
            //Caste wall event
            if (player.GetComponent<CutScene>().castleWall)
            {


                if (!questEvents[6].isComplete)
                {
                    objList.GetComponent<Text>().text += "\n • " + questEvents[6].EventName;
                    openInformationwindow(questEvents[6]);

                }
            }
        }
    }

            public void openInformationwindow(QuestEvent questevent)
            {
                questevent.isActive = true;

                if (!questevent.isComplete)
                {
                    player.GetComponent<Animator>().Play("Idle");
                    player.GetComponent<PlayerMovement>().moveTo = player.transform.position;
                    informationWindowActive = true;
                    title.GetComponent<Text>().text = questevent.EventName;
                    information.GetComponent<Text>().text = questevent.eventInformation;
                    informationWindow.SetActive(true);
                    questevent.isComplete = true;


                }
                else
                {
                    informationWindowActive = true;
                    if (questevent.EventName == "Talk to Asshina")
                    {
                        questevent.eventInformation = "I would suggest you head to the nearby tavern to get some sleep, before you head north into the Blossom Valley. Just go upstairs in the Tavern, the King made sure to book a room for you.";
                    }
                    if (questevent.EventName == "Sleep at the Tavern")
                    {
                        questevent.eventInformation = "The King only booked the room for one night, you will have to move onwards. He is expecting your arrival at the City, past the Blossom Valley to the north.";
                    }
                    title.GetComponent<Text>().text = questevent.EventName;
                    information.GetComponent<Text>().text = questevent.eventInformation;
                    informationWindow.SetActive(true);
                }
            }

        }
