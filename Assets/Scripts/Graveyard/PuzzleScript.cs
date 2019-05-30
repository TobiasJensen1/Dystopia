using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleScript : MonoBehaviour
{

    //Trigger mini boss
    public GameObject Necro;
    public GameObject dungeonEntranceObject;
    float distance;

    GameObject player;

    //doors
    public GameObject door1;
    public GameObject door2;
    bool isopen;
    float door1Y;
    float door2Y;

    //lever puzzle bool
    public  bool leverA;
    public  bool leverB;
    public  bool leverC;
    bool isactive;

    //Enable CorruptedKing
    public GameObject cKing;
    
    // Start is called before the first frame update
    void Start()
    {
    player = GameObject.Find("Player");    
    }

    // Update is called once per frame
    void Update()
    {
        if(leverA && leverB && leverC && !isactive)
        {
            StartCoroutine("Cutscene");
          
        }

        if (isopen)
        {
            dungeonEntrance();
        }


        if(Necro.GetComponent<EnemyStats>().health == 0 && !isopen)
        {
           
            door1.transform.eulerAngles = new Vector3(0, -90 ,0);
            door2.transform.eulerAngles = new Vector3(0, -90,0);
            isopen = true;
        }

    }

    IEnumerator Cutscene()
    {
        isactive = true;
        Necro.SetActive(true);
        Camera.main.GetComponent<CameraBehaviour>().cutScene = true;
        Camera.main.GetComponent<CameraBehaviour>().player = Necro;

        yield return new WaitForSeconds(3f);
       // Camera.main.GetComponent<CameraBehaviour>().player = Player.transform.gameObject;
        Camera.main.GetComponent<CameraBehaviour>().cutScene = false;
        Camera.main.GetComponent<CameraBehaviour>().player = player;

        StopCoroutine("Cutscene");
    
    }


    void dungeonEntrance()
    {
        distance = Vector3.Distance(dungeonEntranceObject.transform.position, player.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //Transport player to tombboss
            if (Input.GetMouseButtonDown(0) && hit.collider.name == "dungeonEntrance" && distance <= 4)
            {
                cKing.SetActive(true);
                GameObject.Find("Player").transform.position = new Vector3(175, 15, 232);
                GameObject.Find("Player").GetComponent<PlayerMovement>().moveTo = Vector3.MoveTowards(transform.position, new Vector3(175, 15, 232), 5 * Time.deltaTime);
            }
        }
    }




}
