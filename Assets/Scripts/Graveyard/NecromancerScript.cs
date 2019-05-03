using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancerScript : MonoBehaviour
{
    //if all skeleton is active
    bool allactive;

    //Rolls already rolled
    public List<int> randomrolled;

    //spawn location
    public List<GameObject> spawnlocation;

    Animator anim;
    GameObject player;
    //aggro checking
    public float aggro;
    public bool isaggro;
    float distance;



    //ínstantiating gameobjects
    public List<GameObject> summons;
    public GameObject skeleton;



    
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        anim = GetComponent<Animator>();

        // Peformence friendly object pooling 5 skeleton
        for(int i = 0; i < 5;i++)
        {
           GameObject obj =(GameObject)Instantiate(skeleton);
            obj.SetActive(false);
            summons.Add(obj);  
        }

    }


    

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform.position);
        distance = Vector3.Distance(player.transform.position, transform.position);
        
        if(distance <= aggro && !isaggro)
        {
            isaggro = true;
            StartCoroutine("Necromancer");
            
        }else if(distance >= aggro)
        {
            StopCoroutine("Necromancer");
            isaggro = false;
        }


    }





    IEnumerator Necromancer()
    {
  
        while (true)
        {
            
           int random = Random.Range(0, 5);
           int randomloc = Random.Range(0, 3);
            


            //cheks if all in the list is active
            for(int i = 0; i < summons.Count; i++)
            {
                if (!summons[i].activeInHierarchy)
                {
                    allactive = false;
                    break;
                }
                else
                {
                    allactive = true;
                }
                
            }


            //summons only if all/some skeleton is inactive else go idle if all is active
            if (!allactive)
            {
                anim.Play("attack_short_001");

                //make sure, to not roll the same number
                while (randomrolled.Contains(random))
                {
                    random = Random.Range(0, 5);
                }


                //spawns skeleton on random chosen locations
                summons[random].SetActive(true);
                summons[random].transform.position = new Vector3(spawnlocation[randomloc].transform.position.x, 15, spawnlocation[randomloc].transform.position.z);
                
                randomrolled.Add(random);
            }
            else if(allactive)
            {
                anim.Play("idle_combat");
            }
           



            print(random);
            print(randomloc);
            

            yield return new WaitForSeconds(5f);
        }
    }
}
