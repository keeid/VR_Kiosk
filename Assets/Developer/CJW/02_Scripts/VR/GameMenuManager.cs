using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMenuManager : MonoBehaviour
{
    public Transform head;
    public float spawnDistance = 2;
    public GameObject menu;
    public InputActionProperty showBtn;

    public void Update()
    {
        // x��ư�� ������ �Լ� ����
        if (showBtn.action.WasPressedThisFrame())
        {
            Debug.Log("����");
            menu.SetActive(!menu.activeSelf);

            menu.transform.position = head.position + new Vector3(head.forward.x, 0.2f, head.forward.z).normalized * spawnDistance;
        }

        // UI�� �ڽ��� �������� �ٶ󺸵���
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        // �¿����
        menu.transform.forward *= -1;
    }
}
