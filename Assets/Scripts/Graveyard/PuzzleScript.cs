using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleScript : MonoBehaviour
{

    //Trigger mini boss
    public GameObject Necro;

    GameObject Player;

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
    
    // Start is called before the first frame update
    void Start()
    {
    Player = GameObject.Find("Player");    
    }

    // Update is called once per frame
    void Update()
    {
        if(leverA && leverB && leverC && !isactive)
        {
            StartCoroutine("Cutscene");
          
        }


        if(Necro.GetComponent<EnemyStats>().health == 0 && !isopen)
        {

            //not opening the door smoothly (need a fix in the future ZZzzz)
            door1Y += door1.transform.eulerAngles.y * 0.0006f * Time.deltaTime;
            door2Y += door2.transform.eulerAngles.y * 0.0006f * Time.deltaTime;
          
            door1.transform.eulerAngles = new Vector3(-90, -door1Y,0);
            door2.transform.eulerAngles = new Vector3(-90, door2Y,0);
            if (door1.transform.eulerAngles.y >= 90f && door1.transform.eulerAngles.y >= 90f)
            {
                isopen = true;
            }
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
        Camera.main.GetComponent<CameraBehaviour>().player = Player;

        StopCoroutine("Cutscene");
    
    }




}
