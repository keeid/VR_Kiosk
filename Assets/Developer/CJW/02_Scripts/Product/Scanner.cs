using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Scanner : MonoBehaviour
{
    private void Start()
    {
        XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
        grabbable.activated.AddListener(ProductRecognition);
    }

    public void ProductRecognition(ActivateEventArgs arg)
    {
        Debug.Log("��ĳ�� �ν� �����մϴ�.");

        // ��ǰ ��ĵ ������ �Լ�(Ű����ũ �Լ� ȣ��)
    }
}