/*
 * GameOver, Clear 패널이 있는 Canvus에 넣을거임
 * 게임오버 + 클리어 창 뜨기, 다시하기, (종료랑 메인메뉴는 SettingMenu에서 활용) 
 * Update()에 if 문에 특정 상황 넣으면 됨 
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class OverClear : MonoBehaviour
{
    public GameObject gameOverPanel; // 실패 시 보여줄 패널
    public GameObject clearPanel;    // 클리어 시 보여줄 패널

    public bool isGameOver = false;
    public bool isGameCleared = false;

    public bool processCorrect = false; // 임플란트 과정이 올바른지 여부
    public bool processFailed = false; // 과정이 실패했는지 여부

    void Awake()
    {
        // 처음 시작 시 패널 비활성화
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);

        }
        if (clearPanel != null)
        {
            clearPanel.SetActive(false);
        }

    }
    private void Update()
    {
        // 키 입력에 따라 게임 오버 및 클리어 처리
        if (Input.GetKeyDown(KeyCode.E)) // E 키를 눌렀을 때
        {
            GameOver(true); // 게임 오버 처리
        }

        if (Input.GetKeyDown(KeyCode.C)) // C 키를 눌렀을 때
        {
            GameClear(true); // 게임 클리어 처리
        }

        CheckImplantProcess(); // 임플란트 과정 체크
    }
    public void GameOver(bool hasFailed)
    {
        if (hasFailed)
        {
            if (isGameOver) return; // 이미 게임이 끝났다면 처리하지 않음

            processFailed = true; // 과정 실패로 상태 변경
            isGameOver = true; // 게임 오버 상태로 설정

            Cursor.lockState = CursorLockMode.Confined;

            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true); // 게임 오버 패널 표시
            }
        }
    }
    public void GameClear(bool isSuccess)
    {
        if (isSuccess)
        {
            if (isGameCleared) return; // 이미 클리어된 상태라면 처리하지 않음

            processCorrect = true; // 과정이 올바른 것으로 상태 변경
            isGameCleared = true; // 게임 클리어 상태로 설정

            Cursor.lockState = CursorLockMode.Confined;

            if (clearPanel != null)
            {
                clearPanel.SetActive(true); // 클리어 패널 표시
            }
        }
    }

    // "재시작" 버튼을 클릭했을 때 호출되는 함수
    public void RestartGame()
    {
        Time.timeScale = 1; // 시간 재개
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // 현재 씬 재시작
    }

    public void CheckImplantProcess()
    {
        // 특정 조건을 체크합니다 (예: processFailed와 processCorrect 변수에 따라)
        if (processFailed) // 조건이 잘못된 경우
        {
            GameOver(true); // 과정 실패 호출
        }
        else if (processCorrect) // 과정이 성공한 경우
        {
            GameClear(true); // 과정 성공 호출
        }
    }
}