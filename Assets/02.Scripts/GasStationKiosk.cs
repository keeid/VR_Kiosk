using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GasStationKiosk : MonoBehaviour
{
    [Header("Ű����ũ")]
    [SerializeField] private GameObject[] kioskPanelArr = null;
    public int kioskPanelIndex = 0;

    [Header("�����Է�")]
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
    // Ű����ũ �г� ��ȯ
    public void OnOffKioskPanel(int dir)
    {
        kioskPanelArr[kioskPanelIndex].SetActive(false);
        kioskPanelIndex += dir;
        kioskPanelArr[kioskPanelIndex].SetActive(true);
    }

    // ���� ����, Ȯ�� ��ư
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

    // ���� �Է�
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

    public void CalPayment()
    {
        switch (toggleType)
        {
            case ToggleType.Won:

                break;
            case ToggleType.Litter:

                break;
            case ToggleType.Man:

                break;
        }
    }

}
