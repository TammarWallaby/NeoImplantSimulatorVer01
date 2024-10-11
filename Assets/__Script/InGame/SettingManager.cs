/* Player가 있는 모든 씬의 SettingManager에 들어갈 스크립트
 * 설정창을 열었을 때, 카메라 기능 비활성화
 * 설정창을 열면 안되는 시점 추가
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public SettingsMenu settingPanel;
    public CameraChange cameraChange;
    public PlayerController playerController;
    public MainCamController mainCamController;
    public SurgeryCamController surgeryCamController;
    public ToolsCamController toolsCamController;

    public bool isSettingPanelOn;

    private void Awake()
    {
        settingPanel = GameObject.Find("SettingCanvas").GetComponent<SettingsMenu>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && cameraChange.isSequencePlaying == false)
        {
            settingPanel.ToggleSettingsPanel();
            isSettingPanelOn = !isSettingPanelOn;
            if (isSettingPanelOn)
            {
                Time.timeScale = 0;
                playerController.enabled = false;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                if (cameraChange.mainCam.enabled)
                {
                    mainCamController.enabled = false;
                }
                else if (cameraChange.surgeryCam.enabled)
                {
                    surgeryCamController.enabled=false;
                }
                else if (cameraChange.toolsCam.enabled)
                {
                    toolsCamController.enabled=false;
                }
            }
            else
            {
                Time.timeScale = 1;
                if (cameraChange.mainCam.enabled)
                {
                    mainCamController.enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                    playerController.enabled = true;
                }
                else if (cameraChange.surgeryCam.enabled)
                {
                    surgeryCamController.enabled=true;
                    Cursor.visible = false;
                }
                else if (cameraChange.toolsCam.enabled)
                {
                    toolsCamController.enabled=true;
                }
            }
        }
    }
}
