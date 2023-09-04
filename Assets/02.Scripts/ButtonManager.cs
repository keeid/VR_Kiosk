using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public Button targetButton; // �ٸ� ��ư���� �÷� ������ �ٲ� �ʱ�ȭ ��ư
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
