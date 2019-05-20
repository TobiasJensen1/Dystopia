using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorController : MonoBehaviour
{
    public Image armorImage;
    bool armor1Check;
    bool armor2Check;

    public Material armor1;
    public Material armor2;
    public GameObject armor;



    public void OnEnter()
    {
        armorImage.color = Color.yellow;
    }

    public void OnExit()
    {
        armorImage.color = Color.white;
    }

    public void OnClick()
    {

        //Dont move player when button is pressed
        GameObject.Find("Player").GetComponent<PlayerMovement>().moveTo = GameObject.Find("Player").transform.position;
        if (!armor1Check && !armor2Check)
        {

            armor.GetComponent<Renderer>().material = armor1;
            armor1Check = true;
        }
        else if (armor1Check && !armor2Check)
        {
            armor.GetComponent<Renderer>().material = armor2;
            armor2Check = true;
        }
    }
}
