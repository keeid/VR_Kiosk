using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;

// 스캔한 상품의 정보
public class ScannedProduct
{
    public int count;
    public GameObject content;
    public TextMeshProUGUI productListCountText;
    public TextMeshProUGUI productContentText;
    public TextMeshProUGUI productCountText;
    public TextMeshProUGUI productPriceText;
    public ProductInfo m_productInfo = null;

    public ScannedProduct(ProductInfo info, GameObject gameObj)
    {
        m_productInfo = info;
        count = 0;
        content = gameObj;
        AddCount(info, 1);
    }

    public void AddProductCount()
    {
        AddCount(m_productInfo, 1);
    }

    public void SubProductCount(int subtractCount = -1)
    {
        if (count > 0)
        {
            AddCount(m_productInfo, subtractCount);

            if (count == 0)
            {
                Kiosk.instance.DeleteProductInList(m_productInfo, content);
            }
        }
    }

    public void AddCount(ProductInfo info, int buttonCount)
    {
        if (m_productInfo == null)
        {
            m_productInfo = info;
        }
        count += buttonCount;
        productContentText.text = info.productName;
        productCountText.text = count.ToString();
        productPriceText.text = (info.price * count).ToString();
        Kiosk.instance.ReWriteSumText(info.price * buttonCount);
        Kiosk.instance.ReWriteSumText(buttonCount);
    }

    public void ResetListIndex()
    {
        int? index = Kiosk.instance.scannedProducts.Select((value, idx) => new { value, idx })
        .FirstOrDefault(pair => pair.value.Item1 == m_productInfo.productName)?.idx;
        productListCountText.text = index.ToString();
    }
}

// 키오스크 시스템
public class Kiosk : MonoBehaviour
{
    public static Kiosk instance;
    private void Awake()
    {
        instance = this;
    }

    #region Kiosk Field
    [Header("BarcodeScanner")]
    [SerializeField] private LayerMask productLayerMask;  // 감지할 레이어 마스크
    [SerializeField] private float maxRaycastDistance = 10f; // Raycast 최대 거리

    public List<Tuple<string, int, ScannedProduct>> scannedProducts = new List<Tuple<string, int, ScannedProduct>>();  // 스캔한 상품<이름, 가격> 목록

    [Header("PaymentUI")]
    [SerializeField] private ToggleGroup productToggleGroup = null;
    [SerializeField] private Transform paymentViewPortTr = null;    // 결제상품란 UI Tr
    [SerializeField] private GameObject paymentContent = null;  // 결제란 콘텐츠
    [SerializeField] private TextMeshProUGUI priceSumText = null;
    [SerializeField] private TextMeshProUGUI countSumText = null;
    private int priceSum = 0;
    private int countSum = 0;
    public Collider prePurchasedProductCol = null;

    [Header("BarcodeScannerLine")]
    public Color lineColor = Color.red;
    public float lineWidth = 1f;
    public float lineDuration = 5f;
    public LayerMask detectLayer;
    #endregion

    private void Update()
    {
        DetectProduct();
    }

    #region Kiosk System
    // 스캐너로 상품을 인식하는 함수

    public Transform scannerRayTr = null;
    private void DetectProduct()
    {
        if (KioskUI.instance.kioskPanelIndex == 1)
        {
            // Raycast 발사
            if (Physics.Raycast(scannerRayTr.position, scannerRayTr.forward, out RaycastHit hit, maxRaycastDistance, productLayerMask))
            {
                // 충돌한 오브젝트가 상품 바코드일 경우 && 상품의 z 각도가 -10~10 사이 일 경우
                //if (hit.collider.CompareTag("Product") && hit.collider != prePurchasedProductCol)
                if (hit.collider.CompareTag("Product") && hit.collider != prePurchasedProductCol
                && hit.transform.rotation.z <= 10f && hit.transform.rotation.z >= -10f)
                {
                    //DrawLine(hit.point, hit.normal, hit.transform, lineColor, lineWidth, lineDuration);
                    // 상품 정보 스크립터블 오브젝트를 가져와서 해당 상품의 정보를 추출
                    ProductInfo productInfo = hit.collider.gameObject.GetComponent<Product>().productInfo;


                    AddProduct(productInfo);

                    // 가장 최근에 등록된 상품으로 저장
                    prePurchasedProductCol = hit.collider;
                }
            }
            else
            {
                prePurchasedProductCol = null;
            }
        }
    }

    public void SubtractProduct()
    {
        var selectedProduct = productToggleGroup.ActiveToggles().FirstOrDefault();
        scannedProducts.Where(x => x.Item1 == selectedProduct.name).First().Item3.SubProductCount();
    }

    public void PlusProduct()
    {
        var selectedProduct = productToggleGroup.ActiveToggles().FirstOrDefault();
        scannedProducts.Where(x=> x.Item1 == selectedProduct.name).First().Item3.AddProductCount();
    }

    public void DeleteProduct()
    {
        DeleteProductInList();
    }

    // 상품 0개 이하로 떨어지면 콘텐츠(상품 리스트 UI 텍스트 & -, +버튼) 삭제
    public void DeleteProductInList(ProductInfo info = null, GameObject content = null)
    {
        if(info == null && content == null)
        {
            var selectedProduct = productToggleGroup.ActiveToggles().FirstOrDefault();
            var scannedProduct = scannedProducts.Where(x => x.Item1 == selectedProduct.name).First();
            scannedProduct.Item3.SubProductCount(-scannedProduct.Item3.count);
            return;
        }
        Destroy(content);
        scannedProducts.Remove(scannedProducts.Where(x => x.Item1 == info.productName).First() as System.Tuple<string, int, ScannedProduct>);
        scannedProducts = scannedProducts.Where(x => x != null).ToList();

        foreach(var item in scannedProducts)
        {
            item.Item3.ResetListIndex();
        }
    }

    // 결제상품 품목 추가 시 실행되는 함수  
    private void AddProduct(ProductInfo productInfo)
    {
        // 같은 상품이면 콘텐츠 텍스트에서 개수만 1 올림
        if (scannedProducts.Where(x => x.Item1 == productInfo.productName).Count() > 0)
        {
            scannedProducts.Where(x => x.Item1 == productInfo.productName).FirstOrDefault().Item3.AddCount(productInfo, 1);
        }
        // 같은 상품이 아니면 새로 콘텐츠를 추가
        else
        {
            GameObject productContent = Instantiate(paymentContent, paymentViewPortTr);
            productContent.GetComponent<Toggle>().group = paymentViewPortTr.GetComponent<ToggleGroup>();
            productContent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = productInfo.productName + " : " + productInfo.price + " : 1개";
            //transform.rotation = new Quaternion(10, 30, 40);
            // 추출한 정보를 이용하여 다른 기능 수행
            scannedProducts.Add(new Tuple<string, int, ScannedProduct>(productInfo.productName, productInfo.price,
                new ScannedProduct(productInfo, productContent)));
        }
    }

    // 총 결제액 변경 시 UI 업데이트
    public void ReWriteSumText(int price)
    {
        priceSum += price;
        priceSumText.text = priceSum.ToString();
    }

    // 개수 늘이고 줄이기
    public void RewriteCountSum(int count)
    {
        countSum += count;
        countSumText.text = countSum.ToString();
    }

    // 결제
    private void Pay()
    {
        // 결제하면 결제완료 창이 뜸
        OnOffKioskScreen(ScreenType.Payment, false);
        OnOffKioskScreen(ScreenType.PayComplete, true);
    }

    // 화면에 스캔한 상품 정보를 보여줌
    // 레이아웃에 맞춰서 오브젝트 생성하고 ui text 추가
    public void ShowOnScreen()
    {
        foreach (var product in scannedProducts)
        {
            GameObject productContent = Instantiate(paymentContent, paymentViewPortTr);
            productContent.GetComponent<TextMeshProUGUI>().text = product.Item1 + product.Item2;
        }
    }

    #endregion

    #region Screen Active True/False
    // 화면 종류
    public enum ScreenType
    {
        Main,
        Payment,
        ProductList,
        PayComplete
    }

    [Header("키오스크 패널들")]
    public GameObject[] screenArr = null;

    // 결제 창 열고/닫기
    public void OnOffKioskScreen(ScreenType screenType, bool onOff)
    {
        screenArr[(int)screenType].SetActive(onOff);
    }
    #endregion

    #region Test Func
    // 바코드 스캐너 빨간색 라인 생성 함수 
    private void DrawLine(Vector3 position, Vector3 normal, Transform targetTransform, Color color, float width, float duration)
    {
        GameObject lineObj = new GameObject();
        lineObj.transform.position = position;
        lineObj.transform.SetParent(targetTransform);
        lineObj.AddComponent<LineRenderer>();
        LineRenderer lineRenderer = lineObj.GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
        lineRenderer.SetPosition(0, position);
        lineRenderer.SetPosition(1, position + targetTransform.TransformDirection(normal) * 0.1f);
        Destroy(lineObj, duration);
    }
    #endregion

    //public GameObject receiptment;
    // 영수증 출력
    public void OutputReceiptMent()
    {
        // 어떻게 출력될지 정하기 -> DoRotate or DoMove
    }
}