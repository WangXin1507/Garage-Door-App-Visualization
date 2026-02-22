using UnityEngine;

public class CutConnection : MonoBehaviour
{
    [SerializeField] RenderNodeConnection connectionRenderer;

    [Tooltip("How close the controller needs to be to sever the line (in meters)")]
    public float sliceThreshold = 0.1f;

    void Update()
    {
        if (connectionRenderer == null || connectionRenderer.connections == null) return;

        Vector3 c = transform.position;

        for (int i = connectionRenderer.connections.Count - 1; i >= 0; i--)
        {
            var connection = connectionRenderer.connections[i];

            Vector3 a = connection.start.position;
            Vector3 b = connection.end.position;

            Vector3 ac = c - a;
            Vector3 bc = c - b;
            Vector3 ab = b - a;
            float perpendicularDistance = Vector3.Cross(ac, bc).magnitude / ab.magnitude;

            float dotProduct = Vector3.Dot(ac, ab.normalized);
            bool isBetweenNodes = dotProduct > 0 && dotProduct < ab.magnitude;

            if (isBetweenNodes && perpendicularDistance < sliceThreshold)
            {
                Destroy(connection.line.gameObject);
                connectionRenderer.connections.RemoveAt(i);
            }
        }
    }
}