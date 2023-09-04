using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProductInfo_Test", menuName = "Scriptable Objects/Spawn ProductInfo_Test", order = 2)]
public class ProductInfo : ScriptableObject
{
    public string productName;
    public int price;
    public string barcode;
    public ObjectType objType;
    public enum ObjectType
    {
        nonProduct,
        Product
    }
}
