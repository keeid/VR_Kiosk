using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetOptionsFromUI : MonoBehaviour
{
    [Header("UI Pages")]
    [SerializeField]
    private GameObject audioWindow;

    [SerializeField]
    private GameObject operateWindow;

    [SerializeField]
    private GameObject descriptWindow;

    [Header("Settings Buttons")]
    [SerializeField]
    private Button audioBtn;

    [SerializeField]
    private Button operateBtn;

    [SerializeField]
    private Button descriptBtn;

    private void Start()
    {
        // Default
        audioWindow.SetActive(true);

        // Events
        audioBtn.onClick.AddListener(EnableAudio);
        operateBtn.onClick.AddListener(EnableOperate);
        descriptBtn.onClick.AddListener(EnableDescript);
    }

    // ����� �г� Ȱ��ȭ
    public void EnableAudio()
    {
        audioWindow.SetActive(true);
        operateWindow.SetActive(false);
        descriptWindow.SetActive(false);
    }

    // ���� ���� �г� Ȱ��ȭ
    public void EnableOperate()
    {
        audioWindow.SetActive(false);
        operateWindow.SetActive(true);
        descriptWindow.SetActive(false);
    }

    // ���� ��� �г� Ȱ��ȭ
    public void EnableDescript()
    {
        audioWindow.SetActive(false);
        operateWindow.SetActive(false);
        descriptWindow.SetActive(true);
    }

}
