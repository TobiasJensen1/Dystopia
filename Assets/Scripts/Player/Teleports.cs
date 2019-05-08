using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleports : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "TeleportCityFromFarm")
        {
            SceneManager.LoadScene(2);
            GetComponent<PlayerMovement>().moveTo = new Vector3(0, 15, 5);
        }
    }
}
