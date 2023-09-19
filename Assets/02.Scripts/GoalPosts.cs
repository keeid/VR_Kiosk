using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPosts : MonoBehaviour
{
    public GameObject ball = null;
    public GameObject confettiObj1 = null;
    public GameObject confettiObj2 = null;
    public float confettiTime = default;
    IEnumerator offConfetti = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ball.tag)
        {
            offConfetti = null;
            offConfetti = OffConfettiCo();
            StartCoroutine(offConfetti);
        }
    }

    IEnumerator OffConfettiCo()
    {
        confettiObj1.SetActive(false);
        confettiObj2.SetActive(false);
        confettiObj1.SetActive(true);
        confettiObj2.SetActive(true);
        yield return new WaitForSeconds(confettiTime);
        StopCoroutine(offConfetti);
    }
}
