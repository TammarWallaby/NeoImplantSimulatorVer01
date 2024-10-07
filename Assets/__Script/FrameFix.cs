/*
 * 프레임 60 고정, 커서 보이게하기
 * 게임씬에 넣음
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameFix : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        Cursor.visible = true;
    }
}
