using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverScript : MonoBehaviour
{
    float distance;
    GameObject player;
   PuzzleScript Lever;

    bool clickable;
    
    // Start is called before the first frame update
    void Start()
    {
        Lever = GameObject.Find("Graveyard").transform.Find("Puzzle").transform.Find("Graveyard Puzzle").GetComponent<PuzzleScript>();

        player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(player.transform.position,transform.position);
        //print(distance + "" + transform.name);
        if(distance <= 3)
        {
            clickable = true;
        }
        else
        {
            clickable = false;
        }
      


    }

    void OnMouseDown()
    {
        if (clickable)
        {
            if (transform.name == "leverA")
            {
                Lever.leverA = true;
            }
            if (transform.name == "leverB")
            {
                Lever.leverB = true;
            }
            if (transform.name == "leverC")
            {
                Lever.leverC = true;
            }
        }
        else
        {
            print("Too far Away");
        }

    }
}
