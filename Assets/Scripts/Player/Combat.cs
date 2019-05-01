using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat : MonoBehaviour
{

    public GameObject player;
    float distanceToEnemy;
    Vector3 moveTo;

    bool combatCheck;
    bool idleAttack;
    bool att1;

    float enemyMaxhealth;
    float enemyHealth;
    float currentHealth;
    float playerDamage;
    float damageGiven;

    string attackString;
    float time;
    float damageMultiplier;

    //Used to check if ability is unlocked
    bool q;
    bool w;
    bool e;
    bool r;

    bool aoe;
    List<GameObject> enemies;

    GameObject quest;

    //Used for cooldown
    public List<Skill> skills;
    public List<Sprite> sprites;

    public float q1FuryCost;
    public float w1FuryCost;
    public float e1FuryCost;
    public float r1FuryCost;


    // Start is called before the first frame update
    void Start()
    {
        quest = GameObject.Find("Controllers").transform.Find("QuestController").gameObject;

        skills = GameObject.Find("Canvas").transform.Find("AbilityBar").GetComponent<SkillCoolDown>().skills;
        playerDamage = player.GetComponent<PlayerStats>().damage;

    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerMovement>().enemy != null)
        {
            enemies = EnemyStats.enemies;
        }

        if (player.GetComponent<PlayerMovement>().enemy != null)
        {
            enemyHealth = player.GetComponent<PlayerMovement>().enemy.GetComponent<EnemyStats>().health;
            enemyMaxhealth = player.GetComponent<PlayerMovement>().enemy.GetComponent<EnemyStats>().maxHealth;
            currentHealth = enemyHealth = player.GetComponent<PlayerMovement>().enemy.GetComponent<EnemyStats>().currentHealth;
        }
        //Bool that is set, given players position relative to enemy
        combatCheck = player.GetComponent<PlayerMovement>().combat;

        combat();
        checkUnlockedAbilities();
        checkInactiveAbility();
    }



    void combat()
    {

        //Initiate Combat
        if (combatCheck && !idleAttack)
        {
            idleAttack = true;
            if (idleAttack)
            {
                StartCoroutine("IdleAttack");
            }
        }

        //Stop Combat
        if (!combatCheck)
        {
            StopCoroutine("IdleAttack");
            idleAttack = false;
        }

        //Abilities

        //Chose attack to use, checks if skill is on cooldown, idle if none chosen
        if (combatCheck)
        {

            if (Input.GetKey("q") && q)
            {
                if (skills[0].currentCoolDown >= skills[0].coolDown && player.GetComponent<PlayerStats>().fury >= q1FuryCost)
                {
                    aoe = false;
                    attackString = "Attack1";
                    time = 1.6f;
                    damageMultiplier = 1.2f;
                    skills[0].currentCoolDown = 0;
                    player.GetComponent<PlayerStats>().fury -= q1FuryCost;


                }
            }
            if (Input.GetKey("w") && w)
            {
                if (skills[1].currentCoolDown >= skills[1].coolDown && player.GetComponent<PlayerStats>().fury >= w1FuryCost)
                {
                    aoe = true;
                    attackString = "Attack2";
                    time = 2.5f;
                    damageMultiplier = 1.1f;
                    skills[1].currentCoolDown = 0;
                    player.GetComponent<PlayerStats>().fury -= w1FuryCost;
                }
            }
            if (Input.GetKey("e") && e)
            {
                if (skills[2].currentCoolDown >= skills[2].coolDown && player.GetComponent<PlayerStats>().fury >= e1FuryCost)
                {
                    aoe = false;
                    attackString = "Taunt";
                    time = 1.6f;
                    damageMultiplier = 0;
                    skills[2].currentCoolDown = 0;
                    player.GetComponent<PlayerStats>().fury -= e1FuryCost;
                }
            }
            if (Input.GetKey("r") && r)
            {
                if (skills[3].currentCoolDown >= skills[3].coolDown && player.GetComponent<PlayerStats>().fury >= r1FuryCost)
                {
                    aoe = false;
                    attackString = "Ultimate";
                    time = 4f;
                    damageMultiplier = player.GetComponent<PlayerStats>().fury / 10;
                    skills[3].currentCoolDown = 0;
                    player.GetComponent<PlayerStats>().fury = 0;
                }
            }
        }
    }
    IEnumerator IdleAttack()
    {
        while (true)
        {

            if (idleAttack)
            {
                //Plays attack if not null
                if (attackString != null)
                {
                    player.GetComponent<Animator>().Play(attackString);
                    attackString = null;
                    yield return new WaitForSeconds(time - 0.7f);
                    damageEnemy(damageMultiplier);
                    yield return new WaitForSeconds(0.7f);

                }
                //Plays idleattack if no attack chosen
                else
                {
                    aoe = false;
                    skills[4].currentCoolDown = 0;
                    player.GetComponent<Animator>().Play("IdleAttack");
                    //Converts damage to % damage
                    yield return new WaitForSeconds(.9f);
                    damageEnemy(1);
                    //Check fury levels and add after basic att
                    //Make into singleton
                    if (player.GetComponent<PlayerStats>().fury < player.GetComponent<PlayerStats>().maxFury - 15)
                    {
                        player.GetComponent<PlayerStats>().fury += 15;
                    }
                    else if (player.GetComponent<PlayerStats>().fury < player.GetComponent<PlayerStats>().maxFury)
                    {
                        player.GetComponent<PlayerStats>().fury = player.GetComponent<PlayerStats>().maxFury;
                    }
                    yield return new WaitForSeconds(.7f);
                }

            }
        }
    }

    void damageEnemy(float damageMultiplier)
    {
        if (!aoe)
        {
            damageGiven = (enemyMaxhealth * playerDamage) / 100;
            enemyHealth = player.GetComponent<PlayerMovement>().enemy.GetComponent<EnemyStats>().currentHealth -= playerDamage * damageMultiplier;
            player.GetComponent<PlayerMovement>().enemy.GetComponent<EnemyStats>().health = (100 / enemyMaxhealth) * player.GetComponent<PlayerMovement>().enemy.GetComponent<EnemyStats>().currentHealth;
        }
        else
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] != null)
                {
                    //Make sure only enemies close enough are hit by Aoe dmg
                    float distance = Vector3.Distance(enemies[i].transform.position, player.transform.position);
                    if (distance <= 2)
                    {
                        damageGiven = (enemies[i].GetComponent<EnemyStats>().maxHealth * playerDamage) / 100;
                        enemies[i].GetComponent<EnemyStats>().health = enemies[i].GetComponent<EnemyStats>().currentHealth -= playerDamage * damageMultiplier;
                        enemies[i].GetComponent<EnemyStats>().health = (100 / enemies[i].GetComponent<EnemyStats>().maxHealth) * enemies[i].GetComponent<EnemyStats>().currentHealth;
                    }
                }
            }
        }
    }



    void checkUnlockedAbilities()
    {
        q = quest.GetComponent<QuestHandler>().q;
        w = quest.GetComponent<QuestHandler>().w;
        e = quest.GetComponent<QuestHandler>().e;
        r = quest.GetComponent<QuestHandler>().r;
    }

    //Checks which abilities to make active/inactive based on players fury amount
    void checkInactiveAbility()
    {
        //if fury is above 50 all abilities are active
        if (player.GetComponent<PlayerStats>().fury > r1FuryCost)
        {
            //Q1 Active
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("QAbility").transform.GetComponent<Image>().sprite = sprites[0];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("QAbility").transform.Find("QAbility (1)").transform.GetComponent<Image>().sprite = sprites[0];
            //W1 Active
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("WAbility").transform.GetComponent<Image>().sprite = sprites[2];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("WAbility").transform.Find("WAbility (1)").transform.GetComponent<Image>().sprite = sprites[2];
            //E1 Active
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("EAbility").transform.GetComponent<Image>().sprite = sprites[4];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("EAbility").transform.Find("EAbility (1)").transform.GetComponent<Image>().sprite = sprites[4];
            //R1 Active
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("RAbility").transform.GetComponent<Image>().sprite = sprites[6];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("RAbility").transform.Find("RAbility (1)").transform.GetComponent<Image>().sprite = sprites[6];
        }
        //If fury is below q1FuryCost all abilities inactive
        else if (player.GetComponent<PlayerStats>().fury < q1FuryCost)
        {
            //Q1 Inactive
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("QAbility").transform.GetComponent<Image>().sprite = sprites[1];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("QAbility").transform.Find("QAbility (1)").transform.GetComponent<Image>().sprite = sprites[1];
            //W1 Inactive
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("WAbility").transform.GetComponent<Image>().sprite = sprites[3];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("WAbility").transform.Find("WAbility (1)").transform.GetComponent<Image>().sprite = sprites[3];
            //E1 Inactive
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("EAbility").transform.GetComponent<Image>().sprite = sprites[5];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("EAbility").transform.Find("EAbility (1)").transform.GetComponent<Image>().sprite = sprites[5];
            //R1 Inactive
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("RAbility").transform.GetComponent<Image>().sprite = sprites[7];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("RAbility").transform.Find("RAbility (1)").transform.GetComponent<Image>().sprite = sprites[7];
        }
        //If fury is below w1FuryCost all abilities but Q1 inactive
        else if (player.GetComponent<PlayerStats>().fury < w1FuryCost)
        {
            //Q1 Active
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("QAbility").transform.GetComponent<Image>().sprite = sprites[0];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("QAbility").transform.Find("QAbility (1)").transform.GetComponent<Image>().sprite = sprites[0];
            //W1 Inactive
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("WAbility").transform.GetComponent<Image>().sprite = sprites[3];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("WAbility").transform.Find("WAbility (1)").transform.GetComponent<Image>().sprite = sprites[3];
            //E1 Inactive
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("EAbility").transform.GetComponent<Image>().sprite = sprites[5];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("EAbility").transform.Find("EAbility (1)").transform.GetComponent<Image>().sprite = sprites[5];
            //R1 Inactive
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("RAbility").transform.GetComponent<Image>().sprite = sprites[7];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("RAbility").transform.Find("RAbility (1)").transform.GetComponent<Image>().sprite = sprites[7];
        }
        //If fury is below e1FuryCost all abilities but Q1+W1 inactive
        else if (player.GetComponent<PlayerStats>().fury < e1FuryCost)
        {
            //Q1 Active
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("QAbility").transform.GetComponent<Image>().sprite = sprites[0];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("QAbility").transform.Find("QAbility (1)").transform.GetComponent<Image>().sprite = sprites[0];
            //W1 Active
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("WAbility").transform.GetComponent<Image>().sprite = sprites[2];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("WAbility").transform.Find("WAbility (1)").transform.GetComponent<Image>().sprite = sprites[2];
            //E1 Inactive
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("EAbility").transform.GetComponent<Image>().sprite = sprites[5];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("EAbility").transform.Find("EAbility (1)").transform.GetComponent<Image>().sprite = sprites[5];
            //R1 Inactive
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("RAbility").transform.GetComponent<Image>().sprite = sprites[7];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("RAbility").transform.Find("RAbility (1)").transform.GetComponent<Image>().sprite = sprites[7];
        }
        //if fury is blow r1FuryCost all abilities but R1 active
        else if (player.GetComponent<PlayerStats>().fury < r1FuryCost)
        {
            //Q1 Active
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("QAbility").transform.GetComponent<Image>().sprite = sprites[0];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("QAbility").transform.Find("QAbility (1)").transform.GetComponent<Image>().sprite = sprites[0];
            //W1 Active
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("WAbility").transform.GetComponent<Image>().sprite = sprites[2];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("WAbility").transform.Find("WAbility (1)").transform.GetComponent<Image>().sprite = sprites[2];
            //E1 Active
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("EAbility").transform.GetComponent<Image>().sprite = sprites[4];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("EAbility").transform.Find("EAbility (1)").transform.GetComponent<Image>().sprite = sprites[4];
            //R1 Inactive
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("RAbility").transform.GetComponent<Image>().sprite = sprites[7];
            GameObject.Find("Canvas").transform.Find("AbilityBar").transform.Find("RAbility").transform.Find("RAbility (1)").transform.GetComponent<Image>().sprite = sprites[7];
        }
    }
}


