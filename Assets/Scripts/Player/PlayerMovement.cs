﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float height;
    float distanceToEnemy;

    public Vector3 moveTo;
    Quaternion targetRotation;
    Rigidbody rb;

    public GameObject enemy;
    public bool combat;

    bool informationWindowActive;
     bool cutScene;


    // Start is called before the first frame update
    void Start()
    {
        //Habour = -35, height, -25
        //Forest1 = 16, height, 26
        //ForestSpider = 27, height, 77
        //Farm = 17, height, 107
        //after farm = 47, height, 221
        //Castlegate = 83, height, 231

        //Scene 3 start = 0, 15, 5
        //Cemetary = 10.8f, height ,158.9f
       //JungleTemple start = 96, 15, 128
    


        GetComponent<Animator>().Play("Idle");
        transform.position = new Vector3(-35, height, -25);
        moveTo = Vector3.MoveTowards(transform.position, new Vector3(-35, height, -25), speed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {

        //Moves and rotates player when moving freely
        movement();
        transform.position = Vector3.MoveTowards(transform.position, moveTo, speed * Time.deltaTime);
        transform.LookAt(moveTo, Vector3.up);

        //Stops movement if window || cutscene
        informationWindowActive = GameObject.Find("Controllers").transform.Find("QuestController").GetComponent<QuestHandler>().informationWindowActive;
        cutScene = Camera.main.GetComponent<CameraBehaviour>().cutScene;

        //If player is within attack reach of enemy, stop player and set combat = true (used in combat script)
        if (enemy != null)
        {
            distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (distanceToEnemy <= 2)
            {
                moveTo = new Vector3(transform.position.x, height, transform.position.z);
                transform.LookAt(enemy.transform.position, Vector3.up);
                combat = true;
            }
        }
    }

    void movement()
    {
        if (!informationWindowActive)
        {
            if (!cutScene)
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    if (Input.GetMouseButtonDown(0) && hit.collider.tag == "Walkable")
                    {

                        //Sets speed to 5, changes animaton to run, changes player target position to hit.point
                        speed = 5;
                        GetComponent<Animator>().Play("Run");
                        moveTo = new Vector3(hit.point.x, height, hit.point.z);
                        if (enemy != null)
                        {
                            enemy = null;
                        }
                    }
                    if (Input.GetMouseButtonDown(0) && hit.collider.tag == "Enemy")
                    {
                        enemy = hit.collider.gameObject;
                        distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                        if (distanceToEnemy >= 2)
                        {
                            //While running to enemy; Sets speed to 5, changes animaton to run, changes player target position to hit.point
                            speed = 5;
                            GetComponent<Animator>().Play("Run");
                            moveTo = new Vector3(hit.point.x, height, hit.point.z);
                        }

                    }
                    //if no enemy, no combat (Used to deactivate combat script)
                    if (enemy == null)
                    {
                        combat = false;
                    }
                }

                //If moveTo has been reached, change attackString to Idle
                if (Vector3.Distance(transform.position, moveTo) < 0.001f && !combat)
                {
                    GetComponent<Animator>().Play("Idle");
                }
            }
        }


    }

    //If player is stuck against wall, stop player
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Wall")
        {
            speed = 0;
            GetComponent<Animator>().Play("Idle");
        }
    }



}
