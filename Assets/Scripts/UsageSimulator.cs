using System.Collections;
using UnityEngine;

public class UsageSimulator : IInteractable
{
    [SerializeField] RenderNodeConnection connectionRenderer;
    [SerializeField] DoorController doorController;

    public override void Interact()
    {
        if (AttackSimulator.attacked) return;
        StartCoroutine(SimulateSignal());
    }

    IEnumerator SimulateSignal()
    {
        foreach (var connection in connectionRenderer.connections)
        {
            connection.line.enabled = false;
        }

        yield return new WaitForSeconds(2);

        foreach (var connection in connectionRenderer.connections)
        {
            connection.line.enabled = true;

            yield return new WaitForSeconds(0.75f);
        }

        doorController.ToggleDoor();
    }
}
