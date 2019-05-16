using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorCollision : MonoBehaviour
{
    public bool meteorCheck;
    public float dmg;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Wall")
        {
            transform.gameObject.SetActive(false);
        }
        if(collision.collider.tag == "Player")
        {
            transform.gameObject.SetActive(false);
            collision.gameObject.GetComponent<PlayerStats>().health -= dmg;
            meteorCheck = true;
        }
    }
}
