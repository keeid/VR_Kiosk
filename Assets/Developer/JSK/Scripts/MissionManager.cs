using System.Collections;
using System.Collections.Generic;
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

    // Current Info - Product
    private int trueCnt = 0;
    public int[] currentProductCnt;

    // Step Info

    public int Step { get; private set; }


    private int productFinishCnt;

    // Time Info
    public float Timer { get; private set; }

    private void Awake()
    {
        instance = this;
        Step = 0;
        InitSetting();
    }
    private void Update()
    {
        Timer += Time.deltaTime;    
    }
    public void AddProcut(Collider productColl)
    {
        string name;
        if (productColl.GetComponent<Product>().productInfo != null)
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
        if (productColl.GetComponent<Product>().productInfo != null)
        {
            name = productColl.GetComponent<Product>().productInfo.productName;
            for (int i = 0; i < missionProducts.Length; i++)
            {
                Debug.Log($"üũ üũ {missionProducts[i].productName} : {name}");
                if (missionProducts[i].productName == name)
                {
                    currentProductCnt[i]--;
                    CheckCnt(missionProducts[i], i);
                }
                else
                {
                }
                i++;
            }
        }
    }
    private void CheckCnt(ProductInfo productinfo, int num)
    {
        //���� �ϳ� �߰� ���� 1.���࿡ �̹� �ִ�ġ�� �������Ѿ��ϳ�?
        //�ѹ����� �Ѿ�� �������ʹ� �ȳѾ�پ�

        if (missionProductCnt[num] == currentProductCnt[num]) trueCnt++;
        if (trueCnt == typeCnt)
        {
            NextStep();
        }
    }

    #region InitSetting
    private void InitSetting()
    {
        // Set Cnt
        typeCnt = Random.Range(2, 4);

        productSelectNums = new int[typeCnt];

        missionProducts = new ProductInfo[typeCnt]; // ��ƾ� �ϴ� ��ǰ�� ���� ����Ʈ
        missionProductCnt = new int[typeCnt];       // ��ǰ �� ��ƾ� �ϴ� ���� 
        currentProductCnt = new int[typeCnt];       // ���� ��ǰ �� ��� ����

        productSelectNums = GetNumber(products.Count);  // ��ƾ� �ϴ� ��ǰ�� ��ȣ ����Ʈ

        for (int i = 0; i < typeCnt; i++)
        {
            missionProducts[i] = products[productSelectNums[i]];
            missionProductCnt[i] = Random.Range(1, 4);
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

    #region Step
    public void NextStep()
    {
        Step++;
        UIManager.Instance.isTaskInfo = true;
    }
    #endregion
}
