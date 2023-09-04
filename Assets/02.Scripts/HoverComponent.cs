using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Linq;
using System;
using UnityEngine.UI;

public class HoverComponent : MonoBehaviour
{
    [SerializeField] private XRRayInteractor leftXr;
    [SerializeField] private XRRayInteractor rightXr;
    [SerializeField] private XRInteractorLineVisual leftLine;
    [SerializeField] private XRInteractorLineVisual rightLine;
    [SerializeField] private Gradient enterGradient = null;
    [SerializeField] private Gradient exitGradient = null;
    [SerializeField] private Gradient alphaGradient = null;

    private void Start()
    {
        leftXr.onHoverEntered.AddListener((leftXr) => { OnHoverEnter(leftXr, leftLine); });
        rightXr.onHoverEntered.AddListener((rightXr) => { OnHoverEnter(rightXr, rightLine);  });
        
        leftXr.onHoverExited.AddListener((leftXr) => { OnHoverExit(leftXr, leftLine); });
        rightXr.onHoverExited.AddListener((rightXr) => { OnHoverExit(rightXr, rightLine); });

        leftXr.onSelectEntered.AddListener((leftXr) => { OnSelectedEnter(leftXr, leftLine); });
        rightXr.onSelectEntered.AddListener((rightXr) => { OnSelectedEnter(rightXr, rightLine);  });
        
        leftXr.onSelectExited.AddListener((leftXr) => { OnSelectedExit(leftXr, leftLine); });
        rightXr.onSelectExited.AddListener((rightXr) => { OnSelectedExit(rightXr, rightLine);  });
    }
   
    private void OnSelectedEnter(XRBaseInteractable interactor, XRInteractorLineVisual line)
    {
        line.invalidColorGradient = alphaGradient;
    }

    private void OnSelectedExit(XRBaseInteractable interactor, XRInteractorLineVisual line)
    {
        line.invalidColorGradient = exitGradient;
    }

    private void OnHoverEnter(XRBaseInteractable interactor, XRInteractorLineVisual line)
    {
        //Debug.Log(interactor.GetComponentsInChildren<Collider>().First().name);
        try
        {
            if(interactor.GetComponent<Button>() != null)
            {
                Debug.Log("button");
            }
            
            
            interactor.GetComponentsInChildren<Collider>().First().GetComponent<OutLineComponenet>().OnOutline();
        }
        catch(NullReferenceException ex)
        {
            Debug.LogException(ex);
        }
        if (!interactor.isSelected)
        {
            line.validColorGradient = enterGradient;
        }
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
