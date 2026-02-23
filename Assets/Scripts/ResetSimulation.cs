using UnityEngine;

public class ResetSimulation : IInteractable
{
    [SerializeField] RenderNodeConnection connectionRenderer;

    public override void Interact()
    {
        connectionRenderer.ResetConnections();
    }
}
