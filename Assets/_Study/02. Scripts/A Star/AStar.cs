using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AStar
{
    private float HeuristicEstimateCost(Node currNode, Node endNode)
    {
        return (currNode.pos - endNode.pos).magnitude;
    }

    public List<Node> FindPath(Node startNode, Node endNode, GridManager gridManager)
    {
        gridManager.ResetNodes();

        var openList = new PriorityQueue(); // 지나갈 예정인 노드
        var closedList = new PriorityQueue(); // 지나왔던 노드

        startNode.nodeTotalCost = 0;
        startNode.estimateCost = HeuristicEstimateCost(startNode, endNode);
        startNode.parent = null;

        openList.Push(startNode);

        Node node = null;

        while (openList.Length != 0)
        {
            node = openList.First();
            openList.Remove(node);
            closedList.Push(node);

            if (node == endNode)
                return CalculatePath(node);

            List<Node> neighbors = new List<Node>();
            gridManager.GetNeighbors(node, neighbors);

            for (int i = 0; i < neighbors.Count; i++)
            {
                Node neighborNode = neighbors[i];
                
                if (closedList.Contains(neighborNode))
                    continue; // Skip

                float costToMove = (node.pos - neighborNode.pos).magnitude; // 목적지까지의 최단 거리
                float tentativeG = node.nodeTotalCost + costToMove; // 아직 결정하지 않은 추정 거리

                bool isInOpenList = openList.Contains(neighborNode);

                if (!isInOpenList || tentativeG < neighborNode.nodeTotalCost)
                {
                    neighborNode.nodeTotalCost = tentativeG;
                    neighborNode.estimateCost = HeuristicEstimateCost(neighborNode, endNode);
                    neighborNode.parent = node;

                    if (!isInOpenList)
                        openList.Push(neighborNode);
                }
            }
        }

        Debug.LogError("Destination Path Not Found");
        return null;
    }

    private List<Node> CalculatePath(Node node)
    {
        List<Node> list = new List<Node>();
        while (node != null)
        {
            list.Add(node);
            node = node.parent;
        }

        list.Reverse();
        return list;
    }
}