using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Product"))
        {
            var product = other.GetComponent<Product>();
            if (!product.isInCart)
            {
                product.isInCart = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Product"))
        {
            var product = other.GetComponent<Product>();
            if (product.canInCart && product.isGrabed)
            {
                product.isInCart = false;
                product.canInCart = false;
                MissionManager.Instance.RemoveProduct(other);
                Debug.Log($"{product.transform.name} ³ª°¨");
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Product"))
        {
            var product = other.GetComponent<Product>();
            if (product.isGrabed)
            {
                product.canInCart = true;
            }
            else
            {
                if (product.canInCart)
                {
                    product.transform.parent = transform;
                    product.transform.localEulerAngles = Vector3.zero;
                    MissionManager.Instance.AddProcut(other);
                    product.transform.localPosition = new Vector3(product.transform.localPosition.x, 0.05f, product.transform.localPosition.z);
                    product.transform.GetComponent<Rigidbody>().isKinematic = true;
                    Debug.Log($"{product.transform.name} µé¾î¿È");
                    other.GetComponent<Product>().canInCart = false;
                }
                else
                {
                    other.GetComponent<Product>().canInCart = false;

                }
            }
        }

    }

}
