using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    int damage;
    void Start()
    {
       damage = GameObject.FindWithTag("Gun").GetComponent<Shoot>().damage;
    }

    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit");
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit" + other.gameObject);
            other.GetComponent<Health>().health -= damage;
        }

    }
}
