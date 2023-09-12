using UnityEngine;

public class GasUpReady : MonoBehaviour
{
    public Transform origin;
    public GameObject myCar;
    public GameObject mySuv;

    public void OnSelectEntered()
    {
        origin.parent = null;
        myCar.SetActive(true);
        Destroy(mySuv);
    }
}