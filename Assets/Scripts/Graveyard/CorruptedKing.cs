using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CorruptedKing : MonoBehaviour
{
    [SerializeField]
    public Transform destination;
    public GameObject centerObj;

    NavMeshAgent nma;
    Vector3 targetVector;
    float distanceToPlayer;

    string animationString;
    Animator anim;
    bool attack;
    bool isAttackActive;

    //until 75% health
    bool phase1;
    //meteor
    bool phase2;
    //until 50% health
    bool phase3;
    //spawns
    bool phase4;
    //25% health
    bool phase5;
    bool center;
    bool meteorCheck;

    //Mechanic objects
    public GameObject ringOfFire;
    public GameObject meteor;
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject wof1;
    public GameObject wof2;
    public GameObject enemy1;
    public GameObject enemy2;


    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        anim = GetComponent<Animator>();
        nma = GetComponent<NavMeshAgent>();

        if (nma == null)
        {

            print("no navmeshagent");
        }

        if (destination == null)
        {
            destination = GameObject.Find("Player").transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (meteor != null)
        {
            if (meteor.activeSelf)
            {
                meteor.transform.position = Vector3.MoveTowards(meteor.transform.position, player.transform.position, 4f * Time.deltaTime);
            }
            meteorCheck = meteor.GetComponent<MeteorCollision>().meteorCheck;
        }
        //Fight up until meteor has hit
        corruptedKingFight1();
        corruptedKingFight2();
    }


    void corruptedKingFight1()
    {
        if (transform.GetComponent<EnemyStats>().health >= 75)
        {
            if (!phase1)
            {
                //also used for npc vs enemy combat
                if (destination != null && destination.gameObject.activeSelf)
                {
                    targetVector = destination.transform.position;
                    distanceToPlayer = Vector3.Distance(transform.position, targetVector);
                    //Move towards player when too far away
                    if (distanceToPlayer >= 2 && !isAttackActive)
                    {
                        anim.Play("Run");
                        transform.LookAt(destination);
                        nma.SetDestination(targetVector);
                    }
                    //Start attacking if close to player
                    else if (distanceToPlayer <= 2 && !isAttackActive)
                    {
                        isAttackActive = true;
                        if (isAttackActive)
                        {
                            StartCoroutine("meleeAttack");
                        }
                    }
                }
            }
        }
        if (!phase1 && transform.GetComponent<EnemyStats>().health <= 75)
        {
            phase1 = true;
        }
        if (phase1 && !phase2)
        {
            isAttackActive = false;
            StopCoroutine("meleeAttack");
            StartCoroutine("phase2Fight");
        }
    }


    void corruptedKingFight2()
    {
        if (transform.GetComponent<EnemyStats>().health >= 50)
        {
            if (phase3)
            {
                //also used for npc vs enemy combat
                if (destination != null && destination.gameObject.activeSelf)
                {
                    targetVector = destination.transform.position;
                    distanceToPlayer = Vector3.Distance(transform.position, targetVector);
                    //Move towards player when too far away
                    if (distanceToPlayer >= 2 && !isAttackActive)
                    {
                        anim.Play("Run");
                        transform.LookAt(destination);
                        nma.SetDestination(targetVector);
                    }
                    //Start attacking if close to player
                    else if (distanceToPlayer <= 2 && !isAttackActive)
                    {
                        isAttackActive = true;
                        if (isAttackActive)
                        {
                            StartCoroutine("meleeAttack");
                        }
                    }
                }
            }
        }
        if (!phase4 && transform.GetComponent<EnemyStats>().health <= 50)
        {
            phase4 = true;
        }
        if (phase4 && !phase5)
        {
            StopCoroutine("meleeAttack");
            StartCoroutine("phase4Fight");
        }
        
    }


    public IEnumerator phase2Fight()
    {

        if (!center)
        {
            //Set camera to boss, and run
            Camera.main.GetComponent<CameraBehaviour>().player = transform.gameObject;
            anim.Play("Run");
            //Teleport player back, set player animation and stop combat
            player.GetComponent<Animator>().Play("Idle");
            player.GetComponent<PlayerMovement>().enemy = null;
            player.GetComponent<PlayerMovement>().combat = false;
            player.transform.position = new Vector3(175, 15, 232);
            player.GetComponent<PlayerMovement>().moveTo = new Vector3(175, 15, 232);
            //Make boss walk to and look at the center
            nma.SetDestination(centerObj.transform.position);
            transform.LookAt(centerObj.transform.position);
        }
        //If boss is at the middle, start phase2 attack
        if (Vector3.Distance(transform.position, centerObj.transform.position) <= .7f)
        {
            transform.LookAt(player.transform.position);
            GetComponent<BoxCollider>().enabled = false;
            center = true;
            ringOfFire.SetActive(true);
            anim.Play("Phase2");
            yield return new WaitForSeconds(3f);
            meteor.SetActive(true);
            yield return new WaitForSeconds(1.5f);
            anim.Play("Idle");
            Camera.main.GetComponent<CameraBehaviour>().player = player;
            //If meteor event is done
            if (meteorCheck)
            {
                phase2 = true;
                StopCoroutine("phase2Fight");
                StartCoroutine("phase3Fight");
            }
        }

    }

    public IEnumerator phase3Fight()
    {
        //enables collider, disables ring of fire, sets center false for future phases, starts phase 3
        GetComponent<BoxCollider>().enabled = true;
        ringOfFire.SetActive(false);
        center = false;
        phase3 = true;
        yield return null;
    }

    public IEnumerator phase4Fight()
    {
        if (!center)
        {
            //Set camera to boss, and run
            Camera.main.GetComponent<CameraBehaviour>().player = transform.gameObject;
            anim.Play("Run");
            //Teleport player back, set player animation and stop combat
            player.GetComponent<Animator>().Play("Idle");
            player.GetComponent<PlayerMovement>().enemy = null;
            player.GetComponent<PlayerMovement>().combat = false;
            player.transform.position = new Vector3(175, 15, 232);
            player.GetComponent<PlayerMovement>().moveTo = new Vector3(175, 15, 232);
            //Make boss walk to and look at the center
            nma.SetDestination(centerObj.transform.position);
            transform.LookAt(centerObj.transform.position);
        }
        //If boss is at the middle, start phase2 attack
        if (Vector3.Distance(transform.position, centerObj.transform.position) <= .7f)
        {
            transform.LookAt(player.transform.position);
            GetComponent<BoxCollider>().enabled = false;
            center = true;
            //activates spawn effect, firewall and spawns
            anim.Play("Phase4");
            spawn1.SetActive(true);
            spawn2.SetActive(true);
            wof1.SetActive(true);
            wof2.SetActive(true);
            enemy1.SetActive(true);
            enemy2.SetActive(true);
            yield return new WaitForSeconds(3f);
            yield return null;
        }
    }


    public IEnumerator meleeAttack()
    {
        while (true)
        {
            anim.Play("Attack");
            nma.SetDestination(transform.position);
            transform.LookAt(destination);
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            isAttackActive = false;

            destination.GetComponent<PlayerStats>().health -= transform.GetComponent<EnemyStats>().damage;

            StopCoroutine("meleeAttack");
        }


    }
}
