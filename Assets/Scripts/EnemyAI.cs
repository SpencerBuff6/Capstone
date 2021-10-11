using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public enum eState
    {
        IDLE,
        ATTACK,
        ATTACK_RETREAT
        
    }

    public int health = 100;
    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = health;
    }
}
