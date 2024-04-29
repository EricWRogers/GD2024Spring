using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public EntityController currentCharacter;

    public static BattleManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void DoBasicAttackOnTarget()
    {
        if (currentCharacter.entityData.ActionReady)
        {
            Debug.Log("Ready to attack");
            if (currentCharacter.entityData.entityGroup == EntityGroup.Friendly && Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Player did an action");
                if (currentCharacter.entityData._target.CanBeAttacked)
                {
                   currentCharacter.entityData.Attack(currentCharacter.entityData.basicAttack);
                }

            }
        }
    }
}
