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
        // x버튼을 누르면 함수 실행
        if (showBtn.action.WasPressedThisFrame())
        {
            Debug.Log("눌림");
            menu.SetActive(!menu.activeSelf);

            menu.transform.position = head.position + new Vector3(head.forward.x, 0.2f, head.forward.z).normalized * spawnDistance;
        }

        // UI가 자신의 방향으로 바라보도록
        menu.transform.LookAt(new Vector3(head.position.x, menu.transform.position.y, head.position.z));
        // 좌우반전
        menu.transform.forward *= -1;
    }
}
