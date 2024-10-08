/*
 * 메인 화면 Canvus에 넣음
 * 화면 전환,튜토 패널 용도
 */

using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리 기능을 위한 네임스페이스
using UnityEngine.UI; // UI 요소를 관리하기 위한 네임스페이스


public class MainMenu : MonoBehaviour
{
    public GameObject tutorialPanel;
    public GameObject startPanel;

    private void Start()
    {
        tutorialPanel.SetActive(false);
        startPanel.SetActive(false);
    }

    public void OnTutorialButtonClick()
    {
        tutorialPanel.SetActive(true);
        Cursor.visible = true;
    }

    public void OnStartButtonClick()
    {
        startPanel.SetActive(true);
        Cursor.visible = true;
    }


    public void IncisorGame()
    {
        // 예를 들어, 게임이 시작하는 씬이 "GameScene"이라면 해당 씬을 로드
        SceneManager.LoadScene("Test");
    }

    public void MolarGame()
    {
        // 예를 들어, 게임이 시작하는 씬이 "GameScene"이라면 해당 씬을 로드
        SceneManager.LoadScene("Test");
    }
}
