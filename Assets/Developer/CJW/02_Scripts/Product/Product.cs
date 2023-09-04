using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public ProductInfo productInfo;
    public bool isGrabed;
    public bool isInCart;
    public bool canInCart;

    private void Start()
    {
        Init();
    }

    private void Init()
    {

        // Outline 없으면 OutlineComponent 추가
        if (GetComponent<Outline>() == null)
        {
            gameObject.AddComponent<OutLineComponenet>();
        }
    }
}
