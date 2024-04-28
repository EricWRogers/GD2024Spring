using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class EntityData
{
    public string characterName = "CharacterName";
    [Space(10)]

    
    public UIData entityUI;

    [Space(10)]

    public int maxHealth = 100;
    public int curHealth = 100;

    [Space(10)]

    public int maxEnergy = 100;
    public int curEnergy = 100;

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
        if (entityGroup == EntityGroup.Friendly)
        {
           entityUI.Init(maxHealth, curHealth, maxEnergy, curEnergy, characterName); 
        }
        

        OnAttack.AddListener(EntityAttackDefault);
        onWasAttacked.AddListener(EntityAttackedDefault);

        entityState = EntityState.Idle;
    }

    public void Attack()
    {
        OnAttack.Invoke();

        //TEMP ATTACK
        _target.Damage(10);
    }

    public void Damage(int damageAmount)
    {
        if (curHealth <= 0)
        {
            curHealth = 0;
            entityState = EntityState.Died;
        }



        curHealth -= damageAmount;
        onWasAttacked.Invoke();
    }

    public bool CanBeAttacked
    {
        get
        {
            return entityState == EntityState.Idle || entityState == EntityState.Ready;
        }
        
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
        Debug.Log(characterName + " was attacked");
    }

    public IEnumerator EntityLoop()
    {
        while(entityState != EntityState.Died)
        {
            if (curSpeed >= speedLimit)
            {
                curSpeed = speedLimit;
                entityState = EntityState.Ready;
            }
            else
            {
                //increase currentspeed
                curSpeed += Time.deltaTime;
                entityState = EntityState.Idle;
            }
            
            yield return null;
        }  
    }

}

[System.Serializable]
public class UIData
{
    public DefUI physicUI;
    public void Init(int maxHealth, int curHealth, int maxEnergy, int curEnergy, string charName)
    {
        UIManager.Instance.SpawnRow();
        //Health Slider Setup
        physicUI.healthSlider.maxValue = maxHealth;
        physicUI.healthSlider.value = curHealth;
        physicUI.healthUI.text = curHealth.ToString() + "/" + maxHealth.ToString();

        //energy slider setup
        physicUI.energySlider.maxValue = maxEnergy;
        physicUI.energySlider.value = curEnergy;
        physicUI.energyUI.text = curEnergy.ToString();

        //Entity Info Setup
        physicUI.entityUI.text = charName;

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

