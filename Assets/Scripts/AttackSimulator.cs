using UnityEngine;

public class AttackSimulator : MonoBehaviour
{
    [SerializeField] RenderNodeConnection connectionRenderer;

    [SerializeField] int targetConnectionIndex;
    [SerializeField] Material attackedMat;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Attack();
        }
    }

    public void Attack()
    {
        if (connectionRenderer == null || 
            targetConnectionIndex > connectionRenderer.connections.Count) return;

        LineRenderer targetLine = connectionRenderer.connections[targetConnectionIndex].line;

        targetLine.material = attackedMat;
        targetLine.startWidth = targetLine.endWidth = 0.02f;
    }
}