using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZoneScript : MonoBehaviour
{
    bool isHabour;
    public Text text;


     void Start()
    {
        text.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            if (!isHabour)
            {

                text.GetComponent<Text>().text = "Blossom Valley";
                isHabour = true;
                StartCoroutine("textanimation");
            }
            else
            {
                StartCoroutine("textanimation");
                text.GetComponent<Text>().text = "Port Sarim";
                isHabour = false;
            }
        }
    }

    IEnumerator textanimation()
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        text.gameObject.SetActive(false);
        StopCoroutine("textanimation");
    }
     
}
