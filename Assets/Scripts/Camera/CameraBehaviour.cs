using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{

    public GameObject player;
    public List<GameObject> questCharacters = new List<GameObject>();

    float z;
    float x;

    public bool cutScene;

    // Start is called before the first frame update
    void Start()
    {
        int amount = GameObject.Find("QuestCharacters").transform.childCount;


        for(int i = 0; i < amount; i++)
        {
            questCharacters.Add(GameObject.Find("QuestCharacters").transform.GetChild(i).gameObject);
        }
        

        //transform.position = new Vector3(transform.position.x + player.transform.position.x, transform.position.y + player.transform.position.y, transform.position.z);

        transform.position = new Vector3(
            player.transform.position.x + -5,
            transform.position.y +4 ,
            player.transform.position.z + 0);
        transform.LookAt(player.transform);

    }

    // Update is called once per frame
    void Update()
    {
        
        x = ((player.transform.position.x + -5 - transform.position.x)) / 2;
        z = ((player.transform.position.z + 0 - transform.position.z)) / 2;
        transform.position += new Vector3((x * 10 * Time.deltaTime), 0, (z * 10 * Time.deltaTime));
        

    }

    
}


    

