using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public Camera camera1; // 첫 번째 카메라
    public Camera camera2; // 두 번째 카메라
    public GameObject aim; // 에임
    public GameObject settingPanel;

    public Bellboy bellboy;
    public PlayerControl playerControl;
    public MainCameraControl mainCameraControl;

    public Vector3 mousePosition;

    private void Start()
    {
        // 시작할 때 첫 번째 카메라만 활성화
        camera1.enabled = true;
        camera2.enabled = false;
    }

    private void Update()
    {
        // 스페이스바를 눌렀을 때 카메라 전환
        if (Input.GetKeyDown(KeyCode.Space) && bellboy.isPlayerIn && settingPanel.activeSelf == false)
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        // 현재 활성화된 카메라를 비활성화하고 다른 카메라를 활성화
        camera1.enabled = !camera1.enabled;
        camera2.enabled = !camera2.enabled;
        if (camera1.enabled)
        {
            aim.SetActive(true);
            playerControl.enabled = true;
            mainCameraControl.enabled = true;
            Cursor.visible = false;
            //CursorControl.SetPosition(mousePosition);
        }
        else
        {
            aim.SetActive(false);
            playerControl.enabled = false;
            mainCameraControl.enabled = false;
            Cursor.visible = true;
            mousePosition = Input.mousePosition;
        }
    }
}
