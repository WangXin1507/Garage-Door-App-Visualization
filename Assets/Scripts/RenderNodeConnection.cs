using System;
using System.Collections.Generic;
using UnityEngine;

public class RenderNodeConnection : MonoBehaviour
{
    public List<Connection> connections = new();

    [SerializeField] Material lineMaterial;
    [SerializeField] float lineWidth = 0.05f;
    [SerializeField] float flowSpeed = 0.5f;

    List<Connection> origConnections;

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
        origConnections = new List<Connection>(connections);
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

    public void ResetConnections()
    {
        // clear existing connections
        foreach (Connection c in connections)
        {
            Destroy(c.line.gameObject);
        }
        connections.Clear();

        // recreate original connections
        for (int i = 0; i < origConnections.Count; i++)
        {
            GameObject lineObj = new($"DataLine_{i}");
            lineObj.layer = LayerMask.NameToLayer("Node");
            lineObj.transform.SetParent(transform);

            LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = lineRenderer.endWidth = lineWidth;
            lineRenderer.textureMode = LineTextureMode.Tile;

            Connection newConnection = new Connection
            {
                start = origConnections[i].start,
                end = origConnections[i].end,
                line = lineRenderer
            };

            if (lineMaterial != null)
            {
                lineRenderer.material = lineMaterial;
            }
            connections.Add(newConnection);
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