using UnityEngine;

public class AttackSimulator : IInteractable
{
    [SerializeField] RenderNodeConnection connectionRenderer;

    [SerializeField] int targetConnectionIndex;
    [SerializeField] Material attackedMat;

    public static bool attacked;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Interact();
        }
    }

    public override void Interact()
    {
        if (connectionRenderer == null || 
            targetConnectionIndex > connectionRenderer.connections.Count) return;

        LineRenderer targetLine = connectionRenderer.connections[targetConnectionIndex].line;

        targetLine.material = attackedMat;
        targetLine.startWidth = targetLine.endWidth = 0.02f;

        attacked = true;
    }
}