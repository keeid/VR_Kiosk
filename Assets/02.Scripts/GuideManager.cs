using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideManager : MonoBehaviour
{
    #region Singleton
    private static GuideManager instance = null;
    public static GuideManager Instance
    {
        get { return instance; }
    }
    #endregion

    public Transform[] GuideCategoryTrArr = null;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if(GuideCategoryTrArr.Length < (int)Define.Category.Last)
        {
            Debug.Log("가이드 카테고리 Tr을 달아두지 않았음");
        }
    }
}
