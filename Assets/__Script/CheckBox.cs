/*
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBox : MonoBehaviour
{
    public Toggle movecheckBox; // 플레이어의 위치가 목표에 도달했을 때 체크되는 체크박스
    public Toggle cameracheckBox; // 카메라가 변경될 때 체크되는 체크박스
    public Toggle pickAnesthesiaCheckBox; // 도구를 클릭했을 때 체크되는 체크박스
    public Toggle dropAnesthesiaCheckBox; // 도구를 내려놓았을 때 체크박스
    public Toggle mousecheckBox; // 카메라가 변경 될 때 체크되는 체크박스
    public Toggle anesthesiaCheckBox; // 
    public Transform player; // 플레이어의 Transform 컴포넌트
    public Vector3 targetPosition = new Vector3(-0.9f, 1f, -1.8f); // 목표 위치

    private Camera currentCamera; // 현재 활성화된 카메라
    private bool isToolPicked = false; // 도구가 클릭되었는지 여부를 추적하는 변수
    private Camera mainCamera; // 메인 카메라
    private Camera toolsCamera; // 도구 카메라
    private Camera surgeryCamera; // 수술 카메라

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        toolsCamera = GameObject.Find("ToolsCamera").GetComponent<Camera>();
        surgeryCamera = GameObject.Find("SurgeryCamera").GetComponent<Camera>();

        // 시작 시 활성화된 카메라 저장
        currentCamera = Camera.main;

        movecheckBox.isOn = false; // 체크 해제
        cameracheckBox.isOn = false;
        pickAnesthesiaCheckBox.isOn = false;
        dropAnesthesiaCheckBox.isOn = false;
        mousecheckBox.isOn = false;
        anesthesiaCheckBox.isOn = false;
    }
    private void Update()
    {
        // 플레이어의 위치가 목표 위치와 가까울 때 체크박스 체크
        if (!movecheckBox.isOn && Vector3.Distance(player.position, targetPosition) < 0.1f)
        {
            movecheckBox.isOn = true; // 체크박스를 체크 상태로 변경
        }





        // 카메라 변경 감지
        Camera newCamera = Camera.main; // 현재 메인 카메라 가져오기
        if (newCamera != null && newCamera == toolsCamera)
        {
            // 새로운 카메라가 ToolsCamera인 경우
            if (currentCamera != toolsCamera)
            {
                currentCamera = newCamera; // 새 카메라로 업데이트
                cameracheckBox.isOn = true; // 체크박스를 체크 상태로 변경
                Debug.Log("카메라가 변경되었습니다."); // 디버그 메시지 추가
            }
        }
        if (newCamera != null && newCamera == surgeryCamera)
        {
            // 새로운 카메라가 surgeryCamera 경우
            if (currentCamera != surgeryCamera)
            {
                currentCamera = newCamera; // 새 카메라로 업데이트
                mousecheckBox.isOn = true; // 체크박스를 체크 상태로 변경
                Debug.Log("카메라가 변경되었습니다."); // 디버그 메시지 추가
            }
        }
    }

}