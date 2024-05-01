using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntityController : MonoBehaviour
{
    public EntityData entityData;
    public EntityController targetData;

    public Coroutine attackQueue = null;

    public Sprite sprite;

    private void Awake()
    {
        entityData._charCont = this;
    }
    private void Start()
    {
        entityData.Init();
        entityData._target = targetData.entityData;

        StartCoroutine(entityData.EntityLoop());
    }

    public void ClearAttackQueue()
    {
        if (attackQueue != null)
        {
            StopCoroutine((attackQueue));
            attackQueue = null;
        }
    
    }

   

    /*private void Update()
    {

        
    }*/

}
