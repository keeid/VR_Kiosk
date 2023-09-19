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

    [Header("Grab")]
    [SerializeField] private XRRayInteractor leftXr;
    [SerializeField] private XRRayInteractor rightXr;
    [SerializeField] private XRInteractorLineVisual leftLine;
    [SerializeField] private XRInteractorLineVisual rightLine;
    public GameObject leftGrabRay = null;
    public GameObject rightGrabRay = null;
    public AudioSource hoverAudio = null;

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
            hoverAudio.PlayOneShot(hoverAudio.clip);
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
}
