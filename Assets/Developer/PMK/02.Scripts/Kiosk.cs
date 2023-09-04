using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Linq;
using DG.Tweening;
using UnityEngine.UI;

// ��ĵ�� ��ǰ�� ����
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

// Ű����ũ �ý���
public class Kiosk : MonoBehaviour
{
    public static Kiosk instance;
    private void Awake()
    {
        instance = this;
    }

    #region Kiosk Field
    [Header("BarcodeScanner")]
    [SerializeField] private LayerMask productLayerMask;  // ������ ���̾� ����ũ
    [SerializeField] private float maxRaycastDistance = 10f; // Raycast �ִ� �Ÿ�

    public List<Tuple<string, int, ScannedProduct>> scannedProducts = new List<Tuple<string, int, ScannedProduct>>();  // ��ĵ�� ��ǰ<�̸�, ����> ���

    [Header("PaymentUI")]
    [SerializeField] private ToggleGroup productToggleGroup = null;
    [SerializeField] private Transform paymentViewPortTr = null;    // ������ǰ�� UI Tr
    [SerializeField] private GameObject paymentContent = null;  // ������ ������
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
    // ��ĳ�ʷ� ��ǰ�� �ν��ϴ� �Լ�

    public Transform scannerRayTr = null;
    private void DetectProduct()
    {
        if (KioskUI.instance.kioskPanelIndex == 1)
        {
            // Raycast �߻�
            if (Physics.Raycast(scannerRayTr.position, scannerRayTr.forward, out RaycastHit hit, maxRaycastDistance, productLayerMask))
            {
                // �浹�� ������Ʈ�� ��ǰ ���ڵ��� ��� && ��ǰ�� z ������ -10~10 ���� �� ���
                //if (hit.collider.CompareTag("Product") && hit.collider != prePurchasedProductCol)
                if (hit.collider.CompareTag("Product") && hit.collider != prePurchasedProductCol
                && hit.transform.rotation.z <= 10f && hit.transform.rotation.z >= -10f)
                {
                    //DrawLine(hit.point, hit.normal, hit.transform, lineColor, lineWidth, lineDuration);
                    // ��ǰ ���� ��ũ���ͺ� ������Ʈ�� �����ͼ� �ش� ��ǰ�� ������ ����
                    ProductInfo productInfo = hit.collider.gameObject.GetComponent<Product>().productInfo;


                    AddProduct(productInfo);

                    // ���� �ֱٿ� ��ϵ� ��ǰ���� ����
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

    // ��ǰ 0�� ���Ϸ� �������� ������(��ǰ ����Ʈ UI �ؽ�Ʈ & -, +��ư) ����
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

    // ������ǰ ǰ�� �߰� �� ����Ǵ� �Լ�  
    private void AddProduct(ProductInfo productInfo)
    {
        // ���� ��ǰ�̸� ������ �ؽ�Ʈ���� ������ 1 �ø�
        if (scannedProducts.Where(x => x.Item1 == productInfo.productName).Count() > 0)
        {
            scannedProducts.Where(x => x.Item1 == productInfo.productName).FirstOrDefault().Item3.AddCount(productInfo, 1);
        }
        // ���� ��ǰ�� �ƴϸ� ���� �������� �߰�
        else
        {
            GameObject productContent = Instantiate(paymentContent, paymentViewPortTr);
            productContent.GetComponent<Toggle>().group = paymentViewPortTr.GetComponent<ToggleGroup>();
            productContent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = productInfo.productName + " : " + productInfo.price + " : 1��";
            //transform.rotation = new Quaternion(10, 30, 40);
            // ������ ������ �̿��Ͽ� �ٸ� ��� ����
            scannedProducts.Add(new Tuple<string, int, ScannedProduct>(productInfo.productName, productInfo.price,
                new ScannedProduct(productInfo, productContent)));
        }
    }

    // �� ������ ���� �� UI ������Ʈ
    public void ReWriteSumText(int price)
    {
        priceSum += price;
        priceSumText.text = priceSum.ToString();
    }

    // ���� ���̰� ���̱�
    public void RewriteCountSum(int count)
    {
        countSum += count;
        countSumText.text = countSum.ToString();
    }

    // ����
    private void Pay()
    {
        // �����ϸ� �����Ϸ� â�� ��
        OnOffKioskScreen(ScreenType.Payment, false);
        OnOffKioskScreen(ScreenType.PayComplete, true);
    }

    // ȭ�鿡 ��ĵ�� ��ǰ ������ ������
    // ���̾ƿ��� ���缭 ������Ʈ �����ϰ� ui text �߰�
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
    // ȭ�� ����
    public enum ScreenType
    {
        Main,
        Payment,
        ProductList,
        PayComplete
    }

    [Header("Ű����ũ �гε�")]
    public GameObject[] screenArr = null;

    // ���� â ����/�ݱ�
    public void OnOffKioskScreen(ScreenType screenType, bool onOff)
    {
        screenArr[(int)screenType].SetActive(onOff);
    }
    #endregion

    #region Test Func
    // ���ڵ� ��ĳ�� ������ ���� ���� �Լ� 
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
    // ������ ���
    public void OutputReceiptMent()
    {
        // ��� ��µ��� ���ϱ� -> DoRotate or DoMove
    }
}