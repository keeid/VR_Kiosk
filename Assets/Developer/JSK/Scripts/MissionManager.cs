using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    #region Singleton
    private static MissionManager instance = null;
    public static MissionManager Instance
    {
        get { return instance; }
    }

    #endregion

    public List<ProductInfo> products = null;

    // Mission Info - Product
    public int typeCnt { get; private set; } // ��ƾ� �ϴ� ��ǰ�� ���� �� 2 ~ 3

    public ProductInfo[] missionProducts = null;
    public int[] missionProductCnt = null;

    private int[] productSelectNums;

    private bool isCountable;

    // Current Info - Product
    private int trueCnt = 0;
    public int[] currentProductCnt;

    // Step Info
    private bool isStepUp = false;
    public int Step { get; private set; }

    // Go To Kiosk
    public int KioskNum { get; private set; }
    private Vector3 missionKisokPos;

    // Time Info
    public float Timer { get; private set; }

    //�����

    public Transform kioskTr;
    public Transform playerTr;

    public Transform basketTriggerTr;

    private void Awake()
    {
        instance = this;
        Step = 0;
        InitSetting0();
    }
    private void Update()
    {
        Timer += Time.deltaTime;
        switch (Step)
        {
            case 1:
                if (isStepUp) InitSetting1();
                missionUpdate1();
                break;
            case 2:
                if (isStepUp) InitSetting2();
                break;
        }
    }
    #region InitSetting - Step0 ��ٱ��Ͽ� ���
    private void InitSetting0()
    {
        // Set Cnt
        //typeCnt = Random.Range(2, 4);
        isCountable = true;
        typeCnt = 1;

        productSelectNums = new int[typeCnt];

        missionProducts = new ProductInfo[typeCnt]; // ��ƾ� �ϴ� ��ǰ�� ���� ����Ʈ
        missionProductCnt = new int[typeCnt];       // ��ǰ �� ��ƾ� �ϴ� ���� 
        currentProductCnt = new int[typeCnt];       // ���� ��ǰ �� ��� ����

        productSelectNums = GetNumber(products.Count);  // ��ƾ� �ϴ� ��ǰ�� ��ȣ ����Ʈ

        for (int i = 0; i < typeCnt; i++)
        {
            missionProducts[i] = products[productSelectNums[i]];
            //missionProductCnt[i] = Random.Range(1, 4);
            missionProductCnt[i] = 2;
            Debug.Log($"�̼� ��ǰ : {missionProducts[i]} , ���� : {missionProductCnt[i]}");
        }
    }
    private int[] GetNumber(int productsCount)
    {
        int[] numbers = new int[productsCount];
        int[] selectNum = new int[typeCnt];
        Debug.Log($"��ϵ� ��ǰ�� ���� : {productsCount}");

        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = i;
        }
        for (int i = 0; i < numbers.Length; i++)
        {
            int randomNum = Random.Range(i, numbers.Length);
            int temp = numbers[i];
            numbers[i] = numbers[randomNum];
            numbers[randomNum] = temp;
        }
        for (int i = 0; i < typeCnt; i++)
        {
            selectNum[i] = numbers[i];
        }
        Debug.Log($"���� ������ : {selectNum}");
        return selectNum;
    }
    #endregion

    #region Step0
    private void ChckProduct()
    {

    }
    public void AddProcut(Collider productColl)
    {
        string name;
        if (productColl.GetComponent<Product>().productInfo != null && isCountable)
        {
            name = productColl.GetComponent<Product>().productInfo.productName;
            for (int i = 0; i < missionProducts.Length; i++)
            {
                Debug.Log($"üũüũ {missionProducts[i].productName} : {name}");
                if (missionProducts[i].productName == name)
                {
                    currentProductCnt[i]++;
                    CheckCnt(missionProducts[i], i);
                }
                i++;
            }
        }
    }
    public void RemoveProduct(Collider productColl)
    {
        string name;
        int productCnt = 0;

        if (productColl.GetComponent<Product>().productInfo != null && isCountable)
        {
            //�̰� �ڽ��ѹ��� ����
            name = productColl.GetComponent<Product>().productInfo.productName;
            List<GameObject> product = new List<GameObject>();
            if (basketTriggerTr.childCount != 0)
            {
              product = basketTriggerTr.GetComponentsInChildren<GameObject>().ToList();
            }

            for (int i = 0; i < product.Count; i++)
            {
                if (product[i].GetComponent<Product>().productInfo.name == name) productCnt++;
            }

            for (int i = 0; i < missionProducts.Length; i++)
            {
                if (missionProducts[i].productName == name)
                {
                    currentProductCnt[i] = productCnt;
                    CheckCnt(missionProducts[i], i);
                }
            }

        }
    }
    private void CheckCnt(ProductInfo productinfo, int num)
    {
        //���� �ϳ� �߰� ���� 1.���࿡ �̹� �ִ�ġ�� �������Ѿ��ϳ�?
        //�ѹ����� �Ѿ�� �������ʹ� �ȳѾ�پ�

        if (isCountable)
        {
            if (missionProductCnt[num] == currentProductCnt[num])
            {
                trueCnt++;
                PathManager.Instance.CheckClearThePrdouct(num);
            }
            if (trueCnt == typeCnt)
            {
                isCountable = false;
                Debug.Log("��������");
                NextStep();
            }
        }
    }
    #endregion

    #region Step1 Ű����ũ�� �̵�
    private void InitSetting1()
    {
        isStepUp = false;
        KioskNum = Random.Range(1, 4); //1~3
        switch (KioskNum)
        {
            case 1:
                missionKisokPos = Vector3.zero;
                break;

            case 2:
                missionKisokPos = Vector3.zero;
                break;

            case 3:

                missionKisokPos = Vector3.zero;
                break;
        }
    }

    private void missionUpdate1()
    {
        Collider[] colls = Physics.OverlapBox(missionKisokPos, Vector3.one * 2, Quaternion.identity);
        if (colls.Length != 0)
        {
            foreach (Collider coll in colls)
            {
                if (coll.CompareTag("Player"))
                {
                    NextStep();
                }
            }
        }
        if (Vector3.Distance(playerTr.position, kioskTr.position) <= 2f)
        {
            NextStep();
        }
    }
    #endregion

    #region Step3 - ��ǰ ��ĵ�ϱ�
    private void InitSetting2()
    {
        isStepUp = false;
        isCountable = true;
        for (int i = 0; i < currentProductCnt.Length; i++)
        {
            currentProductCnt[i] = 0;
        }
    }
    // ��ĵ ��ǰ�� AddProduct

    public void AddProductInKiosk(ProductInfo info)
    {
        string name;
        if (info != null)
        {
            name = info.productName;
            for (int i = 0; i < missionProducts.Length; i++)
            {
                Debug.Log($"üũüũ {missionProducts[i].productName} : {name}");
                if (missionProducts[i].productName == name)
                {
                    currentProductCnt[i]++;
                    CheckCnt(missionProducts[i], i);
                }
                i++;
            }
        }
    }
    public void RemoveProductInKiosk(ProductInfo info)
    {
        string name;
        if (info != null)
        {
            name = info.productName;
            for (int i = 0; i < missionProducts.Length; i++)
            {
                Debug.Log($"üũ üũ {missionProducts[i].productName} : {name}");
                if (missionProducts[i].productName == name)
                {
                    currentProductCnt[i]--;
                    CheckCnt(missionProducts[i], i);
                }
                i++;
            }
        }
    }

    public void DeleterProductInKiosk(ProductInfo info)
    {
        string name;
        if (info != null)
        {
            name = info.productName;
            for (int i = 0; i < missionProducts.Length; i++)
            {
                Debug.Log($"üũ üũ {missionProducts[i].productName} : {name}");
                if (missionProducts[i].productName == name)
                {
                    currentProductCnt[i] = 0;
                    CheckCnt(missionProducts[i], i);
                }
            }
        }
    }
    #endregion

    //Step4 ������ NextStep���� �н�
    #region Step
    public void NextStep()
    {
        Step++;
        UIManager.Instance.isTaskInfo = true;
        isStepUp = true;
    }
    #endregion
}
