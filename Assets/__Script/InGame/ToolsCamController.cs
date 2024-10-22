/* Player가 있는 모든 씬의 ToolsCamera에 들어갈 스크립트
 * 도구카메라에서 도구 및 드릴을 집거나 놓는 역할
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsCamController : MonoBehaviour
{
    public SettingManager settingManager;

    public Camera toolsCam;
    public Transform heldToolTransform;
    public Transform heldDrillTransform;
    public Transform heldAbutmentTransform;

    public Transform toolsTransform;
    
    public float pickUpRange;

    public GameObject currentTool;
    public GameObject currentDrill;
    public GameObject currentAbutment;


    Vector3 toolsOriginPosition;
    Vector3 drillsOriginPosition;
    Vector3 abutmentOriginPosition;

    public bool toolDropped = false; // 도구가 내려졌는지 추적하는 변수
    public bool toolPicked = false; // 도구를 집었는지 추적하는 변수
    private void Awake()
    {
        toolsCam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (toolsCam.enabled == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = toolsCam.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, pickUpRange))
                {
                    if (hit.collider.CompareTag("Tools"))
                    {
                        PickUpTool(hit.collider.gameObject);
                    }
                    if (hit.collider.CompareTag("Drill"))
                    {
                        PickUpDrill(hit.collider.gameObject);
                    }
                    if (hit.collider.CompareTag("Abutment"))
                    {
                        PickUpAbutment(hit.collider.gameObject);
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                DropTool();
            }
        }

        
    }

    void PickUpTool(GameObject tool)
    {
        if (currentTool != null)
        {
            currentTool.GetComponent<Collider>().enabled = true;
            currentTool.transform.SetParent(toolsTransform); // 도구를 부모에서 분리
            currentTool.transform.position = toolsOriginPosition;
            currentTool.transform.rotation = Quaternion.identity;
            currentTool = null; // 현재 도구 초기화
        }
        if (currentDrill != null)
        {
            currentDrill.GetComponent<Collider>().enabled = true;
            currentDrill.transform.SetParent(toolsTransform);
            currentDrill.transform.position = drillsOriginPosition;
            currentDrill.transform.rotation = Quaternion.identity;
            currentDrill = null; // 현재 드릴 초기화
        }
        if (currentAbutment != null)
        {
            currentAbutment.GetComponent<Collider>().enabled = true;
            currentAbutment.transform.SetParent(toolsTransform);
            currentAbutment.transform.position = abutmentOriginPosition;
            currentAbutment.transform.rotation=Quaternion.identity;
            currentAbutment = null;
        }
        currentTool = tool; // 현재 도구 설정
        currentTool.GetComponent<Collider>().enabled = false;
        toolsOriginPosition = tool.transform.position;
        tool.transform.SetParent(heldToolTransform); // 도구를 카메라의 자식으로 설정
        tool.transform.localPosition = Vector3.zero; // 카메라 앞에 위치
        tool.transform.localRotation = Quaternion.identity;
    }

    void PickUpDrill(GameObject drill)
    {
        if (currentTool != null)
        {
            if (currentTool.gameObject.name == "ToolHandpiece")
            {
                if (currentDrill != null)
                {
                    currentDrill.GetComponent<Collider>().enabled = true;
                    currentDrill.transform.SetParent(toolsTransform);
                    currentDrill.transform.position = drillsOriginPosition;
                    currentDrill.transform.rotation = Quaternion.identity;
                    currentDrill = null; // 현재 드릴 초기화
                }
                currentDrill = drill;
                currentDrill.GetComponent<Collider>().enabled = false;
                drillsOriginPosition = drill.transform.position;
                drill.transform.SetParent(heldDrillTransform);
                drill.transform.localPosition = Vector3.zero;
                drill.transform.localRotation = Quaternion.identity;
            }
        }
    }

    void PickUpAbutment(GameObject abutment)
    {
        if (currentTool!=null)
        {
            if(currentTool.gameObject.name=="ToolDriver")
            {
                if (currentAbutment != null)
                {
                    currentAbutment.GetComponent<Collider>().enabled = true;
                    currentAbutment.transform.SetParent(toolsTransform);
                    currentAbutment.transform.position = abutmentOriginPosition;
                    currentAbutment.transform.rotation = Quaternion.identity;
                    currentAbutment = null;
                }
                currentAbutment = abutment;
                currentAbutment.GetComponent<Collider>().enabled = false;
                abutmentOriginPosition = abutment.transform.position;
                abutment.transform.SetParent(heldAbutmentTransform);
                abutment.transform.localPosition= Vector3.zero;
                abutment.transform.localRotation= Quaternion.identity;
            }
        }
    }

    public void DropTool()
    {
        if (currentTool != null)
        {
            toolDropped = true; // 도구가 내려졌음을 표시
            if (currentDrill != null)
            {
                currentDrill.GetComponent<Collider>().enabled = true;
                currentDrill.transform.SetParent(toolsTransform);
                currentDrill.transform.position = drillsOriginPosition;
                currentDrill.transform.rotation = Quaternion.identity;
                currentDrill = null; // 현재 드릴 초기화
            }
            if (currentAbutment != null)
            {
                currentAbutment.GetComponent<Collider>().enabled = true;
                currentAbutment.transform.SetParent(toolsTransform);
                currentAbutment.transform.position = abutmentOriginPosition;
                currentAbutment.transform.rotation = Quaternion.identity;
                currentAbutment = null;
            }
            currentTool.GetComponent<Collider>().enabled = true;
            currentTool.transform.SetParent(toolsTransform); // 도구를 부모에서 분리
            currentTool.transform.position = toolsOriginPosition;
            currentTool.transform.rotation = Quaternion.identity;
            currentTool = null; // 현재 도구 초기화
        }
    }
}
