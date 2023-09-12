using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Linq;
using System;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class HoverComponent : MonoBehaviour
{
    public static HoverComponent instance;

    private void Awake()
    {
        instance = this;
    }

    public ActionBasedContinuousMoveProvider provider = null;

    [Header("Grab")]
    [SerializeField] private XRRayInteractor leftXr;
    [SerializeField] private XRRayInteractor rightXr;
    [SerializeField] private XRInteractorLineVisual leftLine;
    [SerializeField] private XRInteractorLineVisual rightLine;
    [SerializeField] private Gradient enterGradient = null;
    [SerializeField] private Gradient exitGradient = null;
    [SerializeField] private Gradient alphaGradient = null;

    [Header("Teleport")]
    [SerializeField] private XRRayInteractor leftTpXr = null;
    [SerializeField] private XRRayInteractor rightTpXr = null;
    [SerializeField] private XRInteractorLineVisual leftTpLine;
    [SerializeField] private XRInteractorLineVisual rightTpLine;

    private void Start()
    {
        // TP Ray
        leftTpXr.onHoverEntered.AddListener((leftXr) => { SetRayAlphaGradient(leftXr, leftLine); });
        rightTpXr.onHoverEntered.AddListener((rightXr) => { SetRayAlphaGradient(rightXr, rightLine); });
        leftTpXr.onHoverExited.AddListener((leftXr) => { SetRayExitGradient(leftXr, leftLine); });
        rightTpXr.onHoverExited.AddListener((rightXr) => { SetRayExitGradient(rightXr, rightLine); });


        // Grab Ray
        leftXr.onHoverEntered.AddListener((leftXr) => { OnHoverEnter(leftXr, leftLine); });
        rightXr.onHoverEntered.AddListener((rightXr) => { OnHoverEnter(rightXr, rightLine); });

        leftXr.onHoverExited.AddListener((leftXr) => { OnHoverExit(leftXr, leftLine); });
        rightXr.onHoverExited.AddListener((rightXr) => { OnHoverExit(rightXr, rightLine); });

        leftXr.onSelectEntered.AddListener((leftXr) => { SetRayAlphaGradient(leftXr, leftLine, true); });
        rightXr.onSelectEntered.AddListener((rightXr) => { SetRayAlphaGradient(rightXr, rightLine, true); });

        leftXr.onSelectExited.AddListener((leftXr) => { SetRayExitGradient(leftXr, leftLine, true); });
        rightXr.onSelectExited.AddListener((rightXr) => { SetRayExitGradient(rightXr, rightLine, true); });
    }

    public void SetRayAlphaGradient(XRBaseInteractable interactor, XRInteractorLineVisual line, bool isSelected = false)
    {
        Debug.Log(isSelected);
        line.invalidColorGradient = alphaGradient;
        provider.leftHandMoveAction.DisableDirectAction();
        if (isSelected)
        {
            Debug.Log(isSelected);
            //provider.leftHandMoveAction.DisableDirectAction();
        }
    }

    public void SetRayExitGradient(XRBaseInteractable interactor, XRInteractorLineVisual line, bool isSelected = false)
    {
        line.invalidColorGradient = exitGradient;
        if (isSelected)
        {
            provider.leftHandMoveAction.EnableDirectAction();
        }
    }

    private void OnHoverEnter(XRBaseInteractable interactor, XRInteractorLineVisual line)
    {
        try
        {
            if (interactor.GetComponentsInChildren<Button>() != null)
            {
                Debug.Log("button");
            }

            interactor.GetComponentsInChildren<Collider>().First().GetComponent<OutLineComponenet>().OnOutline();
        }
        catch (NullReferenceException ex)
        {
            //Debug.LogException(ex);
        }
        if (!interactor.isSelected)
        {
            line.validColorGradient = enterGradient;
        }
        Debug.Log(interactor.GetComponentsInChildren<Collider>().First().name);
    }


    private void OnHoverExit(XRBaseInteractable interactor, XRInteractorLineVisual line)
    {
        //Debug.Log(interactor.GetComponentsInChildren<Collider>().First().name);
        interactor.GetComponentsInChildren<Collider>().First().GetComponent<OutLineComponenet>().OffOutline();
        if (!interactor.isSelected)
        {
            line.validColorGradient = exitGradient;
        }
    }
}
