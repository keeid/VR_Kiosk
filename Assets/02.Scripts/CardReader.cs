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

    private void Start()
    {
        this.GetComponent<Collider>().enabled = false;
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

    // 무인매장 키오스크 카드 회수
    public void RetrieveCard()
    {
        KioskUI.instance.ChangeKioskPage(1);

        if (KioskUI.instance.kioskPanelIndex == 3 && transform.childCount == 0)
        {
            Debug.Log("2");
        }
        MissionManager.Instance.NextStep();
        /*cardObj.GetComponent<Collider>().isTrigger = false;
        cardObj.transform.DOMoveZ(0.05f, 3f).SetEase(Ease.Linear);*/
    }

    // 주유소 키오스크 카드 회수
    public void GasStationInsertCard()
    {
        GasStationKiosk.Instance.OnOffKioskPanel(1);
    }

    // 주유소 키오스크 카드 회수
    public void GasStationRetrieveCard()
    {
        GasStationKiosk.Instance.OnOffKioskPanel(1);
    }
}
