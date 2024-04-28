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

    public float maxOC = 100;
    public float curOC = 100;

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
           entityUI.Init(maxHealth, curHealth, maxEnergy, curEnergy, characterName, speedLimit, maxOC); 
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

        if(entityGroup == EntityGroup.Friendly)
        {
            IncreaseOC(5);
        }
    }

    public void Damage(int damageAmount)
    {
        curHealth -= damageAmount;

        


        if (curHealth <= 0)
        {
            curHealth = 0;
            entityState = EntityState.Died;
        }
        //OVERCHARGEEEEE
        if (entityGroup == EntityGroup.Friendly)
        {
            IncreaseOC(5);
            entityUI.UpdateHealthBar(curHealth, maxHealth);
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

    void IncreaseOC(int amount)
    {
        curOC += amount;
        curOC = Mathf.Clamp(curOC, 0, maxOC);

        entityUI.UpdateOCBar(curOC);
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

                if(entityGroup == EntityGroup.Friendly)
                {
                    entityUI.UpdateTimeBar(curSpeed);
                }
                
                entityState = EntityState.Idle;
            }
            
            yield return null;
        }  
    }

}

[System.Serializable]
public class UIData
{
    public RowUI physicUI;
    public int placeInUi = 1;

    
    public void Init(int maxHealth, int curHealth, int maxEnergy, int curEnergy, string charName, float speedLimit, float OCmax)
    {
        placeInUi = UIManager.currentUICount;;
        if (placeInUi == 1)
        {
            //Do not spawn another row
            physicUI = UIManager.Instance.defaultRowUI;
        
        }
        else
        {
            //row spawning
            UIManager.Instance.SpawnRow(out physicUI);

        }
        //Health Slider Setup
        physicUI.healthSlider.maxValue = maxHealth;
        UpdateHealthBar(curHealth, maxHealth);

        //energy slider setup
        physicUI.energySlider.maxValue = maxEnergy;
        UpdateEnergyBar(curEnergy);

        //Entity Info Setup
        physicUI.entityUI.text = charName;

        //Limit/Timebar
        physicUI.OCSlider.maxValue = OCmax;
        physicUI.OCSlider.value = 0;

        physicUI.timeSlider.maxValue = speedLimit;
        physicUI.timeSlider.value = 0;

        UIManager.currentUICount++;

    }

    public void UpdateTimeBar(float currentProg)
    {
        physicUI.timeSlider.value = currentProg;

    }

    public void UpdateOCBar(float currentProg)
    {
        physicUI.OCSlider.value = currentProg;
    }

    public void UpdateHealthBar(int currentAmount, int maxAmount)
    {
        physicUI.healthSlider.value = currentAmount;
        physicUI.healthUI.text = currentAmount.ToString() + "/" + maxAmount.ToString();
    }

    public void UpdateEnergyBar(int currentAmount)
    {
        physicUI.energySlider.value = currentAmount;
        physicUI.energyUI.text = currentAmount.ToString();
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

