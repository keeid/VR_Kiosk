using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandOnInput : MonoBehaviour
{
    private Animator handAnimator;

    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;
    

    private void Awake()
    {
        handAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandAnimation();
    }

    // 손 애니메이션
    public void HandAnimation()
    {
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}
