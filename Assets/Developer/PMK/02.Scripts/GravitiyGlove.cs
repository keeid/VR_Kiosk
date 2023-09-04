using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GravitiyGlove : MonoBehaviour
{
    public GameObject obj;
    public Sequence sequence; // Dotween Sequence�� �ߴ��Ϸ��� �ش� ������ �����Ͽ� sequence.Kill();�� ���ָ� ��

    // �繰 �������
    public void StartAttractObject()
    {
        sequence = DOTween.Sequence();
        sequence.Append(obj.transform.DOMove(transform.position, 3).SetEase(Ease.Linear));
        sequence.Insert(0, obj.transform.DORotate(transform.eulerAngles, 3).SetEase(Ease.Linear));
        sequence.OnComplete(SetObjToHand);
        sequence.Play();
    }

    // �繰 ������� ���
    public void StopAttractObject()
    {
        if(sequence != null && sequence.IsPlaying() && sequence.IsActive())
        {
            sequence.Kill();
        }
    }

    // �繰 �տ� �����ϱ�
    private void SetObjToHand()
    {
        // CJW �ڵ� ����
    }
}
