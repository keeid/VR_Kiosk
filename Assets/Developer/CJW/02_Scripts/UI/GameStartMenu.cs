using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartMenu : MonoBehaviour
{
    [Header("UI Pages")]
    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject options;

    [SerializeField]
    private GameObject quit;

    [Header("Main Menu Buttons")]
    [SerializeField]
    private Button startBtn;

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
        mainMenu.SetActive(true);

        // Events
        startBtn.onClick.AddListener(StartGame);
        quitBtn.onClick.AddListener(EnableQuit);
        optionBtn.onClick.AddListener(EnableOptions);
        quitYesBtn.onClick.AddListener(QuitGame);
        quitNoBtn.onClick.AddListener(EnableMainMenu);
        optionExitBtn.onClick.AddListener(EnableMainMenu);
    }

    // 게임 시작
    public void StartGame()
    {
        HideAll();
        SmartSceneManager.SceneTransitionToStore(0);
    }

    // 게임 종료
    public void QuitGame()
    {
        // Only Editor
        UnityEditor.EditorApplication.isPlaying = false;

        // Only Application
        Application.Quit();
    }

    // 패널 전부 종료
    public void HideAll()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        quit.SetActive(false);
    }

    // 메인 메뉴만 활성화
    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
        quit.SetActive(false);
    }

    // 옵션 메뉴만 활성화
    public void EnableOptions()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
        quit.SetActive(false);
    }

    // 종료 메뉴만 활성화
    public void EnableQuit()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        quit.SetActive(true);
    }

    // 종료 메뉴만 활성화
    //public void ExitOptions()
    //{
    //    mainMenu.SetActive(true);
    //    options.SetActive(false);
    //    quit.SetActive(false);
    //}
}
