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

    private ToolsCamController toolsCamController; // ToolsCamController 참조
    private SurgeryCamController surgeryCamController; // SurgeryCamController 참조
    private CameraChange cameraChange;

    private void Start()
    {
        movecheckBox.isOn = false; // 체크 해제
        cameracheckBox.isOn = false;
        pickAnesthesiaCheckBox.isOn = false;
        dropAnesthesiaCheckBox.isOn = false;
        mousecheckBox.isOn = false;
        anesthesiaCheckBox.isOn = false;

        toolsCamController = FindObjectOfType<ToolsCamController>();
        surgeryCamController = FindObjectOfType<SurgeryCamController>();
        cameraChange = FindObjectOfType<CameraChange>();
    }

    private void Update()
    {
        // 플레이어의 위치가 목표 위치와 가까울 때 체크박스 체크
        if (!movecheckBox.isOn && Vector3.Distance(player.position, targetPosition) < 0.1f)
        {
            movecheckBox.isOn = true; // 체크박스를 체크 상태로 변경
        }
        // 도구가 집었을 때 pickAnesthesiaCheckBox 체크
        if (toolsCamController.toolPicked && !pickAnesthesiaCheckBox.isOn)
        {
            pickAnesthesiaCheckBox.isOn = true;
        }

        // 도구가 내려졌을 때 dropAnesthesiaCheckBox 체크
        if (toolsCamController.toolDropped && !dropAnesthesiaCheckBox.isOn)
        {
            dropAnesthesiaCheckBox.isOn = true;
        }
        if (surgeryCamController.playTool && !anesthesiaCheckBox.isOn)
        {
            anesthesiaCheckBox.isOn = true;
        }

        if (cameraChange.toolCameraChange && !cameracheckBox.isOn)
        {
            cameracheckBox.isOn = true;
        }

        if (cameraChange.surgeryCameraChange && !mousecheckBox.isOn)
        {
            mousecheckBox.isOn = true;
        }
    }
}
