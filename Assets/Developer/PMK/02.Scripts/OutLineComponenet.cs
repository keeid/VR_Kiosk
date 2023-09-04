using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OutLineComponenet : MonoBehaviour
{
    public Outline outline = null;

    private void Start()
    {
        InitOutLine();
    }

    // �ƿ����� �ʱ�ȭ
    private void InitOutLine()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = new Color(0, 0, 0, 0);
        outline.OutlineWidth = 5;
    }

    // �ƿ����� �ѱ�
    public void OnOutline()
    {
        outline.OutlineColor = Color.red;
    }

    // �ƿ����� ����
    public void OffOutline()
    {
        outline.OutlineColor = new Color(0, 0, 0, 0);
    }
}
