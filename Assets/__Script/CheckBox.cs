/*
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckBox : MonoBehaviour
{
    public Toggle checkBox; // 체크박스 (Toggle) 오브젝트
    public bool conditionMet = false; // 특정 상황이 충족되었는지 여부를 저장하는 변수

    private void Update()
    {
        // 특정 상황이 충족되었을 때 체크박스를 체크
        if (conditionMet)
        {
            checkBox.isOn = true; // 체크박스를 체크 상태로 변경
        }
        else
        {
            checkBox.isOn = false; // 조건이 충족되지 않으면 체크 해제
        }
    }

    // 특정 조건을 만족했을 때 호출될 함수 예시
    public void MeetCondition()
    {
        conditionMet = true; // 조건이 충족되었음을 나타냄
    }

    // 조건이 충족되지 않았을 때 호출될 함수 예시
    public void UnmeetCondition()
    {
        conditionMet = false; // 조건을 충족하지 않았음을 나타냄
    }
}