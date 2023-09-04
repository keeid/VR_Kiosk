using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GravitiyGlove : MonoBehaviour
{
    public GameObject obj;
    public Sequence sequence; // Dotween Sequence로 중단하려면 해당 변수를 참조하여 sequence.Kill();을 해주면 됨

    // 사물 끌어당기기
    public void StartAttractObject()
    {
        sequence = DOTween.Sequence();
        sequence.Append(obj.transform.DOMove(transform.position, 3).SetEase(Ease.Linear));
        sequence.Insert(0, obj.transform.DORotate(transform.eulerAngles, 3).SetEase(Ease.Linear));
        sequence.OnComplete(SetObjToHand);
        sequence.Play();
    }

    // 사물 끌어당기기 취소
    public void StopAttractObject()
    {
        if(sequence != null && sequence.IsPlaying() && sequence.IsActive())
        {
            sequence.Kill();
        }
    }

    // 사물 손에 정착하기
    private void SetObjToHand()
    {
        // CJW 코드 참조
    }
}
