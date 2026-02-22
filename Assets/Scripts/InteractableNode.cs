using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableNode : Node
{
    [SerializeField] private InputActionReference inputAction;
    [SerializeReference] private IInteractable interactable;

    protected override void Update()
    {
        base.Update();

        if (hovered && inputAction != null && inputAction.action.WasPressedThisFrame())
        {
            interactable?.Interact();
        }
    }    
}
