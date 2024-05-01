using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using UnityEditor.Playables;

[System.Serializable]
public class EntityData
{
    public string characterName = "CharacterName";
    

    [Space(10)]
     public AbilityData basicAttack;
    public List <AbilityData> entityAbilities;

    [Space(10)]

    public UIData entUI;

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

    
    EntityState savedState;


    [Space(10)]

    public UnityEvent OnAttack;
    public UnityEvent OnAttackQueue;
    public UnityEvent onWasAttacked;
    public UnityEvent onJustReady;



    [Space(15)]

    public EntityData _target;

    [HideInInspector]
    public EntityController _charCont;

    public void Init()
    {
        if (entityGroup == EntityGroup.Friendly)
        {
            entUI.charData = this;
            entUI.Init(maxHealth, curHealth, maxEnergy, curEnergy, characterName, speedLimit, maxOC); 
            onJustReady.AddListener(OnReadyDefault);
        }
        

        OnAttack.AddListener(EntityAttackDefault);
        onWasAttacked.AddListener(EntityAttackedDefault);
        OnAttackQueue.AddListener(OnAttackQueueDefault);

        entityState = EntityState.Idle;
    }


    public IEnumerator QueueAttack(AbilityData ability)
    {
        if (entityState == EntityState.Died || entityState == EntityState.TryingAttack)
        {
            _charCont.ClearAttackQueue();
            yield break;
        }

        entityState = EntityState.TryingAttack;

        OnAttackQueue.Invoke();

        yield return new WaitUntil(()=>_target.IsAttackable);

        if(_target.entityState == EntityState.Died)
        {
            yield break;
        }
        

        _target.SaveEntityState();
        _target.entityState = EntityState.Attacked;


        Debug.Log("attacked with " + ability.abilityName + "at" + _target.characterName);



        switch(ability.output)
        {
            case AbilityOutput.Damage:
                _target.Damage(ability.abValue);
                break;
            case AbilityOutput.Heal:
                _target.Heal(ability.abValue);
                break;
        }

        if(entityGroup == EntityGroup.Friendly)
        {
            IncreaseOC(5);
        }

        entityState = EntityState.Attacking;

        _charCont.ClearAttackQueue();
    }


    public void Heal(int healAmount)
    {
        curHealth = Mathf.Clamp(curHealth + healAmount, 0, maxHealth);
        entUI.UpdateHealthBar(curHealth, maxHealth);

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
            entUI.UpdateHealthBar(curHealth, maxHealth);
        }

        onWasAttacked.Invoke();
    }

    public bool CanBeAttacked
    {
        get
        {
            return entityState == EntityState.Idle || entityState == EntityState.Ready;
        }
        
    }

    public bool IsAttackable
    {
        get
        {
            return entityState == EntityState.Idle || entityState == EntityState.Ready;
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

        entUI.UpdateOCBar(curOC);
    }
    void  EntityAttackDefault()
    {
       

    }

    void EntityAttackedDefault()
    {
        Debug.Log(characterName + " was attacked");
        if(entityState != EntityState.Died)
        {
            entityState = savedState;
        }
        
    }

    void OnReadyDefault()
    {
        SelectCharacter();
    }

    void OnAttackQueueDefault()
    {
        curSpeed = 0;
    }

    public void SelectCharacter()
    {
        if (!ActionReady)
        {
            return;
        }

        UIManager.Instance.actionWindow.SetActive(true);
        UIManager.Instance.abilityWindow.SetActive(false);

        foreach(var item in GameObject.FindObjectsOfType<EntityController>())
        {
            if (item.entityData.entityGroup != EntityGroup.Enemy)
            {
                item.entityData.ResetUINameText();
            }
        }
        
        entUI.physicUI.entityUI.color = Color.cyan;
        BattleManager.Instance.currentCharacter = _charCont;
    }

    public void SaveEntityState()
    {
        savedState = entityState;
    }

    public void ResetUINameText()
    {
        entUI.physicUI.entityUI.color = Color.white;

    }

    public IEnumerator EntityLoop()
    {
        while (entityState != EntityState.Died)
        {
            while (curSpeed < speedLimit)
            {
                yield return new WaitUntil(() => entityState != EntityState.TryingAttack);
       
                curSpeed += Time.deltaTime;

                if (entityGroup == EntityGroup.Friendly)
                {
                    entUI.UpdateTimeBar(curSpeed);
                }

                if(entityState == EntityState.Attacked)
                {
                    entityState = EntityState.Idle;
                }
                yield return null;

            }
            //We are ready when the character exits the loop
            curSpeed = speedLimit;
            entityState= EntityState.Ready;
            
            onJustReady.Invoke();
            yield return new WaitUntil(()=> entityState == EntityState.Attacking);

            entityState = EntityState.Idle;

            yield return new WaitUntil(()=> entityState == EntityState.Idle);

        }

    }

}

[System.Serializable]
public class UIData
{
    public RowUI physicUI;
    public int placeInUi = 1;

    [HideInInspector]
    public EntityData charData;
    
    public void Init(int maxHealth, int curHealth, int maxEnergy, int curEnergy, string charName, float speedLimit, float OCmax)
    {
        placeInUi = UIManager.currentUICount;;
        if (placeInUi == 1)
        {
            //Do not spawn another row
            physicUI = UIManager.Instance.defaultRowUI;
            UIManager.Instance.firstOnClick.charHolder = charData;
        
        }
        else
        {
            //row spawning
            UIManager.Instance.SpawnRow(out physicUI, charData);

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
    Died,
    TryingAttack
}

