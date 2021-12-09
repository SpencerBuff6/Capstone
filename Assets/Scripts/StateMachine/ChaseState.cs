using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public NavMovement movement;
    public bool chasing = false;
    public override void Enter(Agent owner)
    {
        Debug.Log(GetType().Name + " Enter");
    }

    public override void Execute(Agent owner)
    {
        GameObject[] gameObjects = owner.perception.GetGameObjects();
        GameObject player = Perception.GetGameObjectFromTag(gameObjects, "Player");

        if(player != null)
        {
            chasing = true;
            movement.speedMax = 5;
            movement.accelerationMax = 4;
        }

        //((StateAgent)owner).StateMachine.SetState("AttackState");
    }

    public override void Exit(Agent owner)
    {
        Debug.Log(GetType().Name + " Exit");
    }
}
