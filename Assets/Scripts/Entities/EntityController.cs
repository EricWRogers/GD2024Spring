using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntityController : MonoBehaviour
{
    public EntityData entityData;
    public EntityController targetData;

    public Coroutine attackQueue = null;

    public Coroutine enemyAttackBehaviour;

    public Sprite sprite;

    private void Awake()
    {
        entityData._charCont = this;
    }
    private void Start()
    {
        entityData.Init();

        if(entityData.entityGroup == EntityGroup.Friendly)
        {
            entityData._target = targetData.entityData;
        }


        StartCoroutine(entityData.EntityLoop());
        enemyAttackBehaviour = StartCoroutine(AttackRandomFriendlyCharacter());
    }

    public void ClearAttackQueue()
    {
        if (attackQueue != null)
        {
            StopCoroutine((attackQueue));
            attackQueue = null;
        }
    
    }

   

    public IEnumerator AttackRandomFriendlyCharacter()
    {
        if (entityData.entityGroup == EntityGroup.Enemy)
        {
            while (entityData.IsAlive)
            {
                yield return new WaitUntil(()=> entityData.ActionReady);

                entityData._target = BattleManager.Instance.RandomFriendlyCharacter.entityData;

                yield return entityData.QueueAttack(entityData.basicAttack);
                
                
                yield return null;

            }
        }
    }

}
