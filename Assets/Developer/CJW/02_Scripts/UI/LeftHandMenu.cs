using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftHandMenu : MonoBehaviour
{
    [Header("UI Pages")]
    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject skipShopping;

    [SerializeField]
    private GameObject moveToKiosk;

    [SerializeField]
    private GameObject options;

    [SerializeField]
    private GameObject quit;


    [Header("Main Menu Buttons")]

    [SerializeField]
    private Button skipBtn;

    [SerializeField]
    private Button skipYesBtn;

    [SerializeField]
    private Button skipNoBtn;

    [SerializeField]
    private Button moveBtn;

    [SerializeField]
    private Button moveYesBtn;

    [SerializeField]
    private Button moveNoBtn;

    [SerializeField]
    private Button quitBtn;

    [SerializeField]
    private Button quitYesBtn;

    [SerializeField]
    private Button quitNoBtn;

    [SerializeField]
    private Button optionBtn;

    [SerializeField]
    private Button optionExitBtn;

    private void Start()
    {
        // Default
        EnableMainMenu();

        // Events
        skipBtn.onClick.AddListener(EnableSkip);
        skipYesBtn.onClick.AddListener(SkipShopping);
        skipNoBtn.onClick.AddListener(EnableMainMenu);

        moveBtn.onClick.AddListener(EnableMove);
        moveYesBtn.onClick.AddListener(MoveToKiosk);
        moveNoBtn.onClick.AddListener(EnableMainMenu);

        quitBtn.onClick.AddListener(EnableQuit);
        quitYesBtn.onClick.AddListener(QuitGame);
        quitNoBtn.onClick.AddListener(EnableMainMenu);

        optionBtn.onClick.AddListener(EnableOptions);
        optionExitBtn.onClick.AddListener(EnableMainMenu);
    }

    // 게임 종료
    public void QuitGame()
    {
        // Only Editor
        UnityEditor.EditorApplication.isPlaying = false;

        // Only Application
        Application.Quit();
    }

    //패널 전부 종료
    public void HideAll()
    {
        skipShopping.SetActive(false);
        moveToKiosk.SetActive(false);
        options.SetActive(false);
        quit.SetActive(false);
    }

    // 메인 메뉴만 활성화
    public void EnableMainMenu()
    {
        HideAll();
        mainMenu.SetActive(true);
    }

    // 쇼핑 생략 패널만 활성화
    public void EnableSkip()
    {
        skipShopping.SetActive(true);
        moveToKiosk.SetActive(false);
        options.SetActive(false);
        quit.SetActive(false);
        mainMenu.SetActive(false);
    }

    // 계산대로 이동 패널만 활성화
    public void EnableMove()
    {
        skipShopping.SetActive(false);
        moveToKiosk.SetActive(true);
        options.SetActive(false);
        quit.SetActive(false);
        mainMenu.SetActive(false);
    }

    // 옵션 메뉴만 활성화
    public void EnableOptions()
    {
        skipShopping.SetActive(false);
        moveToKiosk.SetActive(false);
        options.SetActive(true);
        quit.SetActive(false);
        mainMenu.SetActive(false);
    }

    // 종료 메뉴만 활성화
    public void EnableQuit()
    {
        skipShopping.SetActive(false);
        moveToKiosk.SetActive(false);
        options.SetActive(false);
        quit.SetActive(true);
        mainMenu.SetActive(false);
    }

    // 계산대로 이동
    public void MoveToKiosk()
    {
        // 기능 
        Debug.Log("계산대로 이동");
    }

    // 쇼핑 생략
    public void SkipShopping()
    {
        // 기능
        Debug.Log("쇼핑 생략");
    }

}
