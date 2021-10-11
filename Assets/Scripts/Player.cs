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
        ATTACK,
        ATTACK_RETURN
    }


    public int health = 100;
    public CombatManager game;
    public Slider energyBar;
    public float speed = 0;
    public int damage = 10;

    public Animator animator;

    public int energyUsed;
    eState state = eState.IDLE;
    Vector3 target;
    Vector3 startPosition;
    public int CalculateEnergyUsed()
    {
        Debug.Log(energyUsed);
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

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        switch (state)
        {
            case eState.IDLE:
                break;
            case eState.ATTACK:
                speed = 10;
                animator.SetFloat("speed", speed);
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, target) <= 2f)
                {
                    state = eState.ATTACK_RETURN;
                }
                break;
            case eState.ATTACK_RETURN:
                speed = 10;
                transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
                if (Vector3.Distance(transform.position, startPosition) <= 20f)
                {
                    state = eState.IDLE;
                }
                break;
            default:
                break;
        }
        Debug.Log(state);
    }

    public void Attack(Vector3 position)
    {
        state = eState.ATTACK;
        target = position;
    }

}
