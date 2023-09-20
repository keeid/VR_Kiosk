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

    // ���� ����
    public void StartGame()
    {
        HideAll();
        SmartSceneManager.SceneTransitionToStore(0);
    }

    // ���� ����
    public void QuitGame()
    {
        // Only Editor
        UnityEditor.EditorApplication.isPlaying = false;

        // Only Application
        Application.Quit();
    }

    // �г� ���� ����
    public void HideAll()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        quit.SetActive(false);
    }

    // ���� �޴��� Ȱ��ȭ
    public void EnableMainMenu()
    {
        mainMenu.SetActive(true);
        options.SetActive(false);
        quit.SetActive(false);
    }

    // �ɼ� �޴��� Ȱ��ȭ
    public void EnableOptions()
    {
        mainMenu.SetActive(false);
        options.SetActive(true);
        quit.SetActive(false);
    }

    // ���� �޴��� Ȱ��ȭ
    public void EnableQuit()
    {
        mainMenu.SetActive(false);
        options.SetActive(false);
        quit.SetActive(true);
    }

    // ���� �޴��� Ȱ��ȭ
    //public void ExitOptions()
    //{
    //    mainMenu.SetActive(true);
    //    options.SetActive(false);
    //    quit.SetActive(false);
    //}
}
