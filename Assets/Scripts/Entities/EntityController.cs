using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EntityController : MonoBehaviour
{
    public EntityData entityData;
    public EntityController targetData;

    public Coroutine attackQueue;

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

   

    /*private void Update()
    {

        
    }*/

}
