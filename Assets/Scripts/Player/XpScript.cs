using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XpScript : MonoBehaviour
{
   PlayerStats Player_level;
   public Image barImage;
    const int max_XP = 100;
    public float xp;
    


    void Start()
    {
        Player_level = GameObject.Find("Player").GetComponent<PlayerStats>();
        barImage.fillAmount = 0;   
    }

    void Update()
    {
        XpLogic();
    }


    void XpLogic()
    {
        barImage.fillAmount = (xp / 100);




        if (xp >= max_XP)
        {
            Player_level.level++;
            print(Player_level.level);
            xp = xp - 100;
        }
    }





}
