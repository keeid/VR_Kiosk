using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardReader : MonoBehaviour
{
    public static CardReader instance;
    private void Awake()
    {
        instance = this;
    }

    /*private void OnTriggerStay(Collider other)
    {
        var product = other.GetComponent<Product>();
        if (product != null)
        {
            if (!product.isGrabed && product.productInfo.productName == "카드")
            {
                InsertCard(other);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var product = other.GetComponent<Product>();
        if (product != null)
        {
            if (product.productInfo.productName == "카드")
            {
                // 수정_cjw
                other.isTrigger = false;
                RetrieveCard(other);
            }
        }
    }
*/
    public void OnOffCardReaderCollider(bool enable)
    {
        transform.GetComponent<Collider>().enabled = enable;
    }

    // 카드 넣기
    public void InsertCard()
    {
        KioskUI.instance.ChangeKioskPage(1);
        if (transform.childCount == 0)
        {
            Debug.Log("1");/*
            col.isTrigger = true;
            col.gameObject.transform.parent = this.transform;
            col.GetComponent<Rigidbody>().isKinematic = true;
            col.gameObject.transform.localPosition = Vector3.zero;
            col.gameObject.transform.localRotation = Quaternion.identity;*/
            // 결제 진행 중입니다 표시
            KioskUI.instance.ChangeKioskPage(1);
            //cardObj.transform.DOMoveZ(-0.05f, 3f).SetEase(Ease.Linear);
        }
    }

    // 카드 회수
    public void RetrieveCard()
    {
        KioskUI.instance.ChangeKioskPage(1);

        if (KioskUI.instance.kioskPanelIndex == 3 && transform.childCount == 0)
        {
            Debug.Log("2");
        }
        /*cardObj.GetComponent<Collider>().isTrigger = false;
        cardObj.transform.DOMoveZ(0.05f, 3f).SetEase(Ease.Linear);*/
    }
}
