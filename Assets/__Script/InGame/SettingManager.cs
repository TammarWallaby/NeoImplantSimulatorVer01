using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour
{
    public SettingsMenu settingPanel;
    public CameraChange cameraChange;
    public PlayerController playerController;
    public MainCamController mainCamController;

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
                    //surgeryCamController.enabled=false;
                }
                else if (cameraChange.toolsCam.enabled)
                {
                    //toolsCamController.enabled=false;
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
                    //surgeryCamController.enabled=true;
                    Cursor.visible = false;
                }
                else if (cameraChange.toolsCam.enabled)
                {
                    //toolsCamController.enabled=true;
                }
            }
        }
    }
}
