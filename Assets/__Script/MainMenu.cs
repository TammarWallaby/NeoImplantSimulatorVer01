/*
 * 화면 전환 용도
 * 메인 화면 Canvus에 넣음
 */

using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리 기능을 위한 네임스페이스
using UnityEngine.UI; // UI 요소를 관리하기 위한 네임스페이스


public class MainMenu : MonoBehaviour
{


    // 시작하기 버튼을 클릭했을 때 호출될 함수
    public void StartGame()
    {
        // 예를 들어, 게임이 시작하는 씬이 "GameScene"이라면 해당 씬을 로드
        //SceneManager.LoadScene("GameScene");
        SceneManager.LoadScene("GameChoice");
    }

    public void TutorialsGame()
    {
        // 예를 들어, 게임이 시작하는 씬이 "GameScene"이라면 해당 씬을 로드
        SceneManager.LoadScene("TutorialsScene");
    }

    public void IncisorGame()
    {
        // 예를 들어, 게임이 시작하는 씬이 "GameScene"이라면 해당 씬을 로드
        SceneManager.LoadScene("AlphaTestScene");
    }

    public void MolarGame()
    {
        // 예를 들어, 게임이 시작하는 씬이 "GameScene"이라면 해당 씬을 로드
        SceneManager.LoadScene("AlphaTestScene");
    }
}
