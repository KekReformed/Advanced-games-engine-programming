using System.Collections.Generic;
using UnityEngine;

public class Node
{
    Dictionary<int,Node> _connectedNodes;
    public Vector2 Position { get; private set; }
    public Node BestExploredFrom { get; private set; }
    public bool Explored { get; private set; }
    public bool CostsCalculated { get; private set; }
    
    float _gCost;
    float _hCost;
    public float FCost { get; private set; } = 9999;
    public int ID { get; private set; }

    Node(Vector2 position, int id)
    {
        ID = id;
        Position = position;
    }

    //Check all nodes, If there is a straight line to another node then add it to these nodes connected nodes
    //Note: If experiencing performance issues it's probably a good idea to connect both nodes to each other and skip going to every node
    public void SetConnectedNodes(List<Node> nodes)
    {
        foreach (Node node in nodes)
        {
            if(Physics.Linecast(Position,node.Position)) _connectedNodes.Add(node.ID, node);
        }
    }
    
    void CalcCosts(Node startNode, Node endNode)
    {
        _gCost = Vector2.Distance(Position, startNode.Position) * 10 + startNode._gCost;
        _hCost = Vector2.Distance(Position, endNode.Position) * 10;

        if (_gCost + _hCost >= FCost)
        {
            FCost = _gCost + _hCost;
            BestExploredFrom = startNode;
        }
        
        
        CostsCalculated = true;
    }

    public List<Node> CalcConnectedNodes(Node startNode, Node endNode)
    {
        List<Node> tempList = new List<Node>();
            
        foreach (Node connectedNode in _connectedNodes.Values)
        {
            if(connectedNode.Explored) continue;
            
            connectedNode.CalcCosts(startNode,endNode);
            tempList.Add(connectedNode);
        }
        
        Explored = true;
        return tempList;
    }
}
