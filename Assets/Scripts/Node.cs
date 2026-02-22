using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Node : MonoBehaviour
{
    [SerializeField] TextMeshPro label;
    [SerializeField] Canvas descrp;
    [SerializeField] RectTransform descrpRect;
    [SerializeField] Vector3 targetScale;
    [SerializeField] float enlargeSpeed;
    Vector3 origScale;
    protected bool hovered;

    void Awake()
    {
        origScale = transform.localScale;
        if (descrp) descrp.enabled = false;

        XRSimpleInteractable xrInteract = GetComponent<XRSimpleInteractable>();
        xrInteract.hoverEntered.AddListener(OnHoverEnter);
        xrInteract.hoverExited.AddListener(OnHoverExit);
    }

    protected virtual void Update()
    {
        if (hovered)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * enlargeSpeed);
            if (descrp) descrp.enabled = true;
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, origScale, Time.deltaTime * enlargeSpeed);
            if (descrp) descrp.enabled = false;
        }
        //float dist = Vector3.Distance(transform.position, Camera.main.transform.position);
        //descrpRect.localScale = origScale * dist / 20f;
    }

    void OnHoverEnter(HoverEnterEventArgs args)
    {
        hovered = true;
    }

    void OnHoverExit(HoverExitEventArgs args)
    {
        hovered = false;
    }

    void OnValidate()
    {
        if (label)
        {
            label.text = gameObject.name;
        }
    }
}
