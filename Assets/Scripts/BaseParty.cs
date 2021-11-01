using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseParty
{
    public string name;

    public enum eFightingStyle
    {
        AGGRESSIVE,
        DEFENSIVE,
        HEALER
    }

    public enum eState
    {
        IDLE,
        ATTACK,
        ATTACK_RETURN,
        DEAD
    }

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
