using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntityController : MonoBehaviour
{
    public EntityData entityData;
    public EntityController targetData;

    private void Start()
    {
        entityData.Init();
        entityData._target = targetData.entityData;

        StartCoroutine(entityData.EntityLoop());
    }

    private void Update()
    {

        if (entityData.ActionReady)
        {
            Debug.Log("Ready to attack");
            if (entityData.entityGroup == EntityGroup.Friendly && Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Player did an action");
                if (entityData._target.CanBeAttacked)
                {
                    entityData.Attack();
                }

            }
        }
    }

}
