using UnityEngine;
using UnityEngine.InputSystem;

public class InteractableNode : Node
{
    [SerializeField] private InputActionReference inputAction;
    [SerializeField] private IInteractable interactable;

    protected override void Update()
    {
        base.Update();

        if (hovered && inputAction != null && inputAction.action.WasPressedThisFrame())
        {
            hovered = false;
            interactable?.Interact();
        }
    }    
}
