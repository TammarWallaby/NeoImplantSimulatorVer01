using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraChange : MonoBehaviour
{
    public bool toolsColliderIn;
    public bool surgeryColliderIn;
    public bool isSequencePlaying;

    public Camera mainCam;
    public Camera surgeryCam;
    public Camera toolsCam;

    public PlayerController playerController;
    public MainCamController mainCamController;
    Rigidbody playerRB;

    public Sequence mainToSurgerySequence;
    public Sequence surgeryToMainSequence;
    public Sequence mainToToolsSequence;
    public Sequence toolsToMainSequence;

    public Vector3 mainCamPosition;
    public Vector3 mainCamRotation;

    public GameObject heldTool;
    public GameObject heldDrill;

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
        if (Input.GetKeyDown(KeyCode.Space)&&playerRB.velocity==Vector3.zero&&isSequencePlaying==false)
        {
            if (surgeryColliderIn)
            {
                if (mainCam.enabled)
                {
                    mainCamPosition= mainCam.transform.position;
                    mainCamRotation = mainCam.transform.eulerAngles;
                    mainToSurgerySequence = DOTween.Sequence()
                    .AppendCallback(() =>
                    {
                        isSequencePlaying = true;
                        playerController.enabled = false;
                        mainCamController.enabled = false;
                        heldTool.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        heldDrill.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        //heldTool.SetActive(false);
                        //heldDrill.SetActive(false);
                    })
                    .Append(mainCam.transform.DOMove(surgeryCam.transform.position, 2f))
                    .Join(mainCam.transform.DORotate(surgeryCam.transform.eulerAngles, 2f))
                    .AppendCallback(() =>
                    {
                        mainCam.enabled = false;
                        surgeryCam.enabled = true;
                        Cursor.lockState = CursorLockMode.Confined;
                        Cursor.visible = true;
                        isSequencePlaying = false;
                    });
                }
                else if (surgeryCam.enabled)
                {
                    surgeryToMainSequence = DOTween.Sequence()
                    .AppendCallback(() =>
                    {
                        isSequencePlaying = true;
                        surgeryCam.enabled = false;
                        mainCam.enabled = true;
                        Cursor.lockState = CursorLockMode.Locked;
                    })
                    .Append(mainCam.transform.DOMove(mainCamPosition, 2f))
                    .Join(mainCam.transform.DORotate(mainCamRotation, 2f))
                    .AppendCallback(() =>
                    {
                        playerController.enabled = true;
                        mainCamController.enabled = true;
                        isSequencePlaying = false;
                        //heldTool.SetActive(true);
                        //heldDrill.SetActive(true);
                        heldTool.transform.localScale = new Vector3(1f, 1f, 1f);
                        heldDrill.transform.localScale = new Vector3(1f, 1f, 1f);
                        heldTool.transform.localPosition = new Vector3(0.1f, -0.02f, 0.2f);
                        heldDrill.transform.localPosition = new Vector3(0.1f, -0.02f, 0.2f);
                    });
                }
            }
            else if (toolsColliderIn)
            {
                if (mainCam.enabled)
                {
                    mainCamPosition = mainCam.transform.position;
                    mainCamRotation = mainCam.transform.eulerAngles;
                    mainToToolsSequence = DOTween.Sequence()
                    .AppendCallback(() =>
                    {
                        isSequencePlaying = true;
                        playerController.enabled = false;
                        mainCamController.enabled = false;
                    })
                    .Append(mainCam.transform.DOMove(toolsCam.transform.position, 2f))
                    .Join(mainCam.transform.DORotate(toolsCam.transform.eulerAngles, 2f))
                    .AppendCallback(() =>
                    {
                        mainCam.enabled = false;
                        toolsCam.enabled = true;
                        Cursor.lockState = CursorLockMode.Confined;
                        Cursor.visible = true;
                        isSequencePlaying = false;
                    });
                }
                else if (toolsCam.enabled)
                {
                    toolsToMainSequence = DOTween.Sequence()
                    .AppendCallback(() =>
                    {
                        isSequencePlaying = true;
                        toolsCam.enabled = false;
                        mainCam.enabled = true;
                        Cursor.lockState = CursorLockMode.Locked;
                    })
                    .Append(mainCam.transform.DOMove(mainCamPosition, 2f))
                    .Join(mainCam.transform.DORotate(mainCamRotation, 2f))
                    .AppendCallback(() =>
                    {
                        playerController.enabled = true;
                        mainCamController.enabled = true;
                        isSequencePlaying = false;
                    });
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
        else if (other.tag == "ToolsSetting")
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
        else if (other.tag == "ToolsSetting")
        {
            toolsColliderIn = false;
        }
    }
}
