using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("��ġ ����");
        if (collision.transform.GetComponent<Product>().isInCart == true)
        {
            Debug.Log("��ġ ����");
            collision.rigidbody.isKinematic = true;
            collision.transform.position = transform.parent.GetChild(0).position;
        }
    }
}
