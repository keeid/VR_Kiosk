using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    public RectTransform MainMissionPanel;
    public Text MainMissionText;
    public RectTransform ProductPanel;
    public Text[] productTexts;
    public Transform GuidePanel;
    public Text GuideText;
    public Text timeText;

    private IEnumerator ActiveCor;
    private IEnumerator ActiveCor2;
    private IEnumerator UnactiveCor;

    private Vector3 cameraPos;

    private Vector3 headPos;

    public Transform playerTr;
    //Time
    private float minutes = 0f;
    private float seconds = 0f;
    private float unactiveSec = 2f;

    private string timeString;

    public bool isTaskInfo = true;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        isTaskInfo = true;
        isTaskInfo = true;
        SettingProductPanel();
        UpdatePos();
    }
    private void UpdatePos()
    {
        cameraPos = Camera.main.transform.position;
        headPos = cameraPos + new Vector3(Camera.main.transform.forward.x, 0.3f, Camera.main.transform.forward.z).normalized * 2f;
    }
    private void Update()
    {
        UpdatePrdouctPanel();
        SettingProductPanel();

        if (isTaskInfo)
        {
            Debug.Log("인포트루");
            isTaskInfo = false;
            SetTaskInfo();
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
    private void ActivePanel(RectTransform targetTr, bool sustainable, bool fixIntoPlayer, Vector3 pos, float sec)
    {
        if (ActiveCor != null)
        {
            StopCoroutine(ActiveCor);
        }
        ActiveCor = CorActive(targetTr, sustainable, fixIntoPlayer, pos, sec);
        StartCoroutine(ActiveCor);
    }
    private void ActivePanel2(RectTransform targetTr, bool sustainable, bool fixIntoPlayer, Vector3 pos, float sec)
    {
        Debug.Log('s');
        if (ActiveCor2 != null)
        {
            StopCoroutine(ActiveCor2);
        }
        ActiveCor2 = CorActive2(targetTr, sustainable, fixIntoPlayer, pos, sec);
        StartCoroutine(ActiveCor2);
    }
    private void UnActivePanel(RectTransform targetTr, float sec)
    {
        if (UnactiveCor != null)
        {
            StopCoroutine(UnactiveCor);
        }
        UnactiveCor = CorUnactive(targetTr, sec);
        StartCoroutine(UnactiveCor);
    }
    public IEnumerator CorActive(RectTransform targetTr, bool sustainable, bool fixIntoPlayer, Vector3 pos, float sec)
    {
        yield return new WaitForSeconds(0.5f);
        targetTr.gameObject.SetActive(true);
        if (!sustainable)
        {
            if (fixIntoPlayer)
            {
                float tt = Time.time;
                while (Time.time <= tt + sec)
                {
                    targetTr.position = pos;
                    yield return null;
                }
                targetTr.gameObject.SetActive(false);
            }
            else
            {
                targetTr.position = pos;
                yield return new WaitForSeconds(sec);
                targetTr.gameObject.SetActive(false);
            }
        }
        else
        {
            if (fixIntoPlayer)
            {
                while (true)
                {
                    targetTr.position = pos;
                    yield return null;
                }
            }
        }
    }

    public Vector3 productPanelPos = default;

    public IEnumerator CorActive2(RectTransform targetTr, bool sustainable, bool fixIntoPlayer, Vector3 pos, float sec)
    {
        yield return new WaitForSeconds(0.5f);
        targetTr.gameObject.SetActive(true);

        if (!sustainable)
        {
            if (fixIntoPlayer)
            {
                float tt = Time.time;
                while (Time.time <= tt + sec)
                {
                    Debug.Log('2');
                    targetTr.position = pos;
                    yield return null;
                }
                targetTr.gameObject.SetActive(false);
            }
            else
            {
                targetTr.position = pos;
                yield return new WaitForSeconds(sec);
                targetTr.gameObject.SetActive(false);
            }
        }
        else
        {
            if (fixIntoPlayer)
            {
                while (true)
                {
                    var newPose = Camera.main.transform.position + Camera.main.transform.TransformDirection(Vector3.zero) + Camera.main.transform.TransformVector(productPanelPos);
                    targetTr.position = new Vector3(newPose.x, productPanelPos.y, newPose.z);
                    targetTr.LookAt(Camera.main.transform);
                    yield return null;
                }
            }
            else
            {
                targetTr.position = pos;
            }
        }
    }
    public IEnumerator CorUnactive(RectTransform targetTr, float sec)
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
                MainMissionText.text = "상품을 바구니에 담으세요.";
                ActivePanel(MainMissionPanel, false, true, headPos, 3f);
                ActivePanel2(ProductPanel, true, true, playerTr.position, 3f);
                break;

            case 1:
                MainMissionText.text = $"{MissionManager.Instance.KioskNum}번 계산대 이동 하세요.";
                UpdatePos();
                ActivePanel(MainMissionPanel, false, true, headPos, 3f);
                UnActivePanel(ProductPanel, 0f);
                break;

            case 2:
                SettingProductPanel();
                MainMissionText.text = "상품 스캔하기.";
                Debug.Log("2번");
                ActivePanel(MainMissionPanel, false, false, new Vector3(0, 3f, 2), 3f);
                break;

            case 3:
                MainMissionText.text = "상품 결제하기.";
                Debug.Log("3번");
                ActivePanel(MainMissionPanel, false, false, new Vector3(0, 3f, 2), 3f);
                break;

            case 4:
                MainMissionText.text = "미션성공";
                Debug.Log("4번");
                ActivePanel(MainMissionPanel, false, false, new Vector3(0, 3f, 2), 3f);
                UnActivePanel(ProductPanel, 0f);
                break;
        }
    }
    #endregion

    #region Guide

    public void ShowGuide(string context)
    {
        GuideText.text = context;
        //ActivePanel(GuidePanel, false, unactiveSec);
    }
    #endregion

}