using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using System;





public class BattleManager : MonoBehaviour
{
    public EntityController currentCharacter;

    public List<EntityController> friendlyCharacters = new List<EntityController>();

    List<EntityController> enemyCharacters = new List<EntityController>();

    
    public static BattleManager Instance;

    public GameController gameController;
    public Player_Controller playerController;

    public event Action onBattleOver;

    

    void Awake()
    {
        Instance = this;
    }

    public void StartBattle()
    {
        
        friendlyCharacters = FindObjectsOfType<EntityController>().ToList().FindAll(x => x.entityData.entityGroup == EntityGroup.Friendly);
        enemyCharacters = FindObjectsOfType<EntityController>().ToList().FindAll(x => x.entityData.entityGroup == EntityGroup.Enemy);
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
                if (currentCharacter.entityData._target.IsAttackable)
                {
                    if(currentCharacter.attackQueue == null)
                    {
                        currentCharacter.attackQueue = StartCoroutine(currentCharacter.entityData.QueueAttack(currentCharacter.entityData.basicAttack));
                    }
                   
                }

            }
        }
    }

   

    public EntityController RandomFriendlyCharacter
    {
        get
        {
            return friendlyCharacters[UnityEngine.Random.Range(0, friendlyCharacters.Count)];
        }
    }

    public void CheckMatchStatus()
    {
        if (FriendlyCharacterAlive && !EnemyCharacterAlive)
        {
            Debug.Log("Victory!");
            onBattleOver();
            StopAllCharacters();
            

            
        }

        else if (!FriendlyCharacterAlive && EnemyCharacterAlive)
        {
            Debug.Log("Womp Womp");
            StopAllCharacters();
            onBattleOver();
            
            
        }


    }

    void StopAllCharacters()
    {

        for (int i = 0; i < friendlyCharacters.Count; i++)
        {
            friendlyCharacters[i].StopAll();
        }

        for (int i = 0; i < enemyCharacters.Count; i++)
        {
            enemyCharacters[i].StopAll();
        } 
    }

    public bool FriendlyCharacterAlive
    {
        get
        {
            bool friendlyAlive = false;

            for (int i = 0; i < friendlyCharacters.Count; i++)
            {
                if (friendlyCharacters[i].entityData.IsAlive)
                {
                    friendlyAlive = true;
                }
            }

            return friendlyAlive;
        }
    }

    

    bool EnemyCharacterAlive
    {
        get
        {
            bool enemyAlive = false;

            for (int i = 0; i < enemyCharacters.Count; i++)
            {
                if (enemyCharacters[i].entityData.IsAlive)
                {
                    enemyAlive = true;
                }
            }

            return enemyAlive;
        }
    }
}
