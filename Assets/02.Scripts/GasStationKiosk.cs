using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GasStationKiosk : Singleton<GasStationKiosk>
{
    #region field
    [Header("키오스크")]
    [SerializeField] private GameObject[] kioskPanelArr = null;
    public int kioskPanelIndex = 0;
    public Collider cardReaderCol = null;
    public Collider card = null;

    [Header("주유타입")]
    public int gasolinePrice = 1739;
    public int dissel = 1659;
    public int h_gasolinePrice = 1959;
    public float oilInputTimePerLitter = 0;
    public float oilInputTime = 1;
    public float lastPrice = 0;
    IEnumerator inputOil = null;

    [Header("숫자입력")]
    [SerializeField] private TextMeshProUGUI numberText = null;
    [SerializeField] private TextMeshProUGUI toggleTypeText = null;
    public int resultNumber = 0;

    public OilType oilType = OilType.Gasoline;
    public enum OilType
    {
        Gasoline = 0,
        Diesel,
        HGasoline
    }

    public ToggleType toggleType = ToggleType.Won;
    public enum ToggleType
    {
        Man,
        Won,
        Litter
    }
    #endregion

    private void Awake()
    {
        inputOil = CoInputOil();
    }

    /// <summary>
    /// 0 => 휘발유
    /// 1 => 경유
    /// 2 => 고급휘발유
    /// </summary>
    /// <param name="new_oilType"></param>
    public void SetOilType(int new_oilType)
    {
        switch (new_oilType)
        {
            case 0:
                oilType = OilType.Gasoline;
                break;
            case 1:
                oilType = OilType.Diesel;
                break;
            case 2:
                oilType = OilType.HGasoline;
                break;
        }
    }

    public void SetPaymentType(int new_paymentType)
    {
        switch (new_paymentType)
        {
            case 0:
                toggleType = ToggleType.Man;
                toggleTypeText.text = "만원";
                break;
            case 1:
                toggleType = ToggleType.Won;
                toggleTypeText.text = "원";
                break;
            case 2:
                toggleType = ToggleType.Litter;
                toggleTypeText.text = "리터";
                break;
        }
    }

    // 주유 시작
    public void StartInputOil()
    {
        StartCoroutine(inputOil);
    }

    // 키오스크 패널 전환
    public void OnOffKioskPanel(int dir)
    {
        kioskPanelArr[kioskPanelIndex].SetActive(false);
        kioskPanelIndex += dir;
        kioskPanelArr[kioskPanelIndex].SetActive(true);
        if(kioskPanelIndex == 5)
        {
            cardReaderCol.enabled = true;
        }
        else if(kioskPanelIndex == 6)
        {
            StartCoroutine(ChangePanelSequence());
            cardReaderCol.enabled = false;
            card.enabled = false;
        }
        else if(kioskPanelIndex == 10)
        {
            cardReaderCol.enabled = true;
            card.enabled = true;
        }
    }

    // 숫자 선택, 확인 버튼
    public void ChooseNumber(int number = -1)
    {
        if (number != -1)
        {
            resultNumber = number;
        }
        else
        {
            resultNumber = int.Parse(numberText.text);
        }
    }

    // 숫자 입력
    public void ClickNumber(int number = -1)
    {
        if (number != -1)
        {
            numberText.text = numberText.text + number;
        }
        else
        {
            numberText.text = string.Empty;
        }
    }

    // 금액 계산
    public void CalPayment()
    {
        switch (toggleType)
        {
            case ToggleType.Won:

                break;
            case ToggleType.Litter:
                CheckOilTypeAndCalResultNum();
                break;
            case ToggleType.Man:
                resultNumber = resultNumber * 10000;
                break;
        }
    }

    // 오일 타입 별 금액 재정의
    public void CheckOilTypeAndCalResultNum()
    {
        switch (oilType)
        {
            case OilType.Diesel:
                resultNumber = resultNumber * dissel;
                break;
            case OilType.Gasoline:
                resultNumber = resultNumber * gasolinePrice;
                break;
            case OilType.HGasoline:
                resultNumber = resultNumber * h_gasolinePrice;
                break;
        }
    }

    // 결제금액 확정
    public void CalLastPrice(bool isCalWithAmount)
    {
        if (!isCalWithAmount)
        {
            lastPrice = resultNumber - resultNumber % 1000;
        }
        else
        {
            switch (oilType)
            {
                case OilType.Diesel:
                    lastPrice = resultNumber - resultNumber % dissel;
                    break;
                case OilType.Gasoline:
                    lastPrice = resultNumber - resultNumber % gasolinePrice;
                    break;
                case OilType.HGasoline:
                    lastPrice = resultNumber - resultNumber % h_gasolinePrice;
                    break;
            }
        }
    }

    // 주유 시간 계산
    public void CalOilInputTime()
    {
        switch (oilType)
        {
            case OilType.Diesel:
                oilInputTime = resultNumber / dissel;
                break;
            case OilType.Gasoline:
                oilInputTime = resultNumber / gasolinePrice;
                break;
            case OilType.HGasoline:
                oilInputTime = resultNumber / h_gasolinePrice;
                break;
        }

        oilInputTime = oilInputTime * oilInputTimePerLitter;
    }

    private IEnumerator ChangePanelSequence()
    {
        yield return new WaitForSeconds(5);
        OnOffKioskPanel(1);
    }

    // 주유
    private IEnumerator CoInputOil()
    {
        Debug.Log(oilInputTime);
        OnOffKioskPanel(1);
        yield return new WaitForSeconds(5);
        OnOffKioskPanel(1);
        StopCoroutine(inputOil);
        inputOil = null;
        inputOil = CoInputOil();
    }
}
