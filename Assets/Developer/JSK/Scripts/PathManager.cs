using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    #region Singleton
    private static PathManager instance = null;
    public static PathManager Instance
    {
        get { return instance; }
    }

    #endregion
    [SerializeField]
    private ProductInfo[] missionProducts = null;
    [SerializeField]
    private bool[] clearTheProduct;
    [SerializeField]
    private Transform[] productSign;


    private bool isSetUp = false;
    private void Start()
    {
        missionProducts = MissionManager.Instance.missionProducts;
        clearTheProduct = new bool[missionProducts.Length];
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (!isSetUp) SetProductSign();
    }

    private void SetProductSign()
    {
        //Get ProudctInfo's Transform And Add Transform Into Array;
        for (int i = 0; i < missionProducts.Length; i++)
        {

        }
    }

    public void CheckClearThePrdouct(int i)
    {
        clearTheProduct[i] = true;
        // Set Off The Sign;z
    }

    private void ShowProductSign()
    {
        for (int i = 0; i < missionProducts.Length; i++)
        {

        }
    }

}
