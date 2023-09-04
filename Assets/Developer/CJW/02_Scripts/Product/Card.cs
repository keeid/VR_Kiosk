using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Card : XRGrabInteractable
{
    public Transform rightAttachTransform;
    private Rigidbody rbCard;

    private void Start()
    {
        rbCard = GetComponent<Rigidbody>();
    }

    private void PutCardRightHand()
    {
        attachTransform = rightAttachTransform;
        //rbCard.isKinematic = true;
    }
}
