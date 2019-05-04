using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleScript : MonoBehaviour
{

    //Trigger mini boss
    public GameObject Necro;
    GameObject Player;
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
