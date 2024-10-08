/* 
 * 각종 Panel(Prefab으로) 들어감
 * GameOver, Clear에 있는 버튼에도 씀
 * 환경설정 패널 + 시간정지 , 끝내기 '게임 종료'
 */

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public GameObject settingsPanel; // 설정 패널 , 설정필수
    public Slider backgroundVolumeSlider; // 배경음 슬라이더 , 설정필수
    public Slider effectVolumeSlider; // 효과음 슬라이더 , 설정필수
    public Slider mouseSensitivitySlider; // 마우스 감도 조절 슬라이더 , 설정필수
    public Text backgroundVolumeText; // 사운드 슬라이더 값 표시 텍스트 , 설정필수
    public Text sensitivityValueText; // 마우스 감도 슬라이더 값 표시 텍스트 , 설정필수
    public Text effectVolumeValueText; // 효과음 슬라이더 값 표시 텍스트 , 설정필수
    public InputField sensitivityInputField; // 마우스 감도를 입력할 InputField , 설정필수


    // displayedSensitivity를 클래스 변수로 선언
    private float displayedSensitivity;
    void Start()
    {

        // 설정 패널 숨기기
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        // 마우스 감도 슬라이더 설정
        if (mouseSensitivitySlider != null)
        {
            mouseSensitivitySlider.minValue = 0.24f; // 최소값
            mouseSensitivitySlider.maxValue = 240f;  // 최대값

            // 초기 슬라이더 값을 설정하고, InputField 및 텍스트에 반영
            mouseSensitivitySlider.value = SettingsData.instance.mouseSensitivity;

            // 슬라이더 값을 0.01 ~ 10 범위로 변환하여 표시
            float displayedSensitivity = Mathf.Lerp(0.01f, 10f, Mathf.InverseLerp(0.24f, 240f, mouseSensitivitySlider.value));
            sensitivityValueText.text = displayedSensitivity.ToString("F2"); // 텍스트로 표시
            sensitivityInputField.text = displayedSensitivity.ToString("F2"); // InputField에도 표시

            // 슬라이더 변화 시 감도 설정
            mouseSensitivitySlider.onValueChanged.AddListener(SetMouseSensitivity);
            sensitivityInputField.onEndEdit.AddListener(delegate { OnSensitivityInputChanged(sensitivityInputField.text); });
        }
        else
        {
            Debug.LogError("mouseSensitivitySlider가 할당되지 않았습니다.");
        }

        // 사운드 슬라이더 설정
        if (backgroundVolumeSlider != null)
        {
            backgroundVolumeSlider.minValue = 0;
            backgroundVolumeSlider.maxValue = 100;
            backgroundVolumeSlider.value = SettingsData.instance.backgroundVolume * 100; // 현재 사운드 볼륨을 슬라이더에 반영
            backgroundVolumeText.text = backgroundVolumeSlider.value.ToString("F0") + "%"; // 초기값 텍스트로 표시
            backgroundVolumeSlider.onValueChanged.AddListener(SetVolume); // 사운드 조절 연결
        }
        else
        {
            Debug.LogError("backgroundVolumeSlider가 할당되지 않았습니다.");
        }

        // 효과음 슬라이더 설정
        if (effectVolumeSlider != null)
        {
            effectVolumeSlider.minValue = 0;
            effectVolumeSlider.maxValue = 100;
            effectVolumeSlider.value = SettingsData.instance.effectVolume * 100; // 현재 사운드 볼륨을 슬라이더에 반영
            effectVolumeValueText.text = effectVolumeSlider.value.ToString("F0") + "%"; // 초기값 텍스트로 표시
            effectVolumeSlider.onValueChanged.AddListener(SetEffectVolume); // 사운드 조절 연결
        }
        else
        {
            Debug.LogError("effectVolumeSlider가 할당되지 않았습니다.");
        }
    }

    public void ToggleSettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf); //패널 활성화

    }

    void SetVolume(float volume) //배경음
    {
        AudioManager.instance.SetBackgroundVolume(volume / 100f); // 사운드를 0~1 범위로 설정
        backgroundVolumeText.text = volume.ToString("F0") + "%"; // 소수점 없이 슬라이더 값을 텍스트로 표시
        SettingsData.instance.backgroundVolume = volume / 100f; // 볼륨 값을 SettingsData에 저장
    }

    void SetMouseSensitivity(float sensitivity) //마우스 감도
    {
        SettingsData.instance.mouseSensitivity = sensitivity; // 마우스 감도 값을 SettingsData에 저장

        // 슬라이더 값을 0.01 ~ 10 범위로 변환하여 표시
        float displayedSensitivity = Mathf.Lerp(0.01f, 10f, Mathf.InverseLerp(0.24f, 240f, sensitivity));
        sensitivityValueText.text = displayedSensitivity.ToString("F2"); // 텍스트로 표시
        sensitivityInputField.text = displayedSensitivity.ToString("F2"); // InputField에도 표시

    }

    void SetEffectVolume(float volume) //효과음
    {
        AudioManager.instance.SetEffectVolume(volume / 100f); // 사운드를 0~1 범위로 설정
        effectVolumeValueText.text = volume.ToString("F0") + "%"; // 소수점 없이 슬라이더 값을 텍스트로 표시
        SettingsData.instance.effectVolume = volume / 100f; // 볼륨 값을 SettingsData에 저장
    }

    public void ReturnToMainMenu() // 메인메뉴로 돌아가기
    {
        Time.timeScale = 1; // 시간을 재개


        Cursor.lockState = CursorLockMode.Confined;
        SceneManager.LoadScene("MainMenu"); // MainMenu라는 씬으로 돌아감
    }
    void OnSensitivityInputChanged(string input)
    {
        if (float.TryParse(input, out float inputValue))
        {
            // 입력값을 0.01 ~ 10 범위 내로 제한
            inputValue = Mathf.Clamp(inputValue, 0.01f, 10f);

            // 입력된 값을 슬라이더 값으로 변환
            float sensitivity = Mathf.Lerp(0.24f, 240f, Mathf.InverseLerp(0.01f, 10f, inputValue));
            mouseSensitivitySlider.value = sensitivity; // 슬라이더 값을 업데이트

            SetMouseSensitivity(sensitivity); // 마우스 감도 업데이트
        }
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
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                ToggleSettingsPanel(); // 패널 토글
            }
        }
    }
}
