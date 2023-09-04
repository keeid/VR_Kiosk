using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KioskUI : MonoBehaviour
{
    public static KioskUI instance;

    [Header("Ű����ũ ȭ��")]
    [SerializeField] private GameObject[] kioskPanelArr = null; // Ű����ũ ȭ�� ����
    public int kioskPanelIndex = 0;    // ���� Ű����ũ ȭ�� �ε���

    [Header("��ȣ�ۿ� ���� �� ����Ǵ� ��� ���")]
    [SerializeField] private Text warningTxt = null; // ��� �ؽ�Ʈ
    IEnumerator CancelKioskSimulatorCo;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
    }

    // ���� �ʱ�ȭ
    private void Init()
    {
        kioskPanelIndex = 0;
    }

    // Ű����ũ ȭ�� ����
    // �Ű����� dir -> ���� ȭ�� -1, ���� ȭ�� +1
    public void ChangeKioskPage(int dir)
    {
        kioskPanelArr[kioskPanelIndex].SetActive(false);
        kioskPanelIndex += dir;
        PanelFuncInit(kioskPanelIndex);
        kioskPanelArr[kioskPanelIndex].SetActive(true);
    }

    public enum ScreenType
    {
        MainPanel = 0,
        ProductPanel,
        PaymentPanel,
        RetrievePanel,
        CompletePanel
    }

    public void PanelFuncInit(int index)
    {
        switch (index)
        {
            case (int)ScreenType.PaymentPanel:
                CardReader.instance.OnOffCardReaderCollider(true);
                break;
            case (int)ScreenType.CompletePanel:
                CardReader.instance.OnOffCardReaderCollider(false);
                break;
            default:
                break;
        }
    }

    // �ڷ�ƾ ����
    private void StartCancelKioskSimulatorCo()
    {
        StopCoroutine(CancelKioskSimulatorCo);
        CancelKioskSimulatorCo = null;
        CancelKioskSimulatorCo = CancelKioskSimulator();
        StartCoroutine(CancelKioskSimulatorCo);
    }

    // �ƹ� ��ȣ�ۿ��� ������ ��� �� �ʱ�ȭ������ ���ư���
    private IEnumerator CancelKioskSimulator()
    {
        yield return new WaitForSeconds(10f);
        // 10�ʰ� ������ ��� ǥ��
        warningTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(10f);
        // 10�ʰ� �� ������ �ʱ�ȭ������ ���ư���
        kioskPanelArr[kioskPanelIndex].SetActive(false);
        kioskPanelIndex = 0;
        kioskPanelArr[kioskPanelIndex].SetActive(true);
    }
}
