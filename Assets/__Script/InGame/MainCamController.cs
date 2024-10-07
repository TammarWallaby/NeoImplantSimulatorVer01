using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamController : MonoBehaviour
{
    public float mouseSpeed; // 회전속도, 설정 필수
    public float upLimit; // 위쪽 한계값, 설정 필수
    public float downLimit; // 아래쪽 한계값, 설정 필수

    private float mouseX; //좌우 회전값
    private float mouseY; //위아래 회전값

    void Start()
    {
        // 마우스를 중앙에 고정하고 숨김
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //마우스 회전 처리
        mouseX += Input.GetAxis("Mouse X") * mouseSpeed * Time.deltaTime;
        mouseY += Input.GetAxis("Mouse Y") * mouseSpeed * Time.deltaTime;

        mouseY = Mathf.Clamp(mouseY, downLimit, upLimit);
        this.transform.localEulerAngles = new Vector3(-mouseY, mouseX, 0);

        //if (Input.GetMouseButtonDown(0)) // 좌클릭 시
        //{
        //    Ray ray = new Ray(transform.position, transform.forward);
        //    RaycastHit hit;

        //    Debug.DrawRay(ray.origin, ray.direction * maxRayDistance, Color.red, 2f); // 2초 동안 레이를 그립니다.

        //    if (Physics.Raycast(ray, out hit, maxRayDistance, toolsLayer))
        //    {
        //        PickUpObject(hit.collider.transform);
        //    }
        //    else if (Physics.Raycast(ray, out hit, maxRayDistance, trayLayer))
        //    {
        //        PutDownObject();
        //    }
        //}
    }

    //private void PickUpObject(Transform objectTransform)
    //{
    //    if (heldObject == null)
    //    {
    //        toolsOriginalPosition = objectTransform.position;
    //        heldObject = objectTransform; // 오브젝트를 쥐기
    //        heldObject.SetParent(transform); // 카메라의 자식으로 설정
    //        heldObject.localPosition = new Vector3(0.28f, -0.14f, 0.362f);
    //        heldObject.localRotation = Quaternion.Euler(-1.46f, 73.778f, -48.861f);
    //    }
    //}

    //private void PutDownObject()
    //{
    //    if (heldObject != null)
    //    {
    //        heldObject.SetParent(onTrayTools.transform);
    //        heldObject.position = toolsOriginalPosition;
    //        heldObject.localRotation = Quaternion.Euler(0, 0, 0);

    //        heldObject = null;
    //    }
    //}

    public Vector3 GetForwardDirection()
    {
        return transform.forward;
    }

    public Vector3 GetRightDirection()
    {
        return transform.right;
    }

    public void SetMouseSensitivity(float sensitivity)
    {
        mouseSpeed = sensitivity; // 슬라이더 값으로 마우스 감도 설정
    }
}
