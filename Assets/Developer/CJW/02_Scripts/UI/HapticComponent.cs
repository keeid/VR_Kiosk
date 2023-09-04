using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HapticComponent : MonoBehaviour
{
    [SerializeField] private ActionBasedController leftXr;
    [SerializeField] private ActionBasedController rightXr;

    public enum ControllerType
    {
        Left,
        Right
    }

    // ���� �Լ�(������ ����, ���� �ð�)
    public void ActivateHaptic(ControllerType controllerType)
    {
        switch (controllerType)
        {
            case ControllerType.Left:
                leftXr.SendHapticImpulse(0.7f, 0.5f);
                break;
            case ControllerType.Right:
                rightXr.SendHapticImpulse(0.7f, 0.5f);
                break;
        }
    }
}
