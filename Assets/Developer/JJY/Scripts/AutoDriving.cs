using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDriving : MonoBehaviour
{
    public GameObject myCar;
    public static int numPoints = 3;
    public Transform[] wayPoints = new Transform[numPoints];
    public Transform origin, anchor;
    Vector3 curPos, next;
    float speed = 1.5f;
    float rotationSpeed = 0.8f;
    int i = 0;    

    void Start()
    {
        curPos = transform.position;
    }

    void Update()
    {

        if (i < numPoints)
        {
            next = wayPoints[i].position;
            Vector3 dir = (next - transform.position).normalized;

            if (Vector3.Distance(transform.position, next) > 0.2f)
            {
                curPos += dir * Time.deltaTime * speed;
                transform.position = curPos;

                Quaternion targetRotation = Quaternion.LookRotation(next - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            }        
            else
            {
                i++;
                curPos = next;
            }
        }    
        else
        {
            origin.parent = null;
            origin.position = anchor.position;
            origin.rotation = anchor.rotation;
            myCar.SetActive(true);
            Destroy(this.gameObject);
            Destroy(GetComponent<AutoDriving>());
        }        
    }
}
