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
