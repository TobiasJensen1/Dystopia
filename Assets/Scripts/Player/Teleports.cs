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
        if(other.name == "EnterCityCollider")
        {
            transform.position = new Vector3(160, 15, 320);
            transform.GetComponent<PlayerMovement>().moveTo = Vector3.MoveTowards(transform.position, new Vector3(160,15,320), 5 * Time.deltaTime);
        }
        if (other.name == "ExitCityCollider")
        {
            transform.position = new Vector3(135, 15, 160);
            transform.GetComponent<PlayerMovement>().moveTo = Vector3.MoveTowards(transform.position, new Vector3(135, 15, 160), 5 * Time.deltaTime);
        }
    }
}
