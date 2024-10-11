/* Player가 있는 모든 씬의 MainCamera에 들어갈 스크립트
 * 메인카메라 시점 및 감도 조작 기능
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamController : MonoBehaviour
{
    SettingsData settingsData;

    public float mouseSpeed; // 회전속도, 설정 필수
    public float upLimit; // 위쪽 한계값, 설정 필수
    public float downLimit; // 아래쪽 한계값, 설정 필수

    private float mouseX; //좌우 회전값
    private float mouseY; //위아래 회전값

    private void Awake()
    {
        settingsData=GameObject.Find("SettingsData").GetComponent<SettingsData>();
    }

    void Start()
    {
        // 마우스를 중앙에 고정하고 숨김
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mouseSpeed = settingsData.mouseSensitivity;

        //마우스 회전 처리
        mouseX += Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
        mouseY += Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;

        mouseY = Mathf.Clamp(mouseY, downLimit, upLimit);
        this.transform.localEulerAngles = new Vector3(-mouseY, mouseX, 0);
    }

    public Vector3 GetForwardDirection()
    {
        return transform.forward;
    }

    public Vector3 GetRightDirection()
    {
        return transform.right;
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        mouseSpeed = sensitivity; // 슬라이더 값으로 마우스 감도 설정
    }
}
