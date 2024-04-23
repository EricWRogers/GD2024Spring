using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float moveSpeed;

    bool isMoving;
    Vector2 input;

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                var targetpos = transform.position;
                targetpos.x += input.x;
                targetpos.y += input.y;

                StartCoroutine(Move(targetpos));
            }
            
        }
        IEnumerator Move(Vector3 targetpos)
        {
            isMoving = true;
            while ((targetpos - transform.position).sqrMagnitude > Mathf.Epsilon)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetpos, moveSpeed * Time.deltaTime);
                yield return null;
            }
            transform.position = targetpos;
            isMoving = false;
        }
        
            
    }
}
