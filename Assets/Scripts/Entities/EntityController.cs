using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public EntityData playerData;
    public EntityData enemyData;

    private void Start()
    {
        playerData.Init();
        enemyData.Init();
        playerData._target = enemyData;

        StartCoroutine(playerData.EntityLoop());
    }

    private void Update()
    {

        if (playerData.ActionReady)
        {
            Debug.Log("Ready to attack");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Player did an action");
                playerData.Attack();
            }
        }
    }

}
