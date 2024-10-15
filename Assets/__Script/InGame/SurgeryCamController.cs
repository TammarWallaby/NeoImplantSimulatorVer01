/* Player가 있는 모든 씬의 SurgeryCamera에 들어갈 스크립트
 * 온갖 수술 관련 기능 추가 예정
 * 마우스커서에 도구가 따라오게 하는 기능
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
        WrenchWithHealingAbutment,
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
    public GameObject surgicalSutureThree;
    public GameObject surgicalSutureFour;
    public GameObject surgicalSutureFive;
    public GameObject surgicalSutureSix;
    public GameObject surgicalSutureSeven;
    public GameObject surgicalSutureEight;
    public GameObject surgicalSutureNine;
    public GameObject surgicalSutureTen;
    public GameObject surgicalSutureEleven;
    public GameObject surgicalSutureTwelve;
    public GameObject surgicalSutureThirteen;

    public GameObject fixture;
    public GameObject healingAbutment;
    public GameObject abutment;
    public GameObject crown;

    public GameObject heldTool;
    public GameObject heldDrill;

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

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = surgeryCam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, rayRange))
                {
                    if (hit.collider.CompareTag("SurgicalSite"))
                    {
                        HasHeldTool();
                        if (heldTool.name == "ToolHandpiece")
                        {
                            HasHeldDrill();
                        }

                        if (heldTool!= null)
                        {
                            switch (currentState) 
                            {
                                case SurgeryState.Anesthesia:
                                    if (heldTool.name == "ToolSyringe")
                                    {
                                        this.enabled = false;
                                        // 마취 시퀀스
                                        this.enabled = true;
                                        currentState = SurgeryState.IncisionOne;
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
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.DrillSmall:
                                    if(heldTool.name=="HandPiece"&&heldDrill!=null)
                                    {
                                        if (heldDrill.name == "ToolDrill2")
                                        {
                                            this.enabled = false;
                                            dientesOne.SetActive(false);
                                            dientesTwo.SetActive(true);
                                            this.enabled = true;
                                            currentState = SurgeryState.DrillMedium;
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
                                    if (heldTool.name == "HandPiece" && heldDrill != null)
                                    {
                                        if (heldDrill.name == "ToolDrill3")
                                        {
                                            this.enabled = false;
                                            dientesTwo.SetActive(false);
                                            dientesThree.SetActive(true);
                                            this.enabled = true;
                                            currentState = SurgeryState.DrillLarge;
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
                                    if (heldTool.name == "HandPiece" && heldDrill != null)
                                    {
                                        if (heldDrill.name == "ToolDrill4")
                                        {
                                            this.enabled = false;
                                            dientesThree.SetActive(false);
                                            dientesFour.SetActive(true);
                                            this.enabled = true;
                                            currentState = SurgeryState.FixturePlace;
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
                                    if (heldTool.name=="ToolFixture")
                                    {
                                        this.enabled = false;
                                        fixture.SetActive(true);
                                        Destroy(heldTool);
                                        this.enabled = true;
                                        currentState = SurgeryState.HealingAbutmentPlace;
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.HealingAbutmentPlace:
                                    if (heldTool.name == "ToolHealingAbutment")
                                    {
                                        this.enabled = false;
                                        healingAbutment.SetActive(true);
                                        Destroy(heldTool);
                                        this.enabled = true;
                                        currentState = SurgeryState.SutureOne;
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
                                        surgicalSutureTwo.SetActive(true);
                                        surgicalSutureThree.SetActive(true);
                                        surgicalSutureFour.SetActive(true);
                                        surgicalSutureFive.SetActive(true);
                                        this.enabled = true;
                                        currentState = SurgeryState.SutureTwo;
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
                                        surgicalSutureSix.SetActive(true);
                                        surgicalSutureSeven.SetActive(true);
                                        surgicalSutureEight.SetActive(true);
                                        surgicalSutureNine.SetActive(true);
                                        surgicalSutureTen.SetActive(true);
                                        this.enabled = true;
                                        currentState = SurgeryState.SutureThree;
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.SutureThree:
                                    if (heldTool.name == "ToolNeedle")
                                    {
                                        this.enabled = false;
                                        surgicalSutureEleven.SetActive(true);
                                        surgicalSutureTwelve.SetActive(true);
                                        surgicalSutureThirteen.SetActive(true);
                                        this.enabled = true;
                                        currentState = SurgeryState.HealingAbutmentRemove;
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.HealingAbutmentRemove:
                                    if(heldTool.name=="ToolTorqueRatchet")
                                    {
                                        this.enabled = false;
                                        Destroy(healingAbutment);
                                        this.enabled = true;
                                        currentState=SurgeryState.AbutmentPlace;
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.AbutmentPlace:
                                    if (heldTool.name == "ToolAbutment")
                                    {
                                        this.enabled = false;
                                        Destroy(heldTool);
                                        abutment.SetActive(true);
                                        this.enabled = true;
                                        currentState = SurgeryState.CrownPlace;
                                    }
                                    else
                                    {
                                        Debug.Log("순서 틀림 ㅋ");
                                    }
                                    break;
                                case SurgeryState.CrownPlace:
                                    if(heldTool.name=="ToolIncisorCrown")
                                    {
                                        this.enabled = false;
                                        Destroy(heldTool);
                                        crown.SetActive(true);
                                        this.enabled = true;
                                        currentState = SurgeryState.Finish;
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
}
