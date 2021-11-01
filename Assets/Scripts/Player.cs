using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public enum eState
    {
        IDLE,
        ATTACK_DELIVER,
        ATTACK,
        ATTACK_RETURN,
        DEAD
    }

    public float health = 100;
    public CombatManager game;
    public Slider healthBar;
    public Slider energyBar;
    public int basicDamage = 10;
    public int specialDamage = 30;

    public int numberOfAttacks;

    public Animator animator;

    public int energyUsed;
    public float energyMax = 2;
    public float speed;
    public eState state = eState.IDLE;
    Vector3 target;
    Vector3 startPosition;

    public int CalculateEnergyUsed()
    {
        if(energyUsed == energyBar.value)
        {
            game.EnemyTargetPanel.SetActive(true);
            game.ItemOptionsPanel.SetActive(false);
            game.MainOptionsPanel.SetActive(false);
            game.AttackOptionsPanel.SetActive(false);
            game.ParadigmOptionsPanel.SetActive(false);
        }
        if(energyUsed < energyBar.value)
        {
            if(game.basicAttack.activeInHierarchy == true)
            {
                energyUsed += game.basicAttackValue;
            }
            if(game.basicAttack2.activeInHierarchy == true)
            {
                energyUsed += game.basicAttackValue;
            }
            if(game.ability.activeInHierarchy == true)
            {
                energyUsed += game.abilityValue;
            }
        }
        return energyUsed;
    }

    void Start()
    {
        healthBar.value = health;
        startPosition = transform.position;
    }

    private void Update()
    {
        switch (state)
        {
            case eState.IDLE:
                speed = 0;
                animator.SetBool("Run", false);
                if(energyBar.value <= 0)
                {
                    while (energyBar.value < energyMax)
                    {
                        energyBar.value += Time.deltaTime;
                    }
                } 
                break;
            case eState.ATTACK_DELIVER:
                speed = 10;
                animator.SetBool("Run", true);
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                if(Vector3.Distance(transform.position, target) <= .5f)
                {
                    state = eState.ATTACK;
                }
                break;
            case eState.ATTACK:
                animator.SetBool("Run", false);
                if (Vector3.Distance(transform.position, target) <= 2f)
                {
                    state = eState.ATTACK_RETURN;
                }
                break;
            case eState.ATTACK_RETURN:
                speed = 10;
                animator.SetBool("Run", true);
                transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, startPosition) <= 20f)
                {
                    state = eState.IDLE;
                }
                if(health == 0)
                {
                    state = eState.DEAD;
                }
                break;
            case eState.DEAD:
                break;
            default:
                break;
        }

    }

    public void Attack(Vector3 position)
    {
        state = eState.ATTACK_DELIVER;
        target = position;
    }

}
