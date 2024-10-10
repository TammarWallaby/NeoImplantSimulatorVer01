/*
 * GameOver, Clear 패널이 있는 Canvus에 넣을거임
 * 게임오버 + 클리어 창 뜨기, 다시하기, (종료랑 메인메뉴는 SettingMenu에서 활용) 
 * 임시로 e 누르면 게임오버 , c누르면 클리어 창 뜸
 */

using UnityEngine;
using UnityEngine.SceneManagement;

public class OverClear : MonoBehaviour
{
    public GameObject gameOverPanel; // 실패 시 보여줄 패널
    public GameObject clearPanel;    // 클리어 시 보여줄 패널

    private bool isGameOver = false;
    private bool isGameCleared = false;

    public bool processCorrect = true; // 임플란트 과정이 올바른지 여부
    public bool processFailed = false; // 과정이 실패했는지 여부

    void Start()
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
        // 게임 오버 조건 확인
        /* if (특정 조건)
        {
            ProcessGameOver(true); // 게임 오버 처리
        }*/

        // 특정 조건에서 과정이 성공한 경우
        /*if (특정 조건)
        {
            ProcessGameOver(true); // 게임 클리어 처리
        }*/
    }
    public void ProcessGameOver(bool hasFailed)
    {
        if (hasFailed)
        {
            if (isGameOver) return; // 이미 게임이 끝났다면 처리하지 않음

            processFailed = true; // 과정 실패로 상태 변경
            isGameOver = true; // 게임 오버 상태로 설정

            Cursor.visible = true; // 마우스 커서 보이기

            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(true); // 게임 오버 패널 표시
            }
        }
    }
    public void ProcessGameClear(bool isSuccess)
    {
        if (isSuccess)
        {
            if (isGameCleared) return; // 이미 클리어된 상태라면 처리하지 않음

            processCorrect = true; // 과정이 올바른 것으로 상태 변경
            isGameCleared = true; // 게임 클리어 상태로 설정

            Cursor.visible = true; // 마우스 커서 보이기

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
        // 조건이 잘못된 경우
        /*if (특정 조건이 잘못된 경우)
        {
            ProcessFailed(); // 과정 실패 호출
        }
        else
        {
            ProcessSucceeded(); // 과정 성공 호출
        }*/
    }
}