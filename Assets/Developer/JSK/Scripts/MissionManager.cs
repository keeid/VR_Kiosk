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
    public int typeCnt { get; private set; } // 담아야 하는 상품의 종류 수 2 ~ 3
    
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

    //예비로

    public Transform kioskTr;
    public Transform playerTr;


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
            case 3:
                if (isStepUp) InitSetting3();
                break;
            case 4:
                break;
            case 5:
                break;
        }
    }
    #region InitSetting - Step0 장바구니에 담기
    private void InitSetting0()
    {
        // Set Cnt
        //typeCnt = Random.Range(2, 4);
        isCountable = true;
        typeCnt = 1;

        productSelectNums = new int[typeCnt];

        missionProducts = new ProductInfo[typeCnt]; // 담아야 하는 상품의 종류 리스트
        missionProductCnt = new int[typeCnt];       // 상품 당 담아야 하는 개수 
        currentProductCnt = new int[typeCnt];       // 현재 상품 당 담긴 개수

        productSelectNums = GetNumber(products.Count);  // 담아야 하는 상품의 번호 리스트

        for (int i = 0; i < typeCnt; i++)
        {
            missionProducts[i] = products[productSelectNums[i]];
            //missionProductCnt[i] = Random.Range(1, 4);
            missionProductCnt[i] = 1;
            Debug.Log($"미션 상품 : {missionProducts[i]} , 개수 : {missionProductCnt[i]}");
        }
    }
    private int[] GetNumber(int productsCount)
    {
        int[] numbers = new int[productsCount];
        int[] selectNum = new int[typeCnt];
        Debug.Log($"등록된 상품의 개수 : {productsCount}");

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
        Debug.Log($"뽑힌 순번은 : {selectNum}");
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
                Debug.Log($"체크체크 {missionProducts[i].productName} : {name}");
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
        if (productColl.GetComponent<Product>().productInfo != null && isCountable)
        {
            name = productColl.GetComponent<Product>().productInfo.productName;
            for (int i = 0; i < missionProducts.Length; i++)
            {
                Debug.Log($"체크 체크 {missionProducts[i].productName} : {name}");
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
        //여기 하나 추가 예외 1.만약에 이미 최대치면 증가시켜야하나?
        //한번스텝 넘어가면 다음부터는 안넘어간다야

        if(isCountable)
        {
            if (missionProductCnt[num] == currentProductCnt[num]) trueCnt++;
            if (trueCnt == typeCnt)
            {
                isCountable = false;
                Debug.Log("다음스텝");
                NextStep();
            }
        }
    }
    #endregion

    #region Step1 키오스크로 이동
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
        Collider[] colls = Physics.OverlapBox(missionKisokPos, Vector3.one, Quaternion.identity);
        if (colls.Length != 0)
        {
            foreach (Collider coll in colls)
            {
                if(coll.CompareTag("Player"))
                {
                    NextStep();
                }
            }
        }
        if(Vector3.Distance(playerTr.position,kioskTr.position) <= 2f)
        {
            NextStep();
        }
    }
    #endregion

    // Step2 - 장바구니 올리기는 너무 간단 해 NextStep으로 패스
    private void InitSetting2()
    {
        isStepUp = false;
        NextStep();
    }
    #region Step3 - 상품 스캔하기
    private void InitSetting3()
    {
        isStepUp = false;
        isCountable = true;
        for (int i = 0; i < currentProductCnt.Length; i++)
        {
            currentProductCnt[i] = 0;
        }
    }
    // 스캔 상품은 AddProduct

    public void AddProductInKiosk(ProductInfo info)
    {
        string name;
        if (info != null)
        {
            name = info.productName;
            for (int i = 0; i < missionProducts.Length; i++)
            {
                Debug.Log($"체크체크 {missionProducts[i].productName} : {name}");
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
                Debug.Log($"체크 체크 {missionProducts[i].productName} : {name}");
                if (missionProducts[i].productName == name)
                {
                    currentProductCnt[i]--;
                    CheckCnt(missionProducts[i], i);
                }
                i++;
            }
        }
    }
    #endregion

    //Step4 간단해 NextStep으로 패스
    #region Step
    public void NextStep()
    {
        Step++;
        UIManager.Instance.isTaskInfo = true;
        isStepUp = true;
    }
    #endregion
}
