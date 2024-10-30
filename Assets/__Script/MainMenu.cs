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
        tutorialPanel.SetActive(false); //시작 시, 튜토리얼 패널 비활성화
        startPanel.SetActive(false); //시작 시, 스타트 패널 비활성화
    }

    public void OnTutorialButtonClick()
    {
        tutorialPanel.SetActive(true);//튜토리얼 패널 활성화
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void OnStartButtonClick() 
    {
        startPanel.SetActive(true);//스타트 패널 활성화
        Cursor.lockState = CursorLockMode.Confined;
    }


    public void IncisorGame()
    {
        // 예를 들어, 게임이 시작하는 씬이 "GameScene"이라면 해당 씬을 로드
        SceneManager.LoadScene("Test 1C");
    }

    public void MolarGame()
    {
        // 예를 들어, 게임이 시작하는 씬이 "GameScene"이라면 해당 씬을 로드
        SceneManager.LoadScene("Test 2");
    }
}
