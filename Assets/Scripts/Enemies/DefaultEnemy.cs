using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Create New")]
public class Enemy : ScriptableObject
{
    [SerializeField] new string name;

    [TextArea]
    [SerializeField] string description;
        
    [SerializeField] Sprite enemySprite;

    [SerializeField] EnemyType type;

    //Stats
    [SerializeField] int hp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int attackSpeed;

    public enum EnemyType
    {
        None,
        Heavy,
        Light,
        Basic
    }

}
