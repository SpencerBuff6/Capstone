using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PartyAI : MonoBehaviour
{
    public BaseParty partyMember;
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
        healthBar.value = partyMember.baseHP;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject gameObject in manager.enemies)
        {
            possibleTargets.Add(gameObject);
        }
        targetNum = Random.Range(0, possibleTargets.Count - 1);
        healthBar.value = partyMember.currentHP;
        switch (state)
        {
            case eState.IDLE:
                if (partyMember.currentHP <= 0)
                {
                    state = eState.DEAD;
                }
                animator.SetBool("Run", false);
                partyMember.currentSpeed -= Time.deltaTime;
                target = possibleTargets[targetNum].transform.position;
                if (partyMember.currentSpeed <= 0)
                {
                    state = eState.ATTACK_DELIVER;
                }
                break;
            case eState.ATTACK_DELIVER:
                if (partyMember.currentHP <= 0)
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
                if (partyMember.currentHP <= 0)
                {
                    state = eState.DEAD;
                }
                animator.SetBool("Run", false);
                animator.SetBool("Attack", true);
                Attack();
                state = eState.ATTACK_RETREAT;
                break;
            case eState.ATTACK_RETREAT:
                partyMember.currentSpeed = partyMember.baseSpeed;
                movementSpeed = 10;
                animator.SetBool("Run", true);
                animator.SetBool("Attack", false);
                transform.position = Vector3.MoveTowards(transform.position, startPosition, movementSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, startPosition) <= 20f)
                {
                    state = eState.IDLE;
                }
                if (partyMember.currentHP <= 0)
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
        possibleTargets[targetNum].GetComponent<EnemyAI>().enemy.currentHP -= partyMember.currentAttack;   
    }
}
