using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Animator animator;
    public Rigidbody2D rb;

    public GameObject horGun;
    public GameObject vertGun;

    Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    void FixedUpdate()
    {
       
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        if (movement.x > 0)
        {
            gameObject.transform.localScale = new Vector2(-1, 1);
        }
        if (movement.x < 0)
        {
            gameObject.transform.localScale = new Vector2(1, 1);
        }

        if (animator.GetFloat("Horizontal") != 0)
        {
            vertGun.SetActive(false);
            horGun.SetActive(true);
        }

        if (animator.GetFloat("Vertical") != 0)
        {
            horGun.SetActive(false);
            vertGun.SetActive(true);
        }
    }
}
