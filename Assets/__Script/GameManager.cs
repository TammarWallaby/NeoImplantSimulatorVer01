/*
 * SettingMenu에 합쳤음
 */

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isPaused = false;
    private PlayerControl playerMove; // 플레이어 움직임 제어 스크립트
    private MainCameraControl cameraRot;

    void Start()
    {
        playerMove = FindObjectOfType<PlayerControl>(); // PlayerMove 스크립트 참조
        cameraRot = FindObjectOfType<MainCameraControl>(); // CameraRot 스크립트 참조
        Cursor.visible = false;
    }

    void Update()
    {
        // ESC 키가 눌렸을 때
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();            
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        // 게임을 일시 정지하고 모든 오브젝트의 동작을 멈춤
        if (isPaused)
        {
            Time.timeScale = 0; // 시간을 정지
            if (playerMove != null) playerMove.enabled = false; // 플레이어 움직임 비활성화
            if (cameraRot != null) cameraRot.enabled = false;
            Cursor.visible = true;
            CursorControl.SetPosition(new Vector2(Screen.width / 2, Screen.height / 2));
        }
        else
        {
            Time.timeScale = 1; // 시간을 재개
            if (playerMove != null) playerMove.enabled = true; // 플레이어 움직임 활성화
            if (cameraRot != null) cameraRot.enabled = true;
            Cursor.visible = false;
        }
    }
}
