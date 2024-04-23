using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player_Controller : MonoBehaviour
{
    public EntityData entityData;
    public LayerMask EncounterLayer;
    const float baseThreshold = 1;
    public float currentThreshold = baseThreshold;

    // Update is called once per frame
    void Update()
    {
        RollEncounter();
    }



    public void RollEncounter()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, EncounterLayer)!= null || Input.GetKeyDown(KeyCode.LeftShift))
        {
            int chance = UnityEngine.Random.Range(1, 101);
            if (chance < currentThreshold)
            {
                Debug.Log("Encounter!");
                currentThreshold = baseThreshold;
            }
            else
            {
                currentThreshold += 1;
            }
        }
    }

}
