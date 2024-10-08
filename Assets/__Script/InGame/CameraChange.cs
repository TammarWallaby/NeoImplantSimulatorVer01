using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public bool toolsColliderIn;
    public bool surgeryColliderIn;

    public Camera mainCam;
    public Camera surgeryCam;
    public Camera toolsCam;

    public PlayerController playerController;
    public MainCamController mainCamController;
    Rigidbody playerRB;

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        surgeryCam.enabled = false;
        toolsCam.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&playerRB.velocity==Vector3.zero)
        {
            if (surgeryColliderIn)
            {
                mainCam.enabled = !mainCam.enabled;
                surgeryCam.enabled = !surgeryCam.enabled;
                if (mainCam.enabled)
                {
                    playerController.enabled = true;
                    mainCamController.enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else if (surgeryCam.enabled)
                {
                    playerController.enabled = false;
                    mainCamController.enabled = false;
                    Cursor.lockState = CursorLockMode.Confined;
                }
            }else if (toolsColliderIn)
            {
                mainCam.enabled = !mainCam.enabled;
                toolsCam.enabled = !toolsCam.enabled;
                if (mainCam.enabled)
                {
                    playerController.enabled = true;
                    mainCamController.enabled = true;
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else if (toolsCam.enabled)
                {
                    playerController.enabled = false;
                    mainCamController.enabled = false;
                    Cursor.lockState = CursorLockMode.Confined;
                }
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Patient")
        {
            surgeryColliderIn = true;
        }
        else if (other.tag == "Tools")
        {
            toolsColliderIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Patient")
        {
            surgeryColliderIn = false;
        }
        else if (other.tag == "Tools")
        {
            toolsColliderIn = false;
        }
    }
}
