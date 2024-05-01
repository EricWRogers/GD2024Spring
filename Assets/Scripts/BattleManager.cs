using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    public EntityController currentCharacter;

    public List<EntityController> friendlyCharacters = new List<EntityController>();
    public static BattleManager Instance;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        friendlyCharacters = FindObjectsOfType<EntityController>().ToList().FindAll(x => x.entityData.entityGroup == EntityGroup.Friendly);
    }

    public void SelectCharacter(EntityData newChar)
    {
        newChar.SelectCharacter();
    }

    public void DoBasicAttackOnTarget()
    {
        var charData = currentCharacter.entityData;
        if (currentCharacter.entityData.ActionReady)
        {
            Debug.Log("Ready to attack");
            if (currentCharacter.entityData.entityGroup == EntityGroup.Friendly)
            {
                Debug.Log("Player did an action");
                if (currentCharacter.entityData._target.CanBeAttacked)
                {
                    if(currentCharacter.attackQueue == null)
                    {
                        currentCharacter.attackQueue = StartCoroutine(currentCharacter.entityData.QueueAttack(currentCharacter.entityData.basicAttack));
                    }
                   
                }

            }
        }
    }
}
