using UnityEngine;

[System.Serializable]
public class NodeData
{

    public int idNode;
    public Vector2 position;
}

[System.Serializable]
public class NodeDataCollection
{
    public NodeData[] nodeDataCollection;
}
