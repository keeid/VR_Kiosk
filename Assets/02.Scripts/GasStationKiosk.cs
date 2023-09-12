using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GasStationKiosk : MonoBehaviour
{
    #region field
    [Header("키오스크")]
    [SerializeField] private GameObject[] kioskPanelArr = null;
    public int kioskPanelIndex = 0;

    [Header("주유타입")]
    public int gasolinePrice = 1739;
    public int dissel = 1659;
    public int h_gasolinePrice = 1959;
    public float oilInputTimePerLitter = 0;
    public float oilInputTime = 0;
    public float lastPrice = 0;
    IEnumerator inputOil = null;

    [Header("숫자입력")]
    [SerializeField] private TextMeshProUGUI numberText = null;
    public int resultNumber = 0;

    public OilType oilType = OilType.Gasoline;
    public enum OilType
    {
        Gasoline,
        Dissel,
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
    }

    // 숫자 선택, 확인 버튼
    public void ChooseNumber(int number = default)
    {
        if (number != default)
        {
            resultNumber = number;
        }
        else
        {
            resultNumber = int.Parse(numberText.text);
        }
    }

    // 숫자 입력
    public void ClickNumber(int number = default)
    {
        if (number != default)
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
            case OilType.Dissel:
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
                case OilType.Dissel:
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
            case OilType.Dissel:
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

    // 주유
    private IEnumerator CoInputOil()
    {
        yield return new WaitForSeconds(oilInputTime);
        OnOffKioskPanel(1);
        StopCoroutine(inputOil);
        inputOil.Reset();
    }
}
