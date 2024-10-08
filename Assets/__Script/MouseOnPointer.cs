/*
 * 튜토리얼 전용 Panel에 넣음
 * 튜토리얼 마우스 충돌로 인한 설명에 쓸거임
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.DebugUI;

public class MouseOnPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler // IPointerEnterHandler: 마우스 충돌 함수 , IPointerExitHandler: 마우스 충돌 범위 나갈 때 함수
{
    public GameObject TutorialsPanel; //튜토패널
    public GameObject panel; // 설명 패널


    void Start()
    {

        panel.SetActive(false); // 패널 비활성화
    }

    // 마우스를 버튼 위에 올렸을 때 실행
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (panel != null)
        {
            panel.SetActive(true); // 패널 활성화
        }
    }

    // 마우스를 버튼에서 뗐을 때 실행
    public void OnPointerExit(PointerEventData eventData)
    {
        if (panel != null)
        {
            panel.SetActive(false); // 패널 비활성화
        }
    }
}
