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
        Debug.Log("스캐너 인식 가능합니다.");

        // 상품 스캔 가능한 함수(키오스크 함수 호출)
    }
}