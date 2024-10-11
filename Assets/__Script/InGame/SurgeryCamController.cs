/* Player가 있는 모든 씬의 SurgeryCamera에 들어갈 스크립트
 * 온갖 수술 관련 기능 추가 예정
 * 마우스커서에 도구가 따라오게 하는 기능
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurgeryCamController : MonoBehaviour
{
    public CameraChange cameraChange;

    public Camera surgeryCam;

    Vector3 mousePosition;
    Vector3 mouseWorldPosition;

    public float distanceFromCamera;

    private void Update()
    {
        if (surgeryCam.enabled)
        {
            //나중에 레이캐스트 쏴서 상호작용
            mousePosition=Input.mousePosition;
            mousePosition.z = distanceFromCamera;

            mouseWorldPosition=surgeryCam.ScreenToWorldPoint(mousePosition);

            cameraChange.heldTool.transform.position = mouseWorldPosition;
            cameraChange.heldDrill.transform.position= mouseWorldPosition;
        }
    }
}
