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
    public Transform toolsTransform;
    

    public float pickUpRange;

    public GameObject currentTool;
    public GameObject currentDrill;


    Vector3 toolsOriginPosition;
    Vector3 drillsOriginPosition;

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
        currentTool = tool; // 현재 도구 설정
        currentTool.GetComponent<Collider>().enabled = false;
        toolsOriginPosition = tool.transform.position;
        tool.transform.SetParent(heldToolTransform); // 도구를 카메라의 자식으로 설정
        tool.transform.localPosition = Vector3.zero; // 카메라 앞에 위치
        tool.transform.localRotation = Quaternion.identity;
    }

    void PickUpDrill(GameObject drill)
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

    void DropTool()
    {
        if (currentTool != null)
        {
            if (currentDrill != null)
            {
                currentDrill.GetComponent<Collider>().enabled = true;
                currentDrill.transform.SetParent(toolsTransform);
                currentDrill.transform.position = drillsOriginPosition;
                currentDrill.transform.rotation = Quaternion.identity;
                currentDrill = null; // 현재 드릴 초기화
            }
            currentTool.GetComponent<Collider>().enabled = true;
            currentTool.transform.SetParent(toolsTransform); // 도구를 부모에서 분리
            currentTool.transform.position = toolsOriginPosition;
            currentTool.transform.rotation = Quaternion.identity;
            currentTool = null; // 현재 도구 초기화
        }
    }
}
