/* Player가 있는 모든 씬의 SurgeryCamera에 들어갈 스크립트
 * 온갖 수술 관련 기능 추가 예정
 * 마우스커서에 도구가 따라오게 하는 기능
 * 수술 애니메이션(시퀀스)
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class SurgeryCamController : MonoBehaviour
{
    public CameraChange cameraChange;
    public Camera surgeryCam;
    Vector3 mousePosition;
    Vector3 mouseWorldPosition;
    public float distanceFromCamera; // 수술캠과 도구와의 거리, 설정 필수

    public bool isSequencePlaying;

    public float rayRange;

    public bool playTool = false;
    public bool gameOver = false;
    public bool gameClear = false;

    public enum SurgeryState
    {
        Anesthesia,
        IncisionOne,
        IncisionTwo,
        IncisionThree,
        ElevationOne,
        ElevationTwo,
        DrillSmall,
        DrillMedium,
        DrillLarge,
        FixturePlace,
        WrenchWithFixture,
        HealingAbutmentPlace,
        SutureOne,
        SutureTwo,
        SutureThree,
        HealingAbutmentRemove,
        AbutmentPlace,
        CrownPlace,
        Clear
    }
    public SurgeryState currentState;

    public GameObject dientesOne;
    public GameObject dientesTwo;
    public GameObject dientesThree;
    public GameObject dientesFour;

    public GameObject dientesGums;
    public GameObject dientesGumsOne;
    public GameObject dientesGumsTwo;
    public GameObject dientesGumsThree;
    public GameObject dientesGumsFour;
    public GameObject dientesGumsFive;

    public GameObject surgicalSutureOne;
    public GameObject surgicalSutureTwo;
    public GameObject surgicalSutureThree;
    public GameObject surgicalSutureFour;
    public GameObject surgicalSutureFive;
    public GameObject surgicalSutureSix;
    public GameObject surgicalSutureSeven;
    public GameObject surgicalSutureEight;
    public GameObject surgicalSutureNine;
    public GameObject surgicalSutureTen;

    public GameObject fixture;
    public GameObject healingAbutment;
    public GameObject abutment;
    public GameObject crown;

    public GameObject syringePusher;

    GameObject heldTool; // Player \ MainCamera \ heldTool이 아닌 이 heldTool의 Child 오브젝트
    GameObject heldDrill; // 위와 동일
    GameObject heldAbutment; // 위와 동일

    private void Start()
    {
        currentState = SurgeryState.Anesthesia;
    }

    private void Update()
    {
        if (surgeryCam.enabled)
        { 
            mousePosition=Input.mousePosition;
            mousePosition.z = distanceFromCamera;

            mouseWorldPosition=surgeryCam.ScreenToWorldPoint(mousePosition);

            cameraChange.heldTool.transform.position = mouseWorldPosition;
            cameraChange.heldDrill.transform.position= mouseWorldPosition;
            cameraChange.heldAbutment.transform.position = mouseWorldPosition;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = surgeryCam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, rayRange))
                {
                    if (hit.collider.CompareTag("SurgicalSite"))
                    {
                        HasHeldTool();

                        if (heldTool!= null)
                        {
                            if(heldTool.name=="ToolHandpiece")
                            {
                                HasHeldDrill();
                            }
                            else if (heldTool.name == "ToolDriver")
                            {
                                HasHeldAbutment();
                            }
                            switch (currentState) 
                            {
                                case SurgeryState.Anesthesia:
                                    if (heldTool.name == "ToolSyringe")
                                    {
                                        Sequence anethesiaSequence = DOTween.Sequence()
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = false;
                                                cameraChange.enabled = false;
                                                isSequencePlaying = true;
                                                Cursor.lockState = CursorLockMode.Locked;
                                            })
                                            .Append(syringePusher.transform.DOLocalMoveZ(0.08f, 3f))
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = true;
                                                cameraChange.enabled=true;
                                                isSequencePlaying = false;
                                                Cursor.lockState = CursorLockMode.Confined;

                                                currentState = SurgeryState.IncisionOne;
                                                playTool = true;
                                            });
                                    }
                                    else
                                    {
                                        // 실패 UI
                                        gameOver = true;
                                    }
                                    break;
                                case SurgeryState.IncisionOne:
                                    if (heldTool.name == "ToolScalpel")
                                    {
                                        Sequence incisionOneSequence = DOTween.Sequence()
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = false;
                                                cameraChange.enabled = false;
                                                isSequencePlaying = true;
                                                Cursor.lockState = CursorLockMode.Locked;
                                            })
                                            .Append(heldTool.transform.DOLocalMove(new Vector3(0.013f, 0.032f, 0.006f), 1f))
                                            .Join(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(110, 50, 0), 1f))
                                            .Append(heldTool.transform.DOLocalMove(new Vector3(-0.013f, -0.032f, -0.006f), 2f))
                                            .Append(heldTool.transform.DOLocalMove(Vector3.zero,1f))
                                            .Join(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity,1f))
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = true;
                                                cameraChange.enabled = true;
                                                isSequencePlaying = false;
                                                Cursor.lockState = CursorLockMode.Confined;

                                                dientesGums.SetActive(false);
                                                dientesGumsOne.SetActive(true);

                                                currentState = SurgeryState.IncisionTwo;
                                            });
                                    }
                                    else
                                    {
                                        gameOver = true;
                                    }
                                    break;
                                case SurgeryState.IncisionTwo:
                                    if(heldTool.name=="ToolScalpel")
                                    {
                                        Sequence incisionTwoSequence = DOTween.Sequence()
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = false;
                                                cameraChange.enabled = false;
                                                isSequencePlaying = true;
                                                Cursor.lockState = CursorLockMode.Locked;
                                            })
                                            .Append(heldTool.transform.DOLocalMove(new Vector3(-0.02f, 0.01f, -0.008f), 1f))
                                            .Join(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(11, 0, -18), 1f))
                                            .Append(heldTool.transform.DOLocalMove(new Vector3(0.02f, -0.01f, 0.008f), 2f))
                                            .Append(heldTool.transform.DOLocalMove(Vector3.zero, 1f))
                                            .Join(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 1f))
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = true;
                                                cameraChange.enabled = true;
                                                isSequencePlaying = false;
                                                Cursor.lockState = CursorLockMode.Confined;

                                                dientesGumsOne.SetActive(false);
                                                dientesGumsTwo.SetActive(true);

                                                currentState = SurgeryState.IncisionThree;
                                            });
                                    }
                                    else
                                    {
                                        gameOver = true;
                                    }
                                    break;
                                case SurgeryState.IncisionThree:
                                    if (heldTool.name == "ToolScalpel")
                                    {
                                        Sequence incisionThreeSequence = DOTween.Sequence()
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = false;
                                                cameraChange.enabled = false;
                                                isSequencePlaying = true;
                                                Cursor.lockState = CursorLockMode.Locked;
                                            })
                                            .Append(heldTool.transform.DOLocalMove(new Vector3(-0.02f, 0.01f, -0.008f), 1f))
                                            .Join(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(11, 0, -18), 1f))
                                            .Append(heldTool.transform.DOLocalMove(new Vector3(0.02f, -0.01f, 0.008f), 2f))
                                            .Append(heldTool.transform.DOLocalMove(Vector3.zero, 1f))
                                            .Join(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 1f))
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = true;
                                                cameraChange.enabled = true;
                                                isSequencePlaying = false;
                                                Cursor.lockState = CursorLockMode.Confined;

                                                dientesGumsTwo.SetActive(false);
                                                dientesGumsThree.SetActive(true);

                                                currentState = SurgeryState.ElevationOne;                                           
                                            });
                                    }
                                    else
                                    {
                                        gameOver = true;
                                    }
                                    break;
                                case SurgeryState.ElevationOne:
                                    if (heldTool.name == "ToolPeriostealElevator")
                                    {
                                        Sequence elevationOneSequence = DOTween.Sequence()
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = false;
                                                cameraChange.enabled = false;
                                                isSequencePlaying = true;
                                                Cursor.lockState = CursorLockMode.Locked;
                                            })
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(105, 0, -45), 1f))
                                            .Append(heldTool.transform.DOLocalMove(new Vector3(0.033f, -0.0162f, 0.0133f), 1f))
                                            .AppendCallback(() =>
                                            {
                                                dientesGumsThree.SetActive(false);
                                                dientesGumsFour.SetActive(true);
                                            })
                                            .Append(heldTool.transform.DOLocalMove(Vector3.zero, 1f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 1f))
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = true;
                                                cameraChange.enabled = true;
                                                isSequencePlaying = false;
                                                Cursor.lockState = CursorLockMode.Confined;

                                                currentState = SurgeryState.ElevationTwo;
                                            });
                                    }
                                    else
                                    {
                                        gameOver = true;
                                    }
                                    break;
                                case SurgeryState.ElevationTwo:
                                    if (heldTool.name == "ToolPeriostealElevator")
                                    {
                                        Sequence elevationTwoSequence = DOTween.Sequence()
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = false;
                                                cameraChange.enabled = false;
                                                isSequencePlaying = true;
                                                Cursor.lockState = CursorLockMode.Locked;
                                            })
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(105, 0, -45), 1f))
                                            .Append(heldTool.transform.DOLocalMove(new Vector3(-0.033f, 0.0162f, -0.0133f), 1f))
                                            .AppendCallback(() =>
                                            {
                                                dientesGumsFour.SetActive(false);
                                                dientesGumsFive.SetActive(true);
                                            })
                                            .Append(heldTool.transform.DOLocalMove(Vector3.zero, 1f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 1f))
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = true;
                                                cameraChange.enabled = true;
                                                isSequencePlaying = false;
                                                Cursor.lockState = CursorLockMode.Confined;

                                                currentState = SurgeryState.DrillSmall;
                                            });
                                    }
                                    else
                                    {
                                        gameOver = true;
                                    }
                                    break;
                                case SurgeryState.DrillSmall:
                                    if (heldTool.name == "ToolHandpiece" && heldDrill != null)
                                    {
                                        if (heldDrill.name == "ToolDrill2")
                                        {
                                            Sequence drillSmallSequence = DOTween.Sequence()
                                                .AppendCallback(() =>
                                                {
                                                    this.enabled = false;
                                                    cameraChange.enabled = false;
                                                    isSequencePlaying = true;
                                                    Cursor.lockState = CursorLockMode.Locked;
                                                })
                                                .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(115, 80, 30), 2f))
                                                .Join(heldDrill.transform.DOLocalRotateQuaternion(Quaternion.Euler(-30, 5, 0), 2f))
                                                .AppendCallback(() =>
                                                {
                                                    // 드릴 소리
                                                })
                                                .Append(heldTool.transform.DOLocalMove(new Vector3(0.04f, -0.0074f, -0.0462f), 3f))
                                                .Join(heldDrill.transform.DOLocalMove(new Vector3(0.0156f, 0.0292f, 0.0521f), 3f))
                                                .AppendCallback(() =>
                                                {
                                                    dientesOne.SetActive(false);
                                                    dientesTwo.SetActive(true);
                                                })
                                                .Append(heldTool.transform.DOLocalMove(Vector3.zero, 2f))
                                                .Join(heldDrill.transform.DOLocalMove(Vector3.zero, 2f))
                                                .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 2f))
                                                .Join(heldDrill.transform.DOLocalRotateQuaternion(Quaternion.identity, 2f))
                                                .AppendCallback(() =>
                                                {
                                                    this.enabled = true;
                                                    cameraChange.enabled = true;
                                                    isSequencePlaying = false;
                                                    Cursor.lockState = CursorLockMode.Confined;

                                                    currentState = SurgeryState.DrillMedium;
                                                });
                                        }
                                        else
                                        {
                                            gameOver = true;
                                        }
                                    }
                                    else
                                    {
                                        gameOver = true;
                                    }
                                    break;
                                case SurgeryState.DrillMedium:
                                    if (heldTool.name == "ToolHandpiece" && heldDrill != null)
                                    {
                                        if (heldDrill.name == "ToolDrill3")
                                        {
                                            Sequence drillMediumSequence = DOTween.Sequence()
                                                .AppendCallback(() =>
                                                {
                                                    this.enabled = false;
                                                    cameraChange.enabled = false;
                                                    isSequencePlaying = true;
                                                    Cursor.lockState = CursorLockMode.Locked;
                                                })
                                                .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(115, 80, 30), 2f))
                                                .Join(heldDrill.transform.DOLocalRotateQuaternion(Quaternion.Euler(-30, 5, 0), 2f))
                                                .AppendCallback(() =>
                                                {
                                                    // 드릴 소리
                                                })
                                                .Append(heldTool.transform.DOLocalMove(new Vector3(0.04f, -0.0074f, -0.0462f), 3f))
                                                .Join(heldDrill.transform.DOLocalMove(new Vector3(0.0156f, 0.0292f, 0.0521f), 3f))
                                                .AppendCallback(() =>
                                                {
                                                    dientesTwo.SetActive(false);
                                                    dientesThree.SetActive(true);
                                                })
                                                .Append(heldTool.transform.DOLocalMove(Vector3.zero, 2f))
                                                .Join(heldDrill.transform.DOLocalMove(Vector3.zero, 2f))
                                                .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 2f))
                                                .Join(heldDrill.transform.DOLocalRotateQuaternion(Quaternion.identity, 2f))
                                                .AppendCallback(() =>
                                                {
                                                    this.enabled = true;
                                                    cameraChange.enabled = true;
                                                    isSequencePlaying = false;
                                                    Cursor.lockState = CursorLockMode.Confined;

                                                    currentState = SurgeryState.DrillLarge;
                                                });
                                        }
                                        else
                                        {
                                            gameOver = true;
                                        }
                                    }
                                    else
                                    {
                                        gameOver = true;
                                    }
                                    break;
                                case SurgeryState.DrillLarge:
                                    if (heldTool.name == "ToolHandpiece" && heldDrill != null)
                                    {
                                        if (heldDrill.name == "ToolDrill4")
                                        {
                                            Sequence drillLargeSequence = DOTween.Sequence()
                                                .AppendCallback(() =>
                                                {
                                                    this.enabled = false;
                                                    cameraChange.enabled = false;
                                                    isSequencePlaying = true;
                                                    Cursor.lockState = CursorLockMode.Locked;
                                                })
                                                .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(115, 80, 30), 2f))
                                                .Join(heldDrill.transform.DOLocalRotateQuaternion(Quaternion.Euler(-30, 5, 0), 2f))
                                                .AppendCallback(() =>
                                                {
                                                    // 드릴 소리
                                                })
                                                .Append(heldTool.transform.DOLocalMove(new Vector3(0.04f, -0.0074f, -0.0462f), 3f))
                                                .Join(heldDrill.transform.DOLocalMove(new Vector3(0.0156f, 0.0292f, 0.0521f), 3f))
                                                .AppendCallback(() =>
                                                {
                                                    dientesThree.SetActive(false);
                                                    dientesFour.SetActive(true);
                                                })
                                                .Append(heldTool.transform.DOLocalMove(Vector3.zero, 2f))
                                                .Join(heldDrill.transform.DOLocalMove(Vector3.zero, 2f))
                                                .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 2f))
                                                .Join(heldDrill.transform.DOLocalRotateQuaternion(Quaternion.identity, 2f))
                                                .AppendCallback(() =>
                                                {
                                                    this.enabled = true;
                                                    cameraChange.enabled = true;
                                                    isSequencePlaying = false;
                                                    Cursor.lockState = CursorLockMode.Confined;

                                                    currentState = SurgeryState.FixturePlace;
                                                });
                                        }
                                        else
                                        {
                                            gameOver = true;
                                        }
                                    }
                                    else
                                    {
                                        gameOver = true;
                                    }
                                    break;
                                case SurgeryState.FixturePlace:
                                    if (heldTool.name == "ToolHandpiece" && heldDrill != null)
                                    {
                                        if (heldDrill.name == "ToolFixture")
                                        {
                                            Sequence fixturePlaceSequence = DOTween.Sequence()
                                                .AppendCallback(() =>
                                                {
                                                    this.enabled = false;
                                                    cameraChange.enabled = false;
                                                    isSequencePlaying = true;
                                                    Cursor.lockState = CursorLockMode.Locked;
                                                })
                                                .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(115, 80, 30), 2f))
                                                .Join(heldDrill.transform.DOLocalRotateQuaternion(Quaternion.Euler(-30, 5, 0), 2f))
                                                .AppendCallback(() =>
                                                {
                                                    // 드릴 소리
                                                })
                                                .Append(heldTool.transform.DOLocalMove(new Vector3(0.04f, -0.0074f, -0.0462f), 3f))
                                                .Join(heldDrill.transform.DOLocalMove(new Vector3(0.0156f, 0.0292f, 0.0521f), 3f))
                                                .AppendCallback(() =>
                                                {
                                                    Destroy(heldDrill);
                                                    fixture.SetActive(true);
                                                })
                                                .Append(heldTool.transform.DOLocalMove(Vector3.zero, 2f))
                                                .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 2f))
                                                .AppendCallback(() =>
                                                {
                                                    this.enabled = true;
                                                    cameraChange.enabled = true;
                                                    isSequencePlaying = false;
                                                    Cursor.lockState = CursorLockMode.Confined;

                                                    currentState = SurgeryState.WrenchWithFixture;
                                                });
                                        }
                                        else
                                        {
                                            gameOver = true;
                                        }
                                    }
                                    else
                                    {
                                        gameOver = true;
                                    }
                                    break;
                                case SurgeryState.WrenchWithFixture:
                                    if(heldTool.name=="ToolTorqueRatchet")
                                    {
                                        Sequence wrenchWithFixtureSequence = DOTween.Sequence()
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = false;
                                                cameraChange.enabled = false;
                                                isSequencePlaying = true;
                                                Cursor.lockState = CursorLockMode.Locked;
                                            })
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(98, 34, -107), 2f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(77, 28, -107), 2f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(98, 34, -107), 1f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(77, 28, -107), 2f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(98, 34, -107), 1f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 2f))
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = true;
                                                cameraChange.enabled = true;
                                                isSequencePlaying = false;
                                                Cursor.lockState = CursorLockMode.Confined;

                                                currentState = SurgeryState.HealingAbutmentPlace;
                                            });
                                    }
                                    else
                                    {
                                        gameOver = true;
                                    }
                                    break;
                                case SurgeryState.HealingAbutmentPlace:
                                    if (heldTool.name == "ToolDriver" && heldAbutment != null)
                                    {
                                        if (heldAbutment.name == "ToolHealingAbutment")
                                        {
                                            Sequence healingAbutmentPlaceSequence = DOTween.Sequence()
                                                .AppendCallback(() =>
                                                {
                                                    this.enabled = false;
                                                    cameraChange.enabled = false;
                                                    isSequencePlaying = true;
                                                    Cursor.lockState = CursorLockMode.Locked;
                                                })
                                                .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(-8.7f, -45, 0), 1f))
                                                .Join(heldAbutment.transform.DOLocalRotateQuaternion(Quaternion.Euler(40, -27, 0), 1f))
                                                .Append(heldTool.transform.DOLocalMove(new Vector3(0.0425f, -0.009f, -0.043f), 1f))
                                                .Join(heldAbutment.transform.DOLocalMove(new Vector3(-0.0204f, -0.0383f, 0.043f), 1f))
                                                .AppendCallback(() =>
                                                {
                                                    Destroy(heldAbutment);
                                                    healingAbutment.SetActive(true);
                                                })
                                                .Append(heldTool.transform.DOLocalMove(Vector3.zero, 1f))
                                                .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 1f))
                                                .AppendCallback(() =>
                                                {
                                                    this.enabled = true;
                                                    cameraChange.enabled = true;
                                                    isSequencePlaying = false;
                                                    Cursor.lockState = CursorLockMode.Confined;

                                                    currentState = SurgeryState.SutureOne;
                                                });
                                        }
                                        else
                                        {
                                            gameOver = true;
                                        }
                                    }
                                    else
                                    {
                                        gameOver = true;
                                    }
                                    break;
                                case SurgeryState.SutureOne:
                                    if (heldTool.name == "ToolNeedle")
                                    {
                                        Sequence sutureOneSequence = DOTween.Sequence()
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = false;
                                                cameraChange.enabled = false;
                                                isSequencePlaying = true;
                                                Cursor.lockState = CursorLockMode.Locked;

                                                dientesGumsFive.SetActive(false);
                                                dientesGumsThree.SetActive(true);
                                            })
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 30, 0), 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(0.002f, 0.0049f, 0.001f), 0.5f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(-0.002f, -0.0049f, -0.001f), 0.5f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 30, 0), 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(0.002f, 0.0049f, 0.001f), 0.5f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(-0.002f, -0.0049f, -0.001f), 0.5f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 30, 0), 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(0.002f, 0.0049f, 0.001f), 0.5f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(-0.002f, -0.0049f, -0.001f), 0.5f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 30, 0), 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(0.002f, 0.0049f, 0.001f), 0.5f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(-0.002f, -0.0049f, -0.001f), 0.5f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 30, 0), 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(0.002f, 0.0049f, 0.001f), 0.5f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(-0.002f, -0.0049f, -0.001f), 0.5f))
                                            .AppendCallback(() =>
                                            {
                                                surgicalSutureOne.SetActive(true);

                                                this.enabled = true;
                                                cameraChange.enabled = true;
                                                isSequencePlaying = false;
                                                Cursor.lockState = CursorLockMode.Confined;

                                                currentState = SurgeryState.SutureTwo;
                                            });
                                    }
                                    else
                                    {
                                        gameOver = true;
                                    }
                                    break;
                                case SurgeryState.SutureTwo:
                                    if (heldTool.name == "ToolNeedle")
                                    {
                                        Sequence sutureOneSequence = DOTween.Sequence()
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = false;
                                                cameraChange.enabled = false;
                                                isSequencePlaying = true;
                                                Cursor.lockState = CursorLockMode.Locked;
                                            })
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 30, 0), 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(0.002f, 0.0049f, 0.001f), 0.5f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(-0.002f, -0.0049f, -0.001f), 0.5f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 30, 0), 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(0.002f, 0.0049f, 0.001f), 0.5f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(-0.002f, -0.0049f, -0.001f), 0.5f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 30, 0), 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(0.002f, 0.0049f, 0.001f), 0.5f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(-0.002f, -0.0049f, -0.001f), 0.5f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 30, 0), 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(0.002f, 0.0049f, 0.001f), 0.5f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(-0.002f, -0.0049f, -0.001f), 0.5f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 30, 0), 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(0.002f, 0.0049f, 0.001f), 0.5f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 0.5f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(-0.002f, -0.0049f, -0.001f), 0.5f))
                                            .AppendCallback(() =>
                                            {
                                                surgicalSutureTwo.SetActive(true);

                                                this.enabled = true;
                                                cameraChange.enabled = true;
                                                isSequencePlaying = false;
                                                Cursor.lockState = CursorLockMode.Confined;

                                                currentState = SurgeryState.HealingAbutmentRemove;
                                            });
                                    }
                                    else
                                    {
                                        gameOver = true;
                                    }
                                    break;

                                    /* 봉합 완료 후 힐링 어버트먼트 제거 사이에 넣을 것
                                     * 2~3개월 뒤 FadeInUI넣기
                                     * 잇몸 실 녹았다고 치고 dientesGum 기본으로 돌려놓기
                                     */

                                case SurgeryState.HealingAbutmentRemove:
                                    if(heldTool.name=="ToolDriver")
                                    {
                                        Sequence healingAbutmentRemoveSequence = DOTween.Sequence()
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = false;
                                                cameraChange.enabled = false;
                                                isSequencePlaying = true;
                                                Cursor.lockState = CursorLockMode.Locked;
                                            })
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(-8.7f, -45, 0), 1f))
                                            .Join(heldTool.transform.DOLocalMove(new Vector3(0.0131f,-0.0035f,0.0054f),1f))
                                            .Append(heldTool.transform.DOLocalMove(new Vector3(0.0436f, -0.0094f, -0.0284f), 1f))
                                            .AppendCallback(() =>
                                            {
                                                healingAbutment.SetActive(false);
                                            })
                                            .Append(heldTool.transform.DOLocalMove(Vector3.zero, 1f))
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 1f))
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = true;
                                                cameraChange.enabled = true;
                                                isSequencePlaying = false;
                                                Cursor.lockState = CursorLockMode.Confined;

                                                currentState = SurgeryState.AbutmentPlace;
                                            });
                                    }
                                    else
                                    {
                                        gameOver = true;
                                    }
                                    break;
                                case SurgeryState.AbutmentPlace:
                                    if (heldTool.name == "ToolDriver" && heldAbutment != null)
                                    {
                                        if (heldAbutment.name == "ToolAbutment")
                                        {
                                            Sequence abutmentPlaceSequence=DOTween.Sequence()
                                                .AppendCallback(() =>
                                                {
                                                    this.enabled = false;
                                                    cameraChange.enabled = false;
                                                    isSequencePlaying = true;
                                                    Cursor.lockState = CursorLockMode.Locked;
                                                })
                                                .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(-8.7f, -45, 0), 1f))
                                                .Join(heldAbutment.transform.DOLocalRotateQuaternion(Quaternion.Euler(40, -27, 0), 1f))
                                                .Append(heldTool.transform.DOLocalMove(new Vector3(0.0425f, -0.009f, -0.043f), 1f))
                                                .Join(heldAbutment.transform.DOLocalMove(new Vector3(-0.0204f, -0.0383f, 0.043f), 1f))
                                                .AppendCallback(() =>
                                                {
                                                    Destroy(heldAbutment);
                                                    abutment.SetActive(true);
                                                })
                                                .Append(heldTool.transform.DOLocalMove(Vector3.zero, 1f))
                                                .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.identity, 1f))
                                                .AppendCallback(() =>
                                                {
                                                    this.enabled = true;
                                                    cameraChange.enabled = true;
                                                    isSequencePlaying = false;
                                                    Cursor.lockState = CursorLockMode.Confined;

                                                    currentState = SurgeryState.CrownPlace;
                                                });
                                        }
                                        else
                                        {
                                            gameOver = true;
                                        }
                                    }
                                    else
                                    {
                                        gameOver = true;
                                    }
                                    break;
                                case SurgeryState.CrownPlace:
                                    if(heldTool.name=="ToolIncisorCrown"||heldTool.name=="ToolMolarCrown") 
                                    {
                                        Sequence crownPlaceSequence = DOTween.Sequence()
                                            .AppendCallback(() =>
                                            {
                                                this.enabled = false;
                                                cameraChange.enabled = false;
                                                isSequencePlaying = true;
                                                Cursor.lockState = CursorLockMode.Locked;
                                            })
                                            .Append(heldTool.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, -24, 64), 1f))
                                            .Append(heldTool.transform.DOLocalMove(new Vector3(0.00272f, -0.00046f, -0.00333f), 1f))
                                            .AppendCallback(() =>
                                            {
                                                Destroy(heldTool);
                                                crown.SetActive(true);

                                                this.enabled = true;
                                                cameraChange.enabled = true;
                                                isSequencePlaying = false;
                                                Cursor.lockState = CursorLockMode.Confined;

                                                currentState = SurgeryState.Clear;
                                                gameClear = true;
                                            });
                                    }
                                    else
                                    {
                                        gameOver = true;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }

    void HasHeldTool() // Player \ MainCamera \ HeldTool \ ? << 
    {
        if(cameraChange.heldTool.transform.childCount>0) // ? 에 오브젝트가 있는가
        {
            heldTool = cameraChange.heldTool.transform.GetChild(0).gameObject; // 있다면 SurgeryCamController.heldTool에 넣기
        }
        else
        {
            heldTool = null;
        }
    }

    void HasHeldDrill() // Player \ MainCamera \ HeldDrill \ ? << 
    {
        if(cameraChange.heldDrill.transform.childCount>0)
        {
            heldDrill = cameraChange.heldDrill.transform.GetChild(0).gameObject;
        }
        else
        {
            heldDrill = null;
        }
    }

    void HasHeldAbutment() // Player \ MainCamera \ HeldAbutment \ ? << 
    {
        if (cameraChange.heldAbutment.transform.childCount > 0)
        {
            heldAbutment = cameraChange.heldAbutment.transform.GetChild(0).gameObject;
        }
        else
        {
            heldDrill = null;
        }
    }
}
