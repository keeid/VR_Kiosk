using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPosts : MonoBehaviour
{
    public GameObject ball = null;
    public GameObject confettiObj = null;
    public float confettiTime = default;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ball.tag)
        {
            CancelInvoke(); // ¹®Á¦ µÇ¸é »©±â
            confettiObj.SetActive(true);
            Invoke(nameof(OffConfetti), confettiTime);
        }
    }

    // Confetti ²ô±â
    private void OffConfetti()
    {
        confettiObj.SetActive(false);
    }
}
