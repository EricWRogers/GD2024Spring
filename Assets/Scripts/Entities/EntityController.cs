using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public EntityData playerData;
    public EntityData enemyData;

    private void Start()
    {
        playerData._target = enemyData;
    }

    private void Update()
    {


        //Let's increase the player speed

        if (playerData.curSpeed >= playerData.speedLimit)
        {
            playerData.curSpeed = playerData.speedLimit;
        }
        else
        {
            playerData.curSpeed += Time.deltaTime;
        }

        if (playerData.ActionReady)
        {
            Debug.Log("Ready to attack");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Player did an action");
                playerData.curSpeed = 0;
            }
        }
    }

    IEnumerator EntityBehavior()
    {
        // Character Target

        yield return null;
    }

}
