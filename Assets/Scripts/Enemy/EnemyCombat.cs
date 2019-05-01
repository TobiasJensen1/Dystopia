using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombat : MonoBehaviour
{
    GameObject enemyHealthbarObject;
    public Text enemyName;

    GameObject enemy;


    // Start is called before the first frame update
    void Start()
    {
        enemyHealthbarObject = GameObject.Find("PlayerGui").transform.Find("EnemyHealthbar").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        enemy = GameObject.Find("Player").GetComponent<PlayerMovement>().enemy;

        guiEnemyHealthAndName();
    }



    //Used to turn Gui Enemy Healthbar on/off and set gui enemy name if player is in combat with enemy
    void guiEnemyHealthAndName()
    {
        if (GameObject.Find("Player").GetComponent<PlayerMovement>().combat)
        {
            if (enemy != null)
            {
                enemyHealthbarObject.SetActive(true);
                enemyName.text = enemy.GetComponent<EnemyStats>().EnemyName;
                enemyName.enabled = true;
            }
        }
        else
        {
            enemyHealthbarObject.SetActive(false);
            enemyName.enabled = false;
        }
    }
}



