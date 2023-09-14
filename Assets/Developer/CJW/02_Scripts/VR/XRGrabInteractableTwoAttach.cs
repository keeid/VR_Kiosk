using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Linq;

public class XRGrabInteractableTwoAttach : XRGrabInteractable
{
    [SerializeField] private HapticComponent hapticComponent = null;
    [SerializeField] private HoverComponent hoverComponent = null;
    public GameObject leftAttachTransform;
    public GameObject rightAttachTransform;

    public bool isLeftGrabProduct;
    public bool isRightGrabProduct;

    private void Start()
    {
        hapticComponent = FindObjectOfType<HapticComponent>();
        hoverComponent = FindObjectOfType<HoverComponent>();
    }


    // 상품을 잡은 경우
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("Left Hand"))
        {
            isLeftGrabProduct = true;
            attachTransform = leftAttachTransform.transform;
            hapticComponent.ActivateHaptic(HapticComponent.ControllerType.Left);
           
            Debug.Log($"잡은 상품은 : {leftAttachTransform.transform.parent.name} 입니다.");
        }
        else if (args.interactorObject.transform.CompareTag("Right Hand"))
        {
            isRightGrabProduct = true;
            attachTransform = rightAttachTransform.transform;
            hapticComponent.ActivateHaptic(HapticComponent.ControllerType.Right);

            Debug.Log($"잡은 상품은 : {rightAttachTransform.transform.parent.name} 입니다.");
        }
        CheckGrabProduct();

        base.OnSelectEntered(args);
    }

    // 상품을 놓는 경우
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("Left Hand"))
        {
            isLeftGrabProduct = false;
            leftAttachTransform.transform.parent.GetComponent<Rigidbody>().isKinematic = false;

            Debug.Log($"놓친 상품은 : {leftAttachTransform.transform.parent.name} 입니다.");
        }
        else if (args.interactorObject.transform.CompareTag("Right Hand"))
        {
            isRightGrabProduct = false;
            rightAttachTransform.transform.parent.GetComponent<Rigidbody>().isKinematic = false;

            Debug.Log($"놓친 상품은 : {rightAttachTransform.transform.parent.name} 입니다.");
        }
        CheckGrabProduct();

        base.OnSelectExited(args);
    }

    // 플레이어가 상품을 잡고 있는지 확인 및 자식 오브젝트 해제
    private void CheckGrabProduct()
    {
        if (rightAttachTransform != null || leftAttachTransform != null)
        {
            Transform tr = rightAttachTransform.transform ? rightAttachTransform.transform : leftAttachTransform.transform;
            CheckObjectType(tr);
        }
    }

    private void CheckObjectType(Transform tr)
    {
        var product = tr.parent.GetComponent<Product>();
        switch (product.productInfo.objType)
        {
            case Define.ObjectType.nonProduct:
                product.isGrabed = !product.isGrabed;
                break;
            case Define.ObjectType.Product:
                product.isGrabed = !product.isGrabed;
                product.isInCart = false;
                if (product.isInCart == false)
                {
                    Debug.Log("거지");
                    //MissionManager.Instance.RemoveProduct(product.GetComponent<Collider>());
                    tr.parent.parent = null;
                }
                break;
            default:
                Debug.Log("예외입니다");
                break;
        }
    }
}
