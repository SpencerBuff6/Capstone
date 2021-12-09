using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SearchNode : MonoBehaviour
{
    public static SearchNode GetRandomSearchNode()
    {
        SearchNode[] SearchNodes = FindObjectsOfType<SearchNode>();
        return SearchNodes[Random.Range(0, SearchNodes.Length)];
    }
}
