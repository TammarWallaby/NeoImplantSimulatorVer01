/* Player가 있는 모든 씬의 Player에 들어갈 스크립트
 * 카메라 변경 기능(카메라 변경 조건 포함), 자연스러운 화면 전환
 * 수술 카메라로 변경 시 들고 있는 오브젝트 크기 조절
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraChange : MonoBehaviour
{
    public bool toolsColliderIn; 
    public bool surgeryColliderIn;
    public bool isSequencePlaying;
    public bool surgeryCameraChange = false;
    public bool toolCameraChange = false;

    public Camera mainCam; // 플레이어에 붙어있는 메인 카메라
    public Camera surgeryCam; // 환자 입쪽의 고정 카메라
    public Camera toolsCam; // 도구 쟁반 위쪽의 고정 카메라

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
    public GameObject heldAbutment;

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

                        heldTool.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
                        if (heldTool.transform.childCount != 0) // 손에 든 게 크라운일 경우에만 스케일 수정
                        {
                            if (heldTool.transform.GetChild(0).name == "ToolIncisorCrown" || heldTool.transform.GetChild(0).name == "ToolMolarCrown")
                            {
                                heldTool.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                            }
                        }
                        heldDrill.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);
                        heldAbutment.transform.localScale = new Vector3(0.07f, 0.07f, 0.07f);

                        heldTool.SetActive(false);
                        heldDrill.SetActive(false);
                        heldAbutment.SetActive(false);
                    })
                    .Append(mainCam.transform.DOMove(surgeryCam.transform.position, 2f))
                    .Join(mainCam.transform.DORotate(surgeryCam.transform.eulerAngles, 2f))
                    .AppendCallback(() =>
                    {
                        surgeryCameraChange = true;
                        mainCam.enabled = false;
                        surgeryCam.enabled = true;
                        Cursor.lockState = CursorLockMode.Confined;
                        Cursor.visible = false;
                        isSequencePlaying = false;
                        heldTool.SetActive(true);
                        heldDrill.SetActive(true);
                        heldAbutment.SetActive(true);
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
                        heldTool.SetActive(false);
                        heldDrill.SetActive(false);
                        heldAbutment.SetActive(false);
                    })
                    .Append(mainCam.transform.DOMove(mainCamPosition, 2f))
                    .Join(mainCam.transform.DORotate(mainCamRotation, 2f))
                    .AppendCallback(() =>
                    {
                        playerController.enabled = true;
                        mainCamController.enabled = true;
                        isSequencePlaying = false;
                        heldTool.SetActive(true);
                        heldDrill.SetActive(true);
                        heldAbutment.SetActive(true);
                        heldTool.transform.localScale = new Vector3(1f, 1f, 1f);
                        heldDrill.transform.localScale = new Vector3(1f, 1f, 1f);
                        heldAbutment.transform.localScale = new Vector3(1f, 1f, 1f);
                        heldTool.transform.localPosition = new Vector3(0.1f, -0.02f, 0.2f);
                        heldDrill.transform.localPosition = new Vector3(0.1f, -0.02f, 0.2f);
                        heldAbutment.transform.localPosition = new Vector3(0.1f, -0.02f, 0.2f);
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
                        toolCameraChange = true;
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
