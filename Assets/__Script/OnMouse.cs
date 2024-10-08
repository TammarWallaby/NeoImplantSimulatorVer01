/*
 * MainCanvas 안에있는 튜토리얼 종류 버튼마다 넣을거임
 * 튜토리얼 설명 패널 열기 닫기 
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouse : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject explanationPanel;  // 해당 버튼에 연결된 설명 패널

    private void Start()
    {
        explanationPanel.SetActive(false); // 설명 패널 비활성화
    }

    // 마우스가 버튼에 들어왔을 때 호출
    public void OnPointerEnter(PointerEventData eventData)
    {
        explanationPanel.SetActive(true);  // 설명 패널 활성화
    }

    // 마우스가 버튼에서 나갔을 때 호출
    public void OnPointerExit(PointerEventData eventData)
    {
        explanationPanel.SetActive(false); // 설명 패널 비활성화
    }
}
