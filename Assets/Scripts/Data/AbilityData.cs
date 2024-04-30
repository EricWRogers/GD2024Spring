using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityData
{
    public string abilityName = "AbilityOne";



    public int abValue = 10;
    public int energyCost = 1;


    public AbilityType type = AbilityType.Melee;
    public AbilityOutput output = AbilityOutput.Damage;


    

    //Todo Particles

    
    
}

public enum AbilityType
{
    Ranged,
    Melee
}

public enum AbilityOutput
{
    Damage,
    Heal
}