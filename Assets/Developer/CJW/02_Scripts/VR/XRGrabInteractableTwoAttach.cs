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


    // ��ǰ�� ���� ���
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("Left Hand"))
        {
            isLeftGrabProduct = true;
            attachTransform = leftAttachTransform.transform;
            hapticComponent.ActivateHaptic(HapticComponent.ControllerType.Left);
           
            Debug.Log($"���� ��ǰ�� : {leftAttachTransform.transform.parent.name} �Դϴ�.");
        }
        else if (args.interactorObject.transform.CompareTag("Right Hand"))
        {
            isRightGrabProduct = true;
            attachTransform = rightAttachTransform.transform;
            hapticComponent.ActivateHaptic(HapticComponent.ControllerType.Right);

            Debug.Log($"���� ��ǰ�� : {rightAttachTransform.transform.parent.name} �Դϴ�.");
        }
        CheckGrabProduct();

        base.OnSelectEntered(args);
    }

    // ��ǰ�� ���� ���
    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactorObject.transform.CompareTag("Left Hand"))
        {
            isLeftGrabProduct = false;
            leftAttachTransform.transform.parent.GetComponent<Rigidbody>().isKinematic = false;

            Debug.Log($"��ģ ��ǰ�� : {leftAttachTransform.transform.parent.name} �Դϴ�.");
        }
        else if (args.interactorObject.transform.CompareTag("Right Hand"))
        {
            isRightGrabProduct = false;
            rightAttachTransform.transform.parent.GetComponent<Rigidbody>().isKinematic = false;

            Debug.Log($"��ģ ��ǰ�� : {rightAttachTransform.transform.parent.name} �Դϴ�.");
        }
        CheckGrabProduct();

        base.OnSelectExited(args);
    }

    // �÷��̾ ��ǰ�� ��� �ִ��� Ȯ�� �� �ڽ� ������Ʈ ����
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
                    Debug.Log("����");
                    //MissionManager.Instance.RemoveProduct(product.GetComponent<Collider>());
                    tr.parent.parent = null;
                }
                break;
            default:
                Debug.Log("�����Դϴ�");
                break;
        }
    }
}
