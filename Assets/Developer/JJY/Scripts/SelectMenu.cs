using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenu : MonoBehaviour
{
    public GameObject menu, mySuv, myCar;
    public AutoDriving auto;
    public ManualDriving manual;
    public Transform anchor, origin;

    public void Auto()
    {
        menu.SetActive(false);
        auto.enabled = true;
    }

    public void Manual()
    {
        menu.SetActive(false);
        manual.enabled = true;

    }

    public void GasUp()
    {
        menu.SetActive(false);
        GasUpReady();
    }

    public void GasUpReady()
    {
        origin.parent = null;
        origin.position = anchor.position;
        origin.rotation = anchor.rotation;
        myCar.SetActive(true);
        Destroy(mySuv);
    }
}
