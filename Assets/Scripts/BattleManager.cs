using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    public EntityController currentCharacter;

    public List<EntityController> friendlyCharacters = new List<EntityController>();

    List<EntityController> enemyCharacters = new List<EntityController>();

    public AudioSource victorySound;
    public static BattleManager Instance;

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        victorySound = GetComponent<AudioSource>();
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

    public EntityController RandomFriendlyCharacter
    {
        get
        {
            return friendlyCharacters[Random.Range(0, friendlyCharacters.Count)];
        }
    }

    public void CheckMatchStatus()
    {
        if (FriendlyCharacterAlive && !EnemyCharacterAlive)
        {
            Debug.Log("Victory!");
            victorySound.Play();
        }

    }

    bool FriendlyCharacterAlive
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
                if (friendlyCharacters[i].entityData.IsAlive)
                {
                    enemyAlive = true;
                }
            }

            return enemyAlive;
        }
    }
}
