using UnityEngine;

public class AttackSimulator : MonoBehaviour, IInteractable
{
    [SerializeField] RenderNodeConnection connectionRenderer;

    [SerializeField] int targetConnectionIndex;
    [SerializeField] Material attackedMat;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (connectionRenderer == null || 
            targetConnectionIndex > connectionRenderer.connections.Count) return;

        LineRenderer targetLine = connectionRenderer.connections[targetConnectionIndex].line;

        targetLine.material = attackedMat;
        targetLine.startWidth = targetLine.endWidth = 0.02f;
    }
}