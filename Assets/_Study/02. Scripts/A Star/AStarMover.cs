using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStarMover : MonoBehaviour
{
    public GridManager gridManager;
    private AStar aStarCalculator;

    public GameObject startCube, endCube;
    public List<Node> pathList = new List<Node>();

    void Awake()
    {
        aStarCalculator = new AStar(); // 계산기 인스턴스
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);

        while (true)
        {
            GetPath();
            yield return new WaitForSeconds(1f);
        }
    }

    private void GetPath()
    {
        int startIndex = gridManager.GetGridIndex(startCube.transform.position); // 시작 위치의 인덱스 값
        int endIndex = gridManager.GetGridIndex(endCube.transform.position); // 도착지점 위치의 인덱스 값

        if (startIndex == -1 || endIndex == -1)
            return;
        
        Node startNode = gridManager.nodes[gridManager.GetRow(startIndex), gridManager.GetColumn(startIndex)];
        Node endNode = gridManager.nodes[gridManager.GetRow(endIndex), gridManager.GetColumn(endIndex)];

        pathList = aStarCalculator.FindPath(startNode, endNode, gridManager); // 길 찾기 계산 결과
    }

    void OnDrawGizmos() // 경로를 그려주는 기능
    {
        if (pathList == null || pathList.Count < 2)
            return;

        Gizmos.color = Color.green;
        for (int i = 0; i < pathList.Count - 1; i++)
            Gizmos.DrawLine(pathList[i].pos, pathList[i + 1].pos);
    }
}