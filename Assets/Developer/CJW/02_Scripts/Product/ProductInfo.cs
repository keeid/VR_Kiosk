using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductInfo_Test", menuName = "Scriptable Objects/Spawn ProductInfo_Test", order = 2)]
public class ProductInfo : ScriptableObject
{
    public string productName;
    public string barcode;
    public int price;
    public Define.ObjectType objType;
    public Define.Category category;
}
