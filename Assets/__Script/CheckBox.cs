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
    public Transform player; // 플레이어의 Transform 컴포넌트
    public Vector3 targetPosition = new Vector3(-0.9f, 1f, -1.8f); // 목표 위치

    private Camera currentCamera; // 현재 활성화된 카메라
    private bool isToolPicked = false; // 도구가 클릭되었는지 여부를 추적하는 변수
    private Camera mainCamera; // 메인 카메라
    private Camera toolsCamera; // 도구 카메라

    private void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        toolsCamera = GameObject.Find("ToolsCamera").GetComponent<Camera>();
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

            // HandleToolInteraction 호출
            HandleToolInteraction();
        }
        else
        {
            // 카메라가 ToolsCamera가 아닌 경우 초기화
            if (currentCamera == toolsCamera)
            {
                cameracheckBox.isOn = false; // 체크 해제
                currentCamera = null; // 현재 카메라 초기화
            }
        }
    }

    private void HandleToolInteraction()
    {
        // 도구 클릭 및 내려놓기 동작이 가능
        if (currentCamera == toolsCamera)
        {
            // 도구 클릭 체크 (좌클릭)
            if (Input.GetMouseButtonDown(0) && !isToolPicked)
            {
                // 현재 카메라 기준으로 마우스 위치에서 레이 생성
                Ray ray = currentCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // 레이캐스트를 사용하여 클릭된 오브젝트 감지
                if (Physics.Raycast(ray, out hit))
                {
                    // "Tools" 태그가 붙은 오브젝트를 클릭했을 때
                    if (hit.transform.CompareTag("Tools"))
                    {
                        pickAnesthesiaCheckBox.isOn = true; // 도구 클릭 시 체크박스를 체크 상태로 변경
                        isToolPicked = true; // 도구 클릭 상태 설정
                        Debug.Log("도구가 클릭되었습니다.");
                    }
                }
            }

            // 도구 내려놓기 체크 (F키)
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (isToolPicked) // 도구가 선택된 상태일 때만 실행
                {
                    dropAnesthesiaCheckBox.isOn = true; // 도구를 내려놓은 경우 체크박스 활성화
                    isToolPicked = false; // 도구 클릭 상태 해제
                    Debug.Log("HandleToolInteraction: Tool dropped");
                }
                else
                {
                    Debug.Log("도구가 선택되어 있지 않습니다."); // 도구가 선택되지 않았을 때의 디버그 메시지
                }
            }
        }
    }
}