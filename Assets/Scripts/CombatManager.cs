using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatManager : MonoBehaviour
{
    private int clickCounter = 0;
    private string selectedEnemy;

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

    public Player player;

    private GameObject[] enemies;

    private GameObject selectedEnemyObject;
    private EnemyAI enemyToBeAttacked;


    // Start is called before the first frame update
    void Start()
    {
        MainOptionsPanel.SetActive(true);
        AttackOptionsPanel.SetActive(false);
        ItemOptionsPanel.SetActive(false);
        ParadigmOptionsPanel.SetActive(false);
        EnemyTargetPanel.SetActive(false);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        firstEnemyButton.GetComponentInChildren<TextMeshProUGUI>().text = enemies[0].gameObject.name;
        secondEnemyButton.GetComponentInChildren<TextMeshProUGUI>().text = enemies[1].gameObject.name;
        thirdEnemyButton.GetComponentInChildren<TextMeshProUGUI>().text = enemies[2].gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        //PlayerAttackSelectedEnemy();
    }

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
    }

    public void TurnOnAbility()
    {
        ability.SetActive(true);
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
    
    public void PlayerAttackSelectedEnemy()
    {
        EnemySelected();
        player.Attack(GameObject.Find(selectedEnemy).transform.position);
        MainOptionsPanel.SetActive(true);
        AttackOptionsPanel.SetActive(false);
        ItemOptionsPanel.SetActive(false);
        ParadigmOptionsPanel.SetActive(false);
        EnemyTargetPanel.SetActive(false);
        basicAttack.SetActive(false);
        basicAttack2.SetActive(false);
        ability.SetActive(false);
        selectorOne.SetActive(false);
        selectorTwo.SetActive(false);
        selectorThree.SetActive(false);
        selectedEnemyObject = GameObject.Find(selectedEnemy);
        if(selectedEnemyObject != null)
        {
            enemyToBeAttacked = selectedEnemyObject.GetComponent<EnemyAI>();
        }
        DamageDealing();
        clickCounter = 0;
        player.energyUsed = 0;
    }

    public void DamageDealing()
    {
        enemyToBeAttacked.health -= player.damage;
    }
}
