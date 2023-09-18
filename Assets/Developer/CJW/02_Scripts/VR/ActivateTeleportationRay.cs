using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateTeleportationRay : MonoBehaviour
{
    // Teleportation 
    public GameObject leftTeleportation;
    public GameObject rightTeleportation;

    // Grab
    public GameObject leftGrabRay;
    public GameObject rightGrabRay;

    public InputActionProperty leftActivate;
    public InputActionProperty rightActivate;

    public InputActionProperty leftCancel;
    public InputActionProperty rightCancel;

    // UI Ray
    public XRRayInteractor leftRay;
    public XRRayInteractor rightRay;

    public XRInteractorLineVisual leftLineVisual;
    public XRInteractorLineVisual rightLineVisual;

    // bool
    public bool isLeftRayHovering;
    public bool isRightRayHovering;

    public bool isLeftTelepotation;
    public bool isRightTelepotation;

    private void Start()
    {
        leftTeleportation.SetActive(false);
        rightTeleportation.SetActive(false);
    }

    private void Update()
    {
        OnOffTelepotation();
        OnOffGrabRay();
    }

    public void OnOffTelepotation()
    {
        isLeftRayHovering = leftRay.TryGetHitInfo(out Vector3 position, out Vector3 normal, out int positionInLine, out bool isValidTarget);

        leftTeleportation.SetActive(!isLeftRayHovering && leftCancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1);

        isRightRayHovering = rightRay.TryGetHitInfo(out Vector3 r_position, out Vector3 r_normal, out int r_positionInLine, out bool r_isValidTarget);

        rightTeleportation.SetActive(!isRightRayHovering && rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1);
    }

    public void OnOffGrabRay()
    {
        // Left
        if (!leftTeleportation.activeSelf)
        {
            leftLineVisual.enabled = true;
            //leftRay.interactionLayerMask = LayerMask.NameToLayer("Ray Interaction");
        }
        else
        {
            leftLineVisual.enabled = false;
            //leftRay.interactionLayerMask = LayerMask.NameToLayer("Nothing");
        }

        // Right
        if (!rightTeleportation.activeSelf)
        {
            rightLineVisual.enabled = true;
            //rightRay.interactionLayerMask = LayerMask.NameToLayer("Ray Interaction");
        }
        else
        {
            rightLineVisual.enabled= false;
            //rightRay.interactionLayerMask = LayerMask.NameToLayer("Nothing");
        }
    }
}
