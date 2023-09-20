using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOutUIManager : Singleton<FadeInOutUIManager>
{
    public Action loadSceneAction = null;
    public float timeDeltaTime = 0f;
    public Transform playerTr = null;

    [SerializeField]
    private MeshRenderer fadeImage = null;
    [SerializeField]
    private int time;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        playerTr = Camera.main.transform;
    }

    private void SetFadeObjPos()
    {
        if (playerTr != null)
        {
            gameObject.transform.position = playerTr.position;
        }
        else
        {
            playerTr = Camera.main.transform;
        }
    }

    public void StartFadeIn(Action func)
    {
        timeDeltaTime = Time.deltaTime;
        loadSceneAction = null;
        loadSceneAction += func;
        FadeOut();
    }

    private void FadeOut()
    {
        StartCoroutine(CoFadeOut());
    }

    private void FadeIn()
    {
        StartCoroutine(CoFadeIn());
    }

    IEnumerator CoFadeOut()
    {
        fadeImage.gameObject.SetActive(true);

        playerTr = null;

        float alpha = 0f;
        while (alpha < 1f)
        {
            SetFadeObjPos();
            fadeImage.material.color = new Color(0, 0, 0, alpha);
            alpha += timeDeltaTime / time;
            yield return null;
        }
        loadSceneAction.Invoke();
        FadeIn();
        yield return null;
    }

    IEnumerator CoFadeIn()
    {
        playerTr = null;
        float alpha = 1f;
        while (alpha > 0)
        {
            SetFadeObjPos();
            fadeImage.material.color = new Color(0, 0, 0, alpha);
            alpha -= timeDeltaTime / time;
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);
        yield return null;
    }
}
