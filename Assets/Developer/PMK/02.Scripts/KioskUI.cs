using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KioskUI : MonoBehaviour
{
    public static KioskUI instance;

    [Header("키오스크 화면")]
    [SerializeField] private GameObject[] kioskPanelArr = null; // 키오스크 화면 모음
    public int kioskPanelIndex = 0;    // 현재 키오스크 화면 인덱스

    [Header("상호작용 없을 때 실행되는 경고 기능")]
    [SerializeField] private Text warningTxt = null; // 경고 텍스트
    IEnumerator CancelKioskSimulatorCo;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Init();
    }

    // 변수 초기화
    private void Init()
    {
        kioskPanelIndex = 0;
    }

    // 키오스크 화면 변경
    // 매개변수 dir -> 이전 화면 -1, 다음 화면 +1
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

    // 코루틴 실행
    private void StartCancelKioskSimulatorCo()
    {
        StopCoroutine(CancelKioskSimulatorCo);
        CancelKioskSimulatorCo = null;
        CancelKioskSimulatorCo = CancelKioskSimulator();
        StartCoroutine(CancelKioskSimulatorCo);
    }

    // 아무 상호작용이 없으면 경고 후 초기화면으로 돌아가기
    private IEnumerator CancelKioskSimulator()
    {
        yield return new WaitForSeconds(10f);
        // 10초가 지나면 경고 표시
        warningTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(10f);
        // 10초가 또 지나면 초기화면으로 돌아가기
        kioskPanelArr[kioskPanelIndex].SetActive(false);
        kioskPanelIndex = 0;
        kioskPanelArr[kioskPanelIndex].SetActive(true);
    }
}
