using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public BaseEnemy enemy;
    public CombatManager manager;
    public Slider healthBar;
    public float movementSpeed;
    public Animator animator;

    Vector3 startPosition;

    public enum eState
    {
        IDLE,
        ATTACK_DELIVER,
        ATTACK,
        ATTACK_RETREAT,
        DEAD
    }

    private eState state;
    private Vector3 target;
    private int targetNum;
    private List<GameObject> possibleTargets = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        healthBar.value = enemy.baseHP;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        possibleTargets.Add(manager.player.gameObject);
        foreach (GameObject gameObject in manager.partyMembers)
        {
            possibleTargets.Add(gameObject);
        }
        targetNum = Random.Range(0, possibleTargets.Count - 1);
        healthBar.value = enemy.currentHP;
        switch (state)
        {
            case eState.IDLE:
                if (enemy.currentHP <= 0)
                {
                    state = eState.DEAD;
                }
                animator.SetBool("Run", false);
                enemy.currentSpeed -= Time.deltaTime;
                target = possibleTargets[targetNum].transform.position;
                if(enemy.currentSpeed <= 0)
                {
                    state = eState.ATTACK_DELIVER;
                }
                break;
            case eState.ATTACK_DELIVER:
                if (enemy.currentHP <= 0)
                {
                    state = eState.DEAD;
                }
                movementSpeed = 10;
                animator.SetBool("Run", true);
                transform.position = Vector3.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, target) <= .5f)
                {
                    state = eState.ATTACK;
                }
                break;
            case eState.ATTACK:
                if (enemy.currentHP <= 0)
                {
                    state = eState.DEAD;
                }
                animator.SetBool("Run", false);
                animator.SetBool("Attack", true);
                Attack();
                state = eState.ATTACK_RETREAT;
                break;
            case eState.ATTACK_RETREAT:
                enemy.currentSpeed = enemy.baseSpeed;
                movementSpeed = 10;
                animator.SetBool("Attack", false);
                animator.SetBool("Run", true);
                transform.position = Vector3.MoveTowards(transform.position, startPosition, movementSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, startPosition) <= 20f)
                {
                    state = eState.IDLE;
                }
                if (enemy.currentHP <= 0)
                {
                    state = eState.DEAD;
                }
                break;
            case eState.DEAD:
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }

    void Attack()
    {
        if(possibleTargets[targetNum].CompareTag("Player"))
        {
            possibleTargets[targetNum].GetComponent<Player>().health -= enemy.currentAttack;
            Debug.Log(manager.player.health);
        }
        else if (possibleTargets[targetNum].CompareTag("PartyMember"))
        {
            possibleTargets[targetNum].GetComponent<PartyAI>().partyMember.currentHP -= enemy.currentAttack;
        }
    }
}
