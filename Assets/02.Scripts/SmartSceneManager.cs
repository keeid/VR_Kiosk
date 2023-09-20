using UnityEngine;
using UnityEngine.SceneManagement;

public class SmartSceneManager
{
    public static void ExitGame()
    {
        // Only UnityEditor
        UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();
    }

    public static void SceneTransitionToStore(int sceneIndex = 0)
    {
        FadeInOutUIManager.Instance.StartFadeIn(() => SceneManager.LoadScene(sceneIndex));
    }
    public static void MainCity()
    {
        SceneManager.LoadScene(1);
    }

    public static void Prison()
    {
        SceneManager.LoadScene(2);
    }


    public static void GameStart()
    {
        SceneManager.LoadScene(10);
    }

}

