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
    public Toggle dropAnesthesiaCheckBox; // 도구를 내려놓았을 때 체크되는 체크박스
    public Transform player; // 플레이어의 Transform 컴포넌트
    public Vector3 targetPosition = new Vector3(-0.9f, 1f, -1.8f); // 목표 위치

    private Camera currentCamera; // 현재 활성화된 카메라

    private void Start()
    {
        // 시작 시 활성화된 카메라 저장
        currentCamera = Camera.main;
        movecheckBox.isOn = false; // 체크 해제
        cameracheckBox.isOn = false; // 체크 해제
        pickAnesthesiaCheckBox.isOn = false; // 체크 해제
        dropAnesthesiaCheckBox.isOn = false; // 체크 해제
    }

    private void Update()
    {
        // 플레이어의 위치가 목표 위치와 가까울 때 체크박스 체크
        if (!movecheckBox.isOn && Vector3.Distance(player.position, targetPosition) < 0.1f) // 일정 거리 이내일 때
        {
            movecheckBox.isOn = true; // 체크박스를 체크 상태로 변경
        }

        // 카메라 변경 감지
        if (Camera.main != currentCamera)
        {
            currentCamera = Camera.main; // 새 카메라로 업데이트
            CheckCameraChange(); // 카메라가 변경되었을 때 체크박스 체크
        }

        // 도구 클릭 감지
        if (Input.GetMouseButtonDown(0)) // 좌클릭
        {
            DetectToolClick(); // 도구 클릭 감지
        }

        // F 키를 눌렀을 때 도구 내려놓기
        if (Input.GetKeyDown(KeyCode.F))
        {
            DropTool(); // 도구 내려놓기
        }
    }

    // 카메라가 변경되었을 때 호출되는 함수
    private void CheckCameraChange()
    {
        cameracheckBox.isOn = true; // 체크박스를 체크 상태로 변경
    }

    // 도구 클릭 감지 함수
    private void DetectToolClick()
    {
    // 현재 카메라가 null인지 확인
    if (currentCamera == null)
    {
        Debug.LogError("현재 카메라가 null입니다. 카메라를 설정하세요.");
        return; // 카메라가 null인 경우 메서드 종료
    }

    // 현재 카메라 기준으로 레이 생성
    Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

    if (Physics.Raycast(ray, out hit))
    {
        // "Tool" 태그가 붙은 오브젝트를 클릭했을 때
        if (hit.transform.CompareTag("ToolsStetting"))
        {
            pickAnesthesiaCheckBox.isOn = true; // 도구 클릭 시 체크박스를 체크 상태로 변경
            Debug.Log("도구가 클릭되었습니다.");
        }
    }
    }

    // 도구 내려놓기 함수
    private void DropTool()
    {
        dropAnesthesiaCheckBox.isOn = true; // 도구 내려놓을 때 체크박스를 체크 상태로 변경
        Debug.Log("도구가 내려놓아졌습니다.");
    }
}
