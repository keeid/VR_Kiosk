using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductButtonToggleComponent : MonoBehaviour
{
    public void ToggleCheck()
    {
        Debug.Log("ToggleSelected" + gameObject.name);
    }
}
