using System.Collections.Generic;
using UnityEngine;

public class PathfindingManager : MonoBehaviour
{

    public static PathfindingManager Instance;
    public List<Node> Nodes = new List<Node>();
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        int count = 0;
        
        if(Instance != null) Debug.LogError("Multiple pathfinding managers detected! there should only be 1!");
        Instance = this;
        
        foreach (Transform child in transform)
        {
            Node node = new Node(child.position, count);
            node.gameObject = child.gameObject;
            
            Nodes.Add(node);
            count++;
        }
        
        foreach (Node node in Nodes)
        {
            node.SetConnectedNodes(Nodes);
            
            //Debug code that shows connections between nodes with line renderers. Break in case of emergency
            //LineRenderer lr = node.gameObject.AddComponent<LineRenderer>();
            //lr.positionCount = node.ConnectedNodes.Count * 2 + 1;
            //count = 1;
            //
            //lr.SetPosition(0, node.Position);
            //
            //foreach (Node connectedNode in node.ConnectedNodes.Values)
            //{
            //    lr.SetPosition(count, node.Position);
            //    lr.SetPosition(count + 1, connectedNode.Position);
            //    count += 2;
            //}
            //
            //lr.enabled = false;
        }
    }

    public static Node[] FindPath(GameObject unit, Vector3 position)
    {
        Dictionary<int,Node> exploredNodes = new Dictionary<int, Node>();
        List<Node> path = new List<Node>();

        Vector3 StartNodeVector = new Vector3(unit.transform.position.x, unit.transform.position.y + 0.25f, unit.transform.position.z);
        Vector3 EndNodeVector = new Vector3(position.x, position.y + 0.25f, position.z);
        
        Node startNode = new Node(StartNodeVector, -1);
        Node endNode = new Node(EndNodeVector, -2);
        
        startNode.SetConnectedNodes(Instance.Nodes);
        endNode.SetConnectedNodes(Instance.Nodes);
        
        MergeNodes(exploredNodes,startNode.GetConnectedNodes(startNode,endNode));
        
        int count = 0;
        
        for (int i = 0; i < 9999; i++)
        {
            Node nextNode = GetNextNode(exploredNodes);

            if (nextNode.ID == -2) break;
            
            MergeNodes(exploredNodes,nextNode.GetConnectedNodes(nextNode,endNode));
            
            count = i;
        }

        if (count >= 9000)
        {
            Debug.LogError("Couldn't find short enough path!");
            ResetNodes();
            return null;
        }

        Node previousNode = endNode;

        count = 0;
        
        for (int i = 0; i < 9999; i++)
        {
            count = i;
            
            previousNode = previousNode.BestExploredFrom;
            path.Add(previousNode);
            
            if (previousNode.ID == -1) break;
        }
        
        if (count >= 9000)
        {
            Debug.LogError("Path too long! something has gone very very wrong!");
            ResetNodes();
            return null;
        }

        path.Reverse();
        
        Node[] pathArray = path.ToArray();

        ResetNodes();
        
        return pathArray;
    }

    static Node GetNextNode(Dictionary<int,Node> exploredNodes)
    {
        float bestFCost = 99999;
        Node bestNode = null;
        
        foreach (Node node in exploredNodes.Values)
        {
            if(node.Explored) continue;
            if (node.FCost > bestFCost) continue;
            
            bestNode = node;
            bestFCost = node.FCost;
        }

        return bestNode;
    }

    static void MergeNodes(Dictionary<int,Node> origNodes, Dictionary<int,Node> newNodes)
    {
        foreach (Node node in newNodes.Values)
        {
            origNodes[node.ID] = node;
        }
    }

    static void ResetNodes()
    {
        foreach (Node node in Instance.Nodes)
        {
            node.ResetNode();
        }
    }
}
