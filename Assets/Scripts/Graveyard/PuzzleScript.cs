using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleScript : MonoBehaviour
{

   

    //lever puzzle bool
    public  bool leverA;
    public  bool leverB;
    public  bool leverC;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(leverA && leverB && leverC)
        {
            print("door opens");
        }
    }
}
