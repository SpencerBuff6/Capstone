using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public enum CombatState
    {
        INITIATE,
        DURING_COMBAT,
        WINNER
    }

    #region DeclaredVariables

    public int basicAttackValue = 1;
    public int abilityValue = 2;

    public GameObject MainOptionsPanel;
    public GameObject AttackOptionsPanel;
    public GameObject ItemOptionsPanel;
    public GameObject ParadigmOptionsPanel;
    public GameObject EnemyTargetPanel;

    public GameObject basicAttack;
    public GameObject basicAttack2;
    public GameObject ability;

    public GameObject selectorOne;
    public GameObject selectorTwo;
    public GameObject selectorThree;

    public Button firstEnemyButton;
    public Button secondEnemyButton;
    public Button thirdEnemyButton;

    public TMP_Text playerName;
    public TMP_Text partyOneName;
    public TMP_Text partyTwoName;
    public TMP_Text partyThreeName;

    public TMP_Text enemyOneName;
    public TMP_Text enemyTwoName;
    public TMP_Text enemyThreeName;

    public Slider playerHealthBar;
    public Slider partyMemberOneHealthBar;
    public Slider partyMemberTwoHealthBar;
    public Slider partyMemberThreeHealthBar;

    public Slider enemyOneHealthBar;
    public Slider enemyTwoHealthBar;
    public Slider enemyThreeHealthBar;

    public Player player;

    public CombatState combatState;
    
    public EnemyAI enemyToBeAttacked;

    public int clickCounter = 0;
    private string selectedEnemy;

    public GameObject[] enemies;
    public GameObject[] partyMembers;

    private GameObject selectedEnemyObject;

    #endregion

    void Start()
    {
        MainOptionsPanel.SetActive(true);
        AttackOptionsPanel.SetActive(false);
        ItemOptionsPanel.SetActive(false);
        ParadigmOptionsPanel.SetActive(false);
        EnemyTargetPanel.SetActive(false);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        partyMembers = GameObject.FindGameObjectsWithTag("PartyMember");

        firstEnemyButton.GetComponentInChildren<TextMeshProUGUI>().text = enemies[0].gameObject.name;
        secondEnemyButton.GetComponentInChildren<TextMeshProUGUI>().text = enemies[1].gameObject.name;
        thirdEnemyButton.GetComponentInChildren<TextMeshProUGUI>().text = enemies[2].gameObject.name;

        playerName.text = player.gameObject.name;
        partyOneName.text = partyMembers[0].gameObject.name;
        partyTwoName.text = partyMembers[1].gameObject.name;
        partyThreeName.text = partyMembers[2].gameObject.name;

        enemyOneName.text = enemies[0].name;
        enemyTwoName.text = enemies[1].name;
        enemyThreeName.text = enemies[2].name;

        playerHealthBar.value = player.health;
        partyMemberOneHealthBar.value = partyMembers[0].GetComponent<PartyAI>().partyMember.currentHP;
        partyMemberTwoHealthBar.value = partyMembers[1].GetComponent<PartyAI>().partyMember.currentHP;
        partyMemberThreeHealthBar.value = partyMembers[2].GetComponent<PartyAI>().partyMember.currentHP;

        enemyOneHealthBar.value = enemies[0].GetComponent<EnemyAI>().enemy.currentHP;
        enemyTwoHealthBar.value = enemies[1].GetComponent<EnemyAI>().enemy.currentHP;
        enemyThreeHealthBar.value = enemies[2].GetComponent<EnemyAI>().enemy.currentHP;

    }

    void Update()
    {
        switch (combatState)
        {
            case CombatState.INITIATE:
                break;
            case CombatState.DURING_COMBAT:
                break;
            case CombatState.WINNER:
                break;
            default:
                break;
        }
        Debug.Log(player.state);
    }

    #region UIstuff

    public void MainPanelToAttackPanel()
    {
        MainOptionsPanel.SetActive(false);
        AttackOptionsPanel.SetActive(true);
        ItemOptionsPanel.SetActive(false);
        ParadigmOptionsPanel.SetActive(false);
        EnemyTargetPanel.SetActive(false);
    }

    public void MainPanelToItemPanel()
    {
        MainOptionsPanel.SetActive(false);
        AttackOptionsPanel.SetActive(false);
        ItemOptionsPanel.SetActive(true);
        ParadigmOptionsPanel.SetActive(false);
        EnemyTargetPanel.SetActive(false);
    }

    public void MainPanelToParadigmPanel()
    {
        MainOptionsPanel.SetActive(false);
        AttackOptionsPanel.SetActive(false);
        ItemOptionsPanel.SetActive(false);
        ParadigmOptionsPanel.SetActive(true);
        EnemyTargetPanel.SetActive(false);
    }

    public void AnyPanelToMainPanel()
    {
        MainOptionsPanel.SetActive(true);
        AttackOptionsPanel.SetActive(false);
        ItemOptionsPanel.SetActive(false);
        ParadigmOptionsPanel.SetActive(false);
        EnemyTargetPanel.SetActive(false);
        basicAttack.SetActive(false);
        basicAttack2.SetActive(false);
        ability.SetActive(false);
    }

    public void TurnOnBasicAttack()
    {
        clickCounter++;
        basicAttack.SetActive(true);
        if(basicAttack.activeInHierarchy == true && clickCounter >= 2)
        {
            basicAttack2.SetActive(true);
        }
        player.numberOfAttacks = clickCounter;
    }

    public void TurnOnAbility()
    {
        clickCounter++;
        ability.SetActive(true);
        player.numberOfAttacks = clickCounter;
    }

    public void AttackPanelToEnemySelection()
    {
        player.CalculateEnergyUsed();
        if(player.CalculateEnergyUsed() <= 0)
        {
            MainOptionsPanel.SetActive(false);
            AttackOptionsPanel.SetActive(false);
            ItemOptionsPanel.SetActive(false);
            ParadigmOptionsPanel.SetActive(false);
            EnemyTargetPanel.SetActive(true);
        }
    }
    
    public void EnemySelectionOneTurnOn()
    {
        selectorOne.SetActive(true);
        selectorTwo.SetActive(false);
        selectorThree.SetActive(false);
    }

    public void EnemySelectionTwoTurnOn()
    {
        selectorOne.SetActive(false);
        selectorTwo.SetActive(true);
        selectorThree.SetActive(false);
    }  
    
    public void EnemySelectionThreeTurnOn()
    {
        selectorOne.SetActive(false);
        selectorTwo.SetActive(false);
        selectorThree.SetActive(true);
    }

    public void EnemySelectionTurnOff()
    {
        selectorOne.SetActive(false);
        selectorTwo.SetActive(false);
        selectorThree.SetActive(false);
    }

    public void EnemySelected()
    {
        if(selectorOne.activeInHierarchy == true)
        {
            selectedEnemy = enemies[0].name;
        }
        if (selectorTwo.activeInHierarchy == true)
        {
            selectedEnemy = enemies[1].name;
        }
        if (selectorThree.activeInHierarchy == true)
        {
            selectedEnemy = enemies[2].name;
        }
    }

    #endregion

    #region DealingDamage

    public void PlayerAttackSelectedEnemy()
    {
        EnemySelected();
        player.Attack(GameObject.Find(selectedEnemy).transform.position);
        MainOptionsPanel.SetActive(true);
        AttackOptionsPanel.SetActive(false);
        ItemOptionsPanel.SetActive(false);
        ParadigmOptionsPanel.SetActive(false);
        EnemyTargetPanel.SetActive(false);
        selectorOne.SetActive(false);
        selectorTwo.SetActive(false);
        selectorThree.SetActive(false);
        player.energyBar.value = player.energyMax;
        selectedEnemyObject = GameObject.Find(selectedEnemy); 
        if(selectedEnemyObject != null)
        {
            enemyToBeAttacked = selectedEnemyObject.GetComponent<EnemyAI>();
        }
        for(int i = player.numberOfAttacks; i >= 1; i--)
        {
            DamageDealing();
        }
        clickCounter = 0;
        player.energyUsed = 0;
        if(player.state == Player.eState.IDLE)
        {
            Debug.Log("Hi");
            basicAttack.SetActive(false);
            basicAttack2.SetActive(false);
            ability.SetActive(false);
        }
    }

    public void DamageDealing()
    {
        if(basicAttack.activeInHierarchy && basicAttack2.activeInHierarchy)
        {
            enemyToBeAttacked.enemy.currentHP -= player.basicDamage;
        }
        if(ability.activeInHierarchy)
        {
            enemyToBeAttacked.enemy.currentHP -= player.specialDamage;
        }
        player.energyBar.value -= player.energyUsed;
    }

    #endregion
}
