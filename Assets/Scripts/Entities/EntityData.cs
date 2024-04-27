using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EntityData
{
    public string characterName = "CharacterName";
    [Space(10)]

    public int maxHealth = 100;
    public int curHealth = 1000;

    [Space(10)]

    public int maxEnergy = 100;
    public int energyCurr = 100;

    [Space(10)]

    public float maxOCPoints = 100;
    public float currentOCPoints = 100;

    [Space(10)]

    public float speedLimit = 10;
    public float curSpeed = 10;

    public EntityState entityState;
    public EntityGroup entityGroup;


    [Space(10)]

    public UnityEvent OnAttack;
    public UnityEvent onWasAttacked;

    public bool playerJustAttacked;


    [Space(15)]

    public EntityData _target;

    public void Init()
    {
        OnAttack.AddListener(EntityAttackedDefault);


    }

    public void Attack()
    {
        OnAttack.Invoke();

        //TEMP ATTACK
        _target.Damage(10);
        Debug.Log("Target was shot");
    }

    public void Damage(int damageAmount)
    {
        curHealth -= damageAmount;
    }


    public bool CanAttackTarget
    {
        get
        {
            return _target.entityState == EntityState.Idle || _target.entityState == EntityState.Ready;
        }
    }

    public bool ActionReady
    {
        get
        {
            return curSpeed >= speedLimit;
        }
        
    }

    void  EntityAttackDefault()
    {
        playerJustAttacked = false;
        curSpeed = 0;

    }

    void EntityAttackedDefault()
    {
        Debug.Log(characterName + "was attacked");
    }
    public IEnumerator EntityBehavior()
    {
        // Character Target

        yield return new WaitUntil(()=> CanAttackTarget);

        // We need to wait until the character(enemy or player) has done an action
        yield return new WaitUntil(()=> playerJustAttacked);

        OnAttack.Invoke(); 

    }

    public IEnumerator EntityLoop()
    {
        while(entityState != EntityState.Died)
        {
            if (curSpeed >= speedLimit)
            {
                curSpeed = speedLimit;
            }
            else
            {
                //increase currentspeed
                curSpeed += Time.deltaTime;
            }
            
            yield return null;
        }  
    }

}

public enum EntityGroup
{
    Friendly,
    Enemy
}

public enum EntityState
{
    Loading,
    Idle,
    Ready,
    Attacked,
    Attacking,
    Died
}

