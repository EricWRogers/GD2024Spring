using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDmg : MonoBehaviour
{
    public int damage;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D (Collider2D other)
    {
        Debug.Log("Hit");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit" + other.gameObject);
            player.GetComponent<Health>().playerHealth -= damage;
        }
            
    }
}
