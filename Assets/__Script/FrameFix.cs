/* MainMenu씬 MainCamera에 들어가 있는 스크립트, 임시
 * 프레임 고정
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
}
