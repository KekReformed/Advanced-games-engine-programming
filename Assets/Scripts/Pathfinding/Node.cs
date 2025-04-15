using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public Dictionary<int, Node> ConnectedNodes { get; private set; } = new Dictionary<int, Node>();
    public Vector3 Position { get; private set; }
    public Node BestExploredFrom { get; private set; }
    public bool Explored { get; private set; }
    public GameObject gameObject;
    
    float _gCost;
    float _hCost;
    
    public float FCost { get; private set; } = 9999;
    public int ID { get; private set; }

    
    /// <summary>
    /// Constructor for node class, If making a temporary node set the ID to -1
    /// </summary>
    /// <param name="position">Position of node</param>
    /// <param name="id">ID of node used to see if a node is the same</param>
    public Node(Vector3 position, int id)
    {
        ID = id;
        Position = position;
    }

    //Check all nodes, If there is a straight line to another node then add it to these nodes connected nodes
    public void SetConnectedNodes(List<Node> nodes)
    {
        foreach (Node node in nodes)
        {
            if(node.ID == ID) continue;
            
            if (Physics.Linecast(Position, node.Position, Settings.instance.pathfindingLayerMask)) continue;
            
            if(!node.ConnectedNodes.ContainsKey(ID)) node.ConnectedNodes.Add(ID, this);
            if(ConnectedNodes.ContainsKey(node.ID)) continue;

            ConnectedNodes.Add(node.ID, node);
        }
    }
    
    public Dictionary<int,Node> GetConnectedNodes(Node startNode, Node endNode)
    {
        Dictionary<int,Node> tempList = new Dictionary<int, Node>();
            
        foreach (Node connectedNode in ConnectedNodes.Values)
        {
            if(connectedNode.Explored) continue;
            
            connectedNode.CalcCosts(startNode,endNode);
            tempList.Add(connectedNode.ID,connectedNode);
        }
        
        Explored = true;
        return tempList;
    }

    public void ResetNode()
    {
        FCost = 9999;
        _gCost = 0;
        _hCost = 0;

        Explored = false;
        BestExploredFrom = null;

        ConnectedNodes.Remove(-1);
        ConnectedNodes.Remove(-2);
    }
    
    void CalcCosts(Node startNode, Node endNode)
    {
        _gCost = Vector2.Distance(Position, startNode.Position) * 10 + startNode._gCost;
        _hCost = Vector2.Distance(Position, endNode.Position) * 10;

        if (_gCost + _hCost <= FCost)
        {
            FCost = _gCost + _hCost;
            BestExploredFrom = startNode;
        }
    }
}
