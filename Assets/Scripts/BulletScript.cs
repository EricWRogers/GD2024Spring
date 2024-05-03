using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletScript : MonoBehaviour
{
    int damage;
    Rigidbody2D rb;
    private Vector3 previousPos;
    private void Awake()
    {
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        damage = GameObject.FindWithTag("Gun").GetComponent<Shoot>().damage;

        previousPos = transform.position;
    }

    void Update()
    {
        transform.up = -(rb.velocity.normalized);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit" + other.gameObject);
            other.GetComponent<Health>().health -= damage;
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "Walls")
        {
            Destroy(gameObject);
        }

    }
}
