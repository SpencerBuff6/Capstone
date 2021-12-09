using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OOCEnemy : MonoBehaviour
{
    public LevelLoader levelLoader;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("CombatEntered");
        if(collision.transform.tag == "Player")
        {
            levelLoader.LoadBattleLevel();
        }
    }
}
