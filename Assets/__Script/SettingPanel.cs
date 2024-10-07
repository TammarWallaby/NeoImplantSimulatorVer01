/*
 * SettingsMenu에 합쳤음
 */

using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리 기능을 위한 네임스페이스
using UnityEngine.UI; // UI 요소를 관리하기 위한 네임스페이스

public class SettingPanel : MonoBehaviour
{
    public GameObject settingsPanel;

    public void ToggleSettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf); //패널 활성화

    }

    // 끝내기 버튼을 클릭했을 때 호출될 함수
    public void ExitGame()
    {
        // 에디터에서 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 빌드된 게임에서 종료
        Application.Quit();
#endif
    }

    private void Update()
    {
        // ESC 입력으로 설정 패널 토글
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC 누르면 패널 열리게하기
        {
            ToggleSettingsPanel(); // 패널 토글

        }
    }

}
