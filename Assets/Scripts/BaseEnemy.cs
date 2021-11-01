using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseEnemy
{
    public string name;

    public enum eType
    {
        FIRE,
        WATER,
        ELECTRIC,
        STONE, 
        EARTH //Grass
    }

    public enum eState
    {
        IDLE,
        ATTACK,
        ATTACK_RETURN,
        DEAD
    }

    public eType enemyType;

    public bool isStaggered = false;

    public float baseHP;
    public float currentHP;

    public float baseMP;
    public float currentMP;

    public float baseStagger;
    public float currentStagger;

    public float baseAttack;
    public float currentAttack;

    public float baseDefense;
    public float currentDefense;

    public float baseSpeed; //how long it takes for energy gauge to increase
    public float currentSpeed;

}
