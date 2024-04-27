using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public float moveSpeed;
    float SprintWhyNot = 1f;
    public float collisionRadius;

    bool isMoving;
    Vector2 input;

    public LayerMask SolidObjectsLayer1;
    public LayerMask SolidObjectsLayer2;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) { SprintWhyNot = 2; }
        if (Input.GetKeyUp(KeyCode.LeftShift)) { SprintWhyNot = 1; }

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
                
                if (!IsAWall(targetpos))
                {
                    StartCoroutine(Move(targetpos));
                }
                
            }
            
        }
        IEnumerator Move(Vector3 targetpos)
        {
            isMoving = true;
            while ((targetpos - transform.position).sqrMagnitude > Mathf.Epsilon)
            {       
                transform.position = Vector3.MoveTowards(transform.position, targetpos, moveSpeed * SprintWhyNot * Time.deltaTime);
                yield return null;
            }
            transform.position = targetpos;
            isMoving = false;
        }
        bool IsAWall(Vector2 targetpos)
        {
            if (Physics2D.OverlapCircle(targetpos, collisionRadius, SolidObjectsLayer1) != null | Physics2D.OverlapCircle(targetpos, collisionRadius, SolidObjectsLayer2) != null) 
            {
                return true;
            }
            return false;
        }
        

    }
}
