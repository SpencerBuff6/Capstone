using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]

public class StateAgent : Agent
{
    public StateMachine StateMachine { get; private set; }
    ChaseState chase;

    // Start is called before the first frame update
    void Start()
    {
        StateMachine = GetComponent<StateMachine>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Run", true);
        //if(Movement.Velocity.magnitude > 0)
        //{
        //    animator.SetBool("noticesplayer", chase.chasing);
        //    animator.SetBool("isattacking", true);
        //}
        StateMachine.Execute();
    }

}
