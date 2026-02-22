using System;
using System.Collections.Generic;
using UnityEngine;

public class RenderNodeConnection : MonoBehaviour
{
    public List<Connection> connections = new();

    [SerializeField] Material lineMaterial;
    [SerializeField] float lineWidth = 0.05f;
    [SerializeField] float flowSpeed = 0.5f;

    void Awake()
    {
        for (int i = 0; i < connections.Count; i++)
        {
            GameObject lineObj = new($"DataLine_{i}"); // attach linerenderer to self
            lineObj.layer = LayerMask.NameToLayer("Node");
            lineObj.transform.SetParent(transform);

            LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;
            lineRenderer.textureMode = LineTextureMode.Tile;

            connections[i].line = lineRenderer;

            if (lineMaterial != null)
            {
                lineRenderer.material = lineMaterial;
            }
        }
    }

    void Update()
    {
        foreach (Connection connection in connections)
        {
            connection.line.SetPosition(0, connection.start.position);
            connection.line.SetPosition(1, connection.end.position);
            connection.line.material.SetTextureOffset("_BaseMap", -new Vector2(Time.time * flowSpeed, 0));
        }
    }
}

[Serializable]
public class Connection
{
    public Transform start;
    public Transform end;

    [NonSerialized] public LineRenderer line;
}