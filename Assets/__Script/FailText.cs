/*
 * 바꾸는 Text UI 에 위치
 * 도구 실패 시 Fail 패널에 "00" 하지 못했습니다 라고 뜸
 */

using UnityEngine;
using UnityEngine.UI;

public class FailText : MonoBehaviour
{
    public Text uiText; // UI 텍스트 컴포넌트

    private void Start()
    {
        if (uiText != null)
        {
            uiText.text = "시작 텍스트"; // 초기 텍스트 설정
        }

        /* 특정 상황 입력
        if ()
        {
        특정 상황 (() => ChangeText("새로운 텍스트!"));
        }
        */

        //예시

    }

    // 텍스트를 변경하는 메서드
    public void ChangeText(string newText)
    {
        if (uiText != null)
        {
            uiText.text = newText; // 새로운 텍스트로 변경
        }
    }
}
