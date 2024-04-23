using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EntityData
{
    public string characterName;

    [Space(10)]

    public int maxHealth;
    public int curHealth;

    [Space(10)]

    public int maxEnergy;
    public int energyCurr;

    [Space(10)]

    public float maxOCPoints;
    public float currentOCPoints;

    [Space(10)]

    public float speed;
    public float curSpeed;

    public EntityState entityState;
    public EntityGroup entityGroup;
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
    Attacking
}

