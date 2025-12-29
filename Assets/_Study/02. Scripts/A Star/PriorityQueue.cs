using System.Collections.Generic;
using UnityEngine;

public class PriorityQueue
{
    private List<Node> nodes = new List<Node>();

    public int Length
    {
        get => nodes.Count;
    }

    public bool Contains(Node node)
    {
        return nodes.Contains(node);
    }

    public Node First()
    {
        if (Length == 0)
            return null;

        return nodes[0];
    }

    public void Push(Node node)
    {
        nodes.Add(node);
        nodes.Sort();
    }

    public void Remove(Node node)
    {
        nodes.Remove(node);
        nodes.Sort();
    }
}