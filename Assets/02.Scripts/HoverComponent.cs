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
    public GameObject leftGrabRay = null;
    public GameObject rightGrabRay = null;

    [Header("Teleport")]
    [SerializeField] private XRRayInteractor leftTpXr = null;
    [SerializeField] private XRRayInteractor rightTpXr = null;
    [SerializeField] private XRInteractorLineVisual leftTpLine;
    [SerializeField] private XRInteractorLineVisual rightTpLine;

    private void Start()
    {

        // Grab Ray
        leftXr.onHoverEntered.AddListener((leftXr) => { OnHoverEnter(leftXr, leftLine); });
        rightXr.onHoverEntered.AddListener((rightXr) => { OnHoverEnter(rightXr, rightLine); });

        leftXr.onHoverExited.AddListener((leftXr) => { OnHoverExit(leftXr, leftLine); });
        rightXr.onHoverExited.AddListener((rightXr) => { OnHoverExit(rightXr, rightLine); });

    }

    private void OnHoverEnter(XRBaseInteractable interactor, XRInteractorLineVisual line)
    {
        try
        {
            interactor.GetComponentsInChildren<Collider>().First().GetComponent<OutLineComponenet>().OnOutline();
        }
        catch (NullReferenceException ex)
        {
            //Debug.LogException(ex);
        }
    }


    private void OnHoverExit(XRBaseInteractable interactor, XRInteractorLineVisual line)
    {
        interactor.GetComponentsInChildren<Collider>().First().GetComponent<OutLineComponenet>().OffOutline();
    }

    public void OnGrabRay(GameObject go)
    {
        go.SetActive(true);
    }

    public void OffGrabRay(GameObject go)
    {
        go.SetActive(false);
    }
}
