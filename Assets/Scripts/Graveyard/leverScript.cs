using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverScript : MonoBehaviour
{
   PuzzleScript Lever;
    
    // Start is called before the first frame update
    void Start()
    {
        Lever = GameObject.Find("Graveyard").transform.Find("Puzzle").transform.Find("Graveyard Puzzle").GetComponent<PuzzleScript>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(transform.name == "leverA") {
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


}
