using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class abilityInformation : MonoBehaviour
{
    public GameObject abilityWindow;
    public GameObject abilityText;

    // Start is called before the first frame update
    void Start()
    {
        abilityWindow.SetActive(false);
        abilityText.GetComponent<Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void mainAbiity()
    {
        abilityWindow.SetActive(true);
        abilityText.GetComponent<Text>().text = "Basic Ability\n Fury: 0\n A Mighty Cleave\n used to regenerate your Fury";
    }

    public void qAbility()
    {
        abilityWindow.SetActive(true);
        abilityText.GetComponent<Text>().text = "Slice Ability\n Fury: 25\n A Strong slice to pierce\n your enemies";
    }

    public void wAbility()
    {
        abilityWindow.SetActive(true);
        abilityText.GetComponent<Text>().text = "AOE Ability\n Fury: 40\n A Whirlwind attack used\n to attack all enemies around you";
    }

    public void eAbility()
    {
        abilityWindow.SetActive(true);
        abilityText.GetComponent<Text>().text = "Taunt Ability\n Fury: 40\n A Taunt used to\n increase your damage";
    }

    public void rAbility()
    {
        abilityWindow.SetActive(true);
        abilityText.GetComponent<Text>().text = "Ultimate Ability\n Fury: 50\n Empty all your Fury\n to unleash a mighty blow";
    }

    public void healthPotion()
    {
        abilityWindow.SetActive(true);
        abilityText.GetComponent<Text>().text = "Health Potion\n CoolDown: 25 Seconds\n Health Potion, used to regain 50 health;";
    }
    public void furyPotion()
    {
        abilityWindow.SetActive(true);
        abilityText.GetComponent<Text>().text = "Fury Potion\n CoolDown: 25 Seconds\n Fury Potion, used to regain 50 Fury;";
    }
    public void armorUpgrade()
    {
        abilityWindow.SetActive(true);
        abilityText.GetComponent<Text>().text = "Upgrade Armor\n Used to upgrade your armor;";
    }

    public void closeWindow()
    {
        abilityWindow.SetActive(false);
    }
}
