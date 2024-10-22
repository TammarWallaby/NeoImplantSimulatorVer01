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
        Finish
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

    public GameObject fixture;
    public GameObject healingAbutment;
    public GameObject abutment;
    public GameObject crown;

    public GameObject syringePusher;

    GameObject heldTool;
    GameObject heldDrill;
    GameObject heldAbutment;

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
                                        this.enabled = false;
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
                                                Debug.Log("성공!");
                                            });
                                    }
                                    else
                                    {
                                        // 실패 UI
                                        // Time.timeScale = 0f; << 이거 UI창 켜지면 timeScale 자동으로 0 되나? 생각해보니까 failUI 떠있을때 esc 안먹게해야하나?
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.IncisionOne:
                                    if (heldTool.name == "ToolScalpel")
                                    {
                                        this.enabled = false;
                                        dientesGums.SetActive(false);
                                        dientesGumsOne.SetActive(true); // 나중에 시퀀스 대체
                                        this.enabled = true;
                                        currentState = SurgeryState.IncisionTwo;
                                        Debug.Log("성공!");
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.IncisionTwo:
                                    if(heldTool.name=="ToolScalpel")
                                    {
                                        this.enabled = false;
                                        dientesGumsOne.SetActive(false);
                                        dientesGumsTwo.SetActive(true);
                                        this.enabled = true;
                                        currentState=SurgeryState.IncisionThree;
                                        Debug.Log("성공!");
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.IncisionThree:
                                    if (heldTool.name == "ToolScalpel")
                                    {
                                        this.enabled = false;
                                        dientesGumsTwo.SetActive(false);
                                        dientesGumsThree.SetActive(true);
                                        this.enabled = true;
                                        currentState = SurgeryState.ElevationOne;
                                        Debug.Log("성공!");
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.ElevationOne:
                                    if (heldTool.name == "ToolPeriostealElevator")
                                    {
                                        this.enabled = false;
                                        dientesGumsThree.SetActive(false);
                                        dientesGumsFour.SetActive(true);
                                        this.enabled = true;
                                        currentState= SurgeryState.ElevationTwo;
                                        Debug.Log("성공!");
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.ElevationTwo:
                                    if (heldTool.name == "ToolPeriostealElevator")
                                    {
                                        this.enabled = false;
                                        dientesGumsFour.SetActive(false);
                                        dientesGumsFive.SetActive(true);
                                        this.enabled = true;
                                        currentState = SurgeryState.DrillSmall;
                                        Debug.Log("성공!");
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.DrillSmall:
                                    if (heldTool.name == "ToolHandpiece" && heldDrill != null)
                                    {
                                        if (heldDrill.name == "ToolDrill2")
                                        {
                                            this.enabled = false;
                                            dientesOne.SetActive(false);
                                            dientesTwo.SetActive(true);
                                            this.enabled = true;
                                            currentState = SurgeryState.DrillMedium;
                                            Debug.Log("성공!");
                                        }
                                        else
                                        {
                                            Debug.Log("드릴 틀림 ㅋ");
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.DrillMedium:
                                    if (heldTool.name == "ToolHandpiece" && heldDrill != null)
                                    {
                                        if (heldDrill.name == "ToolDrill3")
                                        {
                                            this.enabled = false;
                                            dientesTwo.SetActive(false);
                                            dientesThree.SetActive(true);
                                            this.enabled = true;
                                            currentState = SurgeryState.DrillLarge;
                                            Debug.Log("성공!");
                                        }
                                        else
                                        {
                                            Debug.Log("드릴 틀림 ㅋ");
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.DrillLarge:
                                    if (heldTool.name == "ToolHandpiece" && heldDrill != null)
                                    {
                                        if (heldDrill.name == "ToolDrill4")
                                        {
                                            this.enabled = false;
                                            dientesThree.SetActive(false);
                                            dientesFour.SetActive(true);
                                            this.enabled = true;
                                            currentState = SurgeryState.FixturePlace;
                                            Debug.Log("성공!");
                                        }
                                        else
                                        {
                                            Debug.Log("드릴 틀림 ㅋ");
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.FixturePlace:
                                    if (heldTool.name == "ToolHandpiece" && heldDrill != null)
                                    {
                                        if (heldDrill.name == "ToolFixture")
                                        {
                                            this.enabled = false;
                                            fixture.SetActive(true);
                                            Destroy(heldDrill);
                                            this.enabled = true;
                                            currentState = SurgeryState.WrenchWithFixture;
                                            Debug.Log("성공!");
                                        }
                                        else
                                        {
                                            Debug.Log("드릴 틀림 ㅋ");
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.WrenchWithFixture:
                                    if(heldTool.name=="ToolTorqueRatchet")
                                    {
                                        this.enabled = false;
                                        this.enabled = true;
                                        currentState = SurgeryState.HealingAbutmentPlace;
                                        Debug.Log("성공!");
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.HealingAbutmentPlace:
                                    if (heldTool.name == "ToolDriver" && heldAbutment != null)
                                    {
                                        if (heldAbutment.name == "ToolHealingAbutment")
                                        {
                                            this.enabled = false;
                                            Destroy(heldAbutment);
                                            healingAbutment.SetActive(true);
                                            this.enabled = true;
                                            currentState = SurgeryState.SutureOne;
                                            Debug.Log("성공!");
                                        }
                                        else
                                        {
                                            Debug.Log("어버 틀림 ㅋ");
                                        }
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.SutureOne:
                                    if (heldTool.name == "ToolNeedle")
                                    {
                                        this.enabled = false;
                                        dientesGumsFive.SetActive(false);
                                        dientesGumsThree.SetActive(true);
                                        surgicalSutureOne.SetActive(true);
                                        this.enabled = true;
                                        currentState = SurgeryState.SutureTwo;
                                        Debug.Log("성공!");
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.SutureTwo:
                                    if (heldTool.name == "ToolNeedle")
                                    {
                                        this.enabled = false;
                                        surgicalSutureTwo.SetActive(true);
                                        this.enabled = true;
                                        currentState = SurgeryState.HealingAbutmentRemove;
                                        Debug.Log("성공!");
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.HealingAbutmentRemove:
                                    if(heldTool.name=="ToolTorqueRatchet") // 드라이버 교체예정
                                    {
                                        this.enabled = false;
                                        Destroy(healingAbutment);
                                        this.enabled = true;
                                        currentState=SurgeryState.AbutmentPlace;
                                        Debug.Log("성공!");
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.AbutmentPlace:
                                    if (heldTool.name == "ToolAbutment") // 드라이버 + 어버 교체예정
                                    {
                                        this.enabled = false;
                                        Destroy(heldTool);
                                        abutment.SetActive(true);
                                        this.enabled = true;
                                        currentState = SurgeryState.CrownPlace;
                                        Debug.Log("성공!");
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.CrownPlace:
                                    if(heldTool.name=="ToolIncisorCrown") // 이름때문에 어금니 X
                                    {
                                        this.enabled = false;
                                        Destroy(heldTool);
                                        crown.SetActive(true);
                                        this.enabled = true;
                                        currentState = SurgeryState.Finish;
                                        Debug.Log("끝까지 성공!");
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }

    void HasHeldTool()
    {
        if(cameraChange.heldTool.transform.childCount>0)
        {
            heldTool = cameraChange.heldTool.transform.GetChild(0).gameObject;
        }
        else
        {
            heldTool = null;
        }
    }

    void HasHeldDrill()
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

    void HasHeldAbutment()
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
