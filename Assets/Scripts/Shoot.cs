using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Animator anim;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    Vector2 Direction;

    public float fireRate = 0.2f;
    public float weaponRange = 50f;
    public int damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetFloat("Horizontal") > 0)
            Direction = Vector2.right;

        if (anim.GetFloat("Horizontal") < 0)
            Direction = Vector2.left;

        if (anim.GetFloat("Vertical") > 0)
            Direction = Vector2.up;

        if (anim.GetFloat("Vertical") < 0)
            Direction = Vector2.down;

        if (Input.GetButtonDown("Fire1"))
        {
            GameObject BulletIns = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            BulletIns.GetComponent<Rigidbody2D>().AddForce(Direction * bulletSpeed, ForceMode2D.Force);

        }
    }
}
