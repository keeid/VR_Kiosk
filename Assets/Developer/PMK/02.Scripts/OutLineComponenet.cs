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

    // 아웃라인 초기화
    private void InitOutLine()
    {
        outline = gameObject.AddComponent<Outline>();
        outline.OutlineMode = Outline.Mode.OutlineVisible;
        outline.OutlineColor = new Color(0, 0, 0, 0);
        outline.OutlineWidth = 5;
    }

    // 아웃라인 켜기
    public void OnOutline()
    {
        outline.OutlineColor = Color.red;
    }

    // 아웃라인 끄기
    public void OffOutline()
    {
        outline.OutlineColor = new Color(0, 0, 0, 0);
    }
}
