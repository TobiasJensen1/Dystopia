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

    bool phase1;
    bool phase2;
    bool phase3;
    bool phase4;
    bool center;


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
        corruptedKingFight();
    }


    void corruptedKingFight()
    {
        if (transform.GetComponent<EnemyStats>().health >= 75)
        {
            phase1 = true;
            //also used for npc vs enemy combat
            if (destination != null && destination.gameObject.activeSelf)
            {
                distanceToPlayer = Vector3.Distance(transform.position, targetVector);
                targetVector = destination.transform.position;
                //Only move if player is within aggroRange
                if (GetComponent<EnemyStats>().aggroRange > distanceToPlayer)
                {
                    //Move towards player when too far away
                    if (distanceToPlayer >= 2 && !isAttackActive)
                    {
                        anim.Play("Run");
                        transform.LookAt(destination);
                        nma.SetDestination(targetVector);
                        //Start attacking if close to player
                    }
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
            else
            {
                GetComponent<Animator>().Play("Idle");
            }
        }
        else
        {
            StopCoroutine("meleeAttack");
            phase1 = true;
            if (phase1 && !phase2)
            {
                print("hej");
                StartCoroutine("phase2Fight");
            }
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
            center = true;
            anim.Play("Phase2");
            yield return new WaitForSeconds(3f);
            anim.Play("Idle");
            Camera.main.GetComponent<CameraBehaviour>().player = player;
            phase2 = true;
            StopCoroutine("phase2Fight");
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
            //if friendly Npc
            if (transform.GetComponent<EnemyStats>().EnemyName == "Troll Assassin" || transform.GetComponent<EnemyStats>().EnemyName == "Elf Mage")
            {
                destination.GetComponent<EnemyStats>().health = destination.GetComponent<EnemyStats>().currentHealth -= transform.GetComponent<EnemyStats>().damage;
                destination.GetComponent<EnemyStats>().health = destination.GetComponent<EnemyStats>().currentHealth;
            }
            else
            {
                destination.GetComponent<PlayerStats>().health -= transform.GetComponent<EnemyStats>().damage;
            }
            StopCoroutine("meleeAttack");
        }


    }
}
