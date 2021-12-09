using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMovement : Movement
{
    NavMeshAgent NavMeshAgent;
    public override Vector3 Velocity
    {
        get { return NavMeshAgent.velocity; }
        set { NavMeshAgent.velocity = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        NavMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshAgent.speed = speedMax;
        NavMeshAgent.angularSpeed = turnRate;
        Debug.DrawLine(NavMeshAgent.pathEndPosition, transform.position);

    }

    public override void ApplyForce(Vector3 force)
    {
        //
    }

    public override void MoveTowards(Vector3 target)
    {
        NavMeshAgent.SetDestination(target);
    }

    public override void Stop()
    {
        NavMeshAgent.isStopped = true;
    }

}
