using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNode : SearchNode
{
    public WaypointNode nextWaypoint;

    private void OnTriggerEnter(Collider other)
    {
        Agent Agent = other.GetComponent<Agent>();
        Debug.Log(Agent);
        if(Agent != null)
        {
            SearchPath searchPath = Agent.GetComponent<SearchPath>();
            if (searchPath.Node == this)
            {
                searchPath.Node = nextWaypoint;
            }
        }
    }
}
