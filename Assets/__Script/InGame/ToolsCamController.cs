using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsCamController : MonoBehaviour
{
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
        if (toolsCam.enabled == true && Input.GetMouseButtonDown(0)) // esc 켜져있을때 못하게 추가해야됨
        {

            Ray ray = toolsCam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.DrawRay(ray.origin, ray.direction * pickUpRange, Color.red, 3.0f); // 2초 동안 빨간색 Ray 표시


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
                if (hit.collider.CompareTag("Tray"))
                {
                    DropTool();
                }
            }
        }
    }

    void PickUpTool(GameObject tool)
    {
        if (currentTool == null)
        {
            currentTool = tool; // 현재 도구 설정
            toolsOriginPosition = tool.transform.position;
            tool.transform.SetParent(heldToolTransform); // 도구를 카메라의 자식으로 설정
            tool.transform.localPosition = Vector3.zero; // 카메라 앞에 위치
            tool.transform.localRotation= Quaternion.identity;
        }
    }

    void PickUpDrill(GameObject drill)
    {
        if (currentTool != null)
        {
            if (currentTool.gameObject.name == "ToolHandpiece" && currentDrill == null)
            {
                currentDrill = drill;
                drillsOriginPosition = drill.transform.position;
                drill.transform.SetParent(heldDrillTransform);
                drill.transform.localPosition = Vector3.zero;
                drill.transform.localRotation = Quaternion.identity;
            }
        }
    }

    void DropTool()
    {
        if (currentTool != null)
        {
            if (currentTool.name == "ToolHandpiece" && currentDrill != null)
            {
                currentDrill.transform.SetParent(toolsTransform);
                currentDrill.transform.position = drillsOriginPosition;
                currentDrill.transform.rotation = Quaternion.identity;
                currentDrill = null; // 현재 드릴 초기화
            }
            else
            {
                currentTool.transform.SetParent(toolsTransform); // 도구를 부모에서 분리
                currentTool.transform.position = toolsOriginPosition;
                currentTool.transform.rotation = Quaternion.identity;
                currentTool = null; // 현재 도구 초기화
            }
        }
    }
}
