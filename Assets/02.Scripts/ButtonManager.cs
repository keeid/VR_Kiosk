using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button targetButton; // 다른 버튼들의 컬러 세팅을 바꿀 초기화 버튼
    public Color buttonColor = default;
    // Start is called before the first frame update
    void Awake()
    {
        ColorBlock colorBlock = targetButton.colors;
        Button[] allButtonsInScene = Resources.FindObjectsOfTypeAll<Button>();
        foreach (Button btn in allButtonsInScene)
        {
            btn.colors = colorBlock;
        }
    }
}
