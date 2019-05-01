using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public string EnemyName;

    public float maxHealth;
    public float currentHealth;
    public float health;

    public float damage;
    public float aggroRange;

    GameObject player;
    float distanceToPlayer;

    public EnemyHealthbar enemyHealthbars;

    public static List<GameObject> enemies = new List<GameObject>();

    GameObject enemyHealthbar;
    bool death;
    bool active;

    // Start is called before the first frame update
    void Start()
    {

        maxHealth = health;
        health = 100;
        currentHealth = maxHealth;
        enemyHealthbar = GameObject.Find("PlayerGui").transform.Find("EnemyHealthbar").gameObject;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        lookForPlayer();
        updateHealthbars();
        destroyWhenDead();
    }


    //Updates enemy healthbars
    public void updateHealthbars()
    {
        if (active)
        {
            enemyHealthbars.setHealth(currentHealth / maxHealth);
        }
        /*
        if (player.GetComponent<PlayerMovement>().enemy != null)
        {
            if (player.GetComponent<PlayerMovement>().enemy == transform.gameObject)
            {*/
                enemyHealthbar.GetComponent<MonoHealthbar>().Health = (int)health;
            //}
        //}
    }



    //Destroy and remove enemy from list when health <= 0, disables box collider and makes enemy null to stop combat and collision while death animation plays
    void destroyWhenDead()
    {
        if (health <= 0 && !death)
        {
            if (enemies.Contains(transform.gameObject))
            {
                enemies.Remove(transform.gameObject);
            }
            //Used for farmerevent
            if(EnemyName == "Troll Grunt")
            {
                GameObject.Find("Controllers").transform.Find("QuestController").GetComponent<QuestHandler>().currKills++;
            }
            death = true;
            active = false;
            health = 0;
            currentHealth = 0;
            transform.GetComponent<Animator>().Play("Death");
            transform.GetComponent<BoxCollider>().enabled = false;
            player.GetComponent<PlayerMovement>().enemy = null;
            StartCoroutine("deactivateEnemy");
        }
    }

    public IEnumerator deactivateEnemy()
    {
        yield return new WaitForSeconds(3f);
        transform.gameObject.SetActive(false);
        StopCoroutine("deactivateEnemy");
    }

    //Used to add and remove enemy to list of active enemies, when distance between player and enemy is within aggroRange
    void lookForPlayer()
    {
        if (transform.name != "TrollGeneral")
        {

            distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            if (distanceToPlayer <= aggroRange)
            {
                transform.Find("Healthbar").position = new Vector3(transform.position.x + 0.7f, transform.position.y + 2, transform.position.z + -0.9f);
                transform.Find("Healthbar").rotation = Quaternion.Euler(Camera.main.transform.position.x - transform.position.x - 20, 90, transform.Find("Healthbar").rotation.z);
                transform.Find("Healthbar").gameObject.SetActive(true);
                active = true;
                if (!enemies.Contains(transform.gameObject))
                {
                    enemies.Add(transform.gameObject);
                }
            }
            if (distanceToPlayer > aggroRange)
            {
                transform.gameObject.transform.Find("Healthbar").gameObject.SetActive(false);
                active = false;
                if (enemies.Contains(transform.gameObject))
                {
                    enemies.Remove(transform.gameObject);
                }
            }
        }

    }

}
