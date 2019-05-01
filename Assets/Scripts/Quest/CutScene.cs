using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutScene : MonoBehaviour
{
    List<GameObject> questCharacters;
    public GameObject informationText;

    public bool farmerEvent;
    public bool lastAbilities;
    public bool castleWall;

    public GameObject wallSegment;

    //Enemychunks, activated when hitting certain colliders
    public GameObject blossomValleySpiders;
    public GameObject farmerTrolls;
    public GameObject castleWallEnemies;

    private void Start()
    {
        questCharacters = Camera.main.GetComponent<CameraBehaviour>().questCharacters;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "IsshinaCollider")
        {
            other.enabled = false;
            StartCoroutine("isshina");
        }

        if (other.name == "ThalienCollider")
        {
            other.enabled = false;
            blossomValleySpiders.SetActive(true);
            StartCoroutine("thalien");
        }
        if (other.name == "ForestBlockade")
        {

            informationText.GetComponent<Text>().text = "Dangers lurk ahead, speak with Thalien first";
            StartCoroutine("blocker");
            transform.GetComponent<PlayerMovement>().moveTo = new Vector3(transform.position.x + 5, transform.position.y, transform.position.z - 7);
        }
        if (other.name == "FarmerCollider")
        {
            other.enabled = false;
            farmerTrolls.SetActive(true);
            StartCoroutine("farmer");
        }
        if(other.name == "CastleWallCollider")
        {
            other.enabled = false;
            castleWallEnemies.SetActive(true);
            StartCoroutine("wall");
        }




    }
    //Habour
    public IEnumerator isshina()
    {
        cutScene(questCharacters[0], new Vector3(-6.3f, 15, -14));
        yield return new WaitForSeconds(3f);
        Camera.main.GetComponent<CameraBehaviour>().player = transform.gameObject;
        Camera.main.GetComponent<CameraBehaviour>().cutScene = false;
        StopCoroutine("isshina");
    }
    //Forest

    public IEnumerator thalien()
    {
        cutScene(questCharacters[1], new Vector3(25.5f, 15, 41.2f));
        yield return new WaitForSeconds(3f);
        Camera.main.GetComponent<CameraBehaviour>().player = transform.gameObject;
        Camera.main.GetComponent<CameraBehaviour>().cutScene = false;
        StopCoroutine("thalien");
    }
    //Farm
    public IEnumerator farmer()
    {
        while (true)
        {
            interactableCutScene(questCharacters[2], "farmer");
            yield return null;
        }
    }
    //Troll General
    public IEnumerator trollgeneral(GameObject boss, Vector3 pos)
    {
        cutScene(boss, pos);
        boss.GetComponent<Animator>().Play("Taunt");
        yield return new WaitForSeconds(3f);
        Camera.main.GetComponent<CameraBehaviour>().player = transform.gameObject;
        Camera.main.GetComponent<CameraBehaviour>().cutScene = false;
        StopCoroutine("trollgeneral");
        lastAbilities = true;

    }
    
    //Castle wall event
    public IEnumerator wall()
    {
        cutScene(wallSegment, transform.position);
        yield return new WaitForSeconds(3f);
        Camera.main.GetComponent<CameraBehaviour>().player = transform.gameObject;
        Camera.main.GetComponent<CameraBehaviour>().cutScene = false;
        StopCoroutine("wall");
        castleWall = true;
    }
    

    public IEnumerator blocker()
    {
        Camera.main.GetComponent<CameraBehaviour>().cutScene = true;
        informationText.SetActive(true);
        yield return new WaitForSeconds(2f);
        Camera.main.GetComponent<CameraBehaviour>().cutScene = false;
        informationText.SetActive(false);
        StopCoroutine("blocker");
    }
    public void cutScene(GameObject character, Vector3 pos)
    {
        transform.position = pos;
        transform.GetComponent<Animator>().Play("Idle");
        Camera.main.GetComponent<CameraBehaviour>().cutScene = true;
        Camera.main.GetComponent<CameraBehaviour>().player = character;
    }

    public void interactableCutScene(GameObject character, string coName)
    {

        float distance = Vector3.Distance(transform.position, character.transform.position);
        if (distance >= 2)
        {
            Camera.main.GetComponent<CameraBehaviour>().cutScene = true;
            GetComponent<PlayerMovement>().moveTo = character.transform.position;
        }
        else
        {
            GetComponent<PlayerMovement>().moveTo = transform.position;
            Camera.main.GetComponent<CameraBehaviour>().cutScene = false;
            farmerEvent = true;
            StopCoroutine(coName);
        }
    }
}
