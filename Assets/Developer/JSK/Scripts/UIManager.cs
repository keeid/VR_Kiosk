using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager instance = null;
    public static UIManager Instance
    {
        get { return instance; }
    }

    #endregion
    public Transform MainMissionPanel;
    public Text MainMissionText;
    public Transform ProductPanel;
    public Text[] productTexts;
    public Transform GuidePanel;
    public Text GuideText;
    public Text timeText;

    private IEnumerator ActiveCor;
    private IEnumerator UnactiveCor;

    //Time
    private float minutes = 0f;
    private float seconds = 0f;
    private float unactiveSec = 0.5f;
    
    private string timeString;
    
    public bool isTaskInfo = false;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        MainMissionPanel.gameObject.SetActive(true);
        UnActivePanel(MainMissionPanel, 0.2f);
        ActivePanel(ProductPanel, true, unactiveSec);
        SettingProductPanel();
    }
    private void Update()
    {
        UpdatePrdouctPanel();
        SettingProductPanel();

        if (isTaskInfo)
        {
            isTaskInfo = false;
            SetTaskInfo();
            ActivePanel(MainMissionPanel, false, unactiveSec);
        }
        SetTimeText();
    }

    private void SetTimeText()
    {
        minutes = Mathf.FloorToInt(MissionManager.Instance.Timer / 60);
        seconds = Mathf.FloorToInt(MissionManager.Instance.Timer % 60);

        timeString = string.Format("{0:00}:{1:00}", minutes, seconds);

        timeText.text = timeString;
    }
    #region ProductPanel
    private void SettingProductPanel()
    {
        for (int i = 0; i < MissionManager.Instance.typeCnt; i++)
        {
            productTexts[i].gameObject.SetActive(true);
            productTexts[i].text = $"{MissionManager.Instance.missionProducts[i].productName} : " +
                $"{MissionManager.Instance.currentProductCnt[i]} / {MissionManager.Instance.missionProductCnt[i]}";
        }

    }
    private void UpdatePrdouctPanel()
    {
    }
    #endregion
    
    #region Active & Unactive Panel
    private void ActivePanel(Transform targetTr, bool sustainable, float sec)
    {
        if (ActiveCor != null)
        {
            StopCoroutine(ActiveCor);
        }
        ActiveCor = CorActive(targetTr, sustainable, sec);
        StartCoroutine(ActiveCor);
    }
    private void UnActivePanel(Transform targetTr, float sec)
    {
        if (UnactiveCor != null)
        {
            StopCoroutine(UnactiveCor);
        }
        UnactiveCor = CorUnactive(targetTr, sec);
        StartCoroutine(UnactiveCor);
    }
    public IEnumerator CorActive(Transform targetTr, bool sustainable, float sec)
    {
        yield return new WaitForSeconds(sec);
        targetTr.gameObject.SetActive(true);
        if (!sustainable)
        {
            yield return unactiveSec;
            targetTr.gameObject.SetActive(false);
        }
        else
        {
            StopCoroutine(ActiveCor);
        }
    }
    public IEnumerator CorUnactive(Transform targetTr, float sec)
    {
        yield return new WaitForSeconds(sec);
        targetTr.gameObject.SetActive(false);

    }

    #endregion

    #region TaskInfo
    private void SetTaskInfo()
    {
        switch (MissionManager.Instance.Step)
        {
            case 0:
                ActivePanel(MainMissionPanel, false, 0.5f);
                MainMissionText.text = "상품을 바구니에 담으세요.";
                break;

            case 1:
                ActivePanel(MainMissionPanel, false, 0.5f);
                UnActivePanel(ProductPanel, 0f);
                MainMissionText.text = $"{MissionManager.Instance.KioskNum}번 계산대 이동 하세요.";
                break;

            case 2:
                ActivePanel(MainMissionPanel, false, 0.5f);
                MainMissionText.text = "장바구니 올리기.";
                break;

            case 3:
                SettingProductPanel();
                ActivePanel(MainMissionPanel, false, 0.5f);
                ActivePanel(ProductPanel, true, unactiveSec);
                MainMissionText.text = "상품 스캔하기.";
                break;

            case 4:
                ActivePanel(MainMissionPanel, false, 0.5f);
                MainMissionText.text = "상품 결제하기.";
                break;
                
            case 5:
                ActivePanel(MainMissionPanel, true, 0.5f);
                MainMissionText.text = "미션성공";
                break;
        }
    }
    #endregion

    #region Guide

    public void ShowGuide(string context)
    {
        GuideText.text = context;
        ActivePanel(GuidePanel, false, unactiveSec);
    }
    #endregion

}
