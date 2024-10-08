/*
 * MainCanvas 안에있는 튜토리얼 종류 버튼마다 넣을거임
 * 튜토리얼 설명 패널 열기 닫기 
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouse : MonoBehaviour, IPointerEnterHandler
{
    public GameObject explanationPanel;  // 해당 버튼에 연결된 설명 패널
    private static GameObject activePanel = null;  // 현재 활성화된 패널을 추적하는 변수

    private void Start()
    {
        explanationPanel.SetActive(false); // 설명 패널 비활성화
    }

    // 마우스가 버튼에 들어왔을 때 호출
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 이미 다른 패널이 활성화되어 있다면 비활성화
        if (activePanel != null && activePanel != explanationPanel)
        {
            activePanel.SetActive(false);
        }

        explanationPanel.SetActive(true);  // 현재 버튼의 설명 패널 활성화
        activePanel = explanationPanel;    // 활성화된 패널로 설정
    }


}
