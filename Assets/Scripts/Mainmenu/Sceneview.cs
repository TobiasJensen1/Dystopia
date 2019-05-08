using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sceneview : MonoBehaviour
{

    public float timer;

    bool scene1;
    bool scene2;
    bool scene3;


    public GameObject one;
    public GameObject two;
    public GameObject three;
    public GameObject four;
    public GameObject five;

    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(12.3f,72.3f,-2.4f);
        transform.position = new Vector3(5.8f,31.3f,41.92f);
        StartCoroutine("ScenePlayWait");
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (scene1)
        {
            
            transform.position = Vector3.MoveTowards(transform.position, two.transform.position, 5 * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, two.transform.position) < 0.05f)
        {
            scene1 = false;
            scene2 = true;
            transform.position = new Vector3(three.transform.position.x, three.transform.position.y, three.transform.position.z);
            transform.eulerAngles = new Vector3(-0.6f, -180f, 0);
        }
        if (scene2)
        {
            transform.Rotate(transform.eulerAngles.x * 0.6f * Time.deltaTime, 0, 0);
            if(transform.eulerAngles.x >= 50f){
                transform.position = new Vector3(four.transform.position.x, four.transform.position.y, four.transform.position.z);
                transform.eulerAngles = new Vector3(0, 270.4f, 0);

                scene1 = false;
                scene2 = false;
                scene3 = true;
            }
        }
        if (scene3)
        {
            transform.position = Vector3.MoveTowards(transform.position, five.transform.position, 5 * Time.deltaTime);
        }
        if (Vector3.Distance(transform.position, five.transform.position) < 0.05f)
        {
            transform.eulerAngles = new Vector3(12.3f, 72.3f, -2.4f);
            transform.position = new Vector3(5.8f, 31.3f, 41.92f);
            StartCoroutine("ScenePlayWait");
            scene3 = false;
        }
    }


    IEnumerator ScenePlayWait()
    {
        yield return new WaitForSeconds(timer);
        scene1 = true;
        StopCoroutine("ScenePlayWait");
    }

    
}
