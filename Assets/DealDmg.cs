using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDmg : MonoBehaviour
{
    public int damage;
    public LayerMask targetMask;
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == targetMask)
        {
            //player.health -= damage;
        }
            
    }
}
