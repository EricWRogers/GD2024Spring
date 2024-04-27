using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Space(15)]

    public EntityData _target;


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

