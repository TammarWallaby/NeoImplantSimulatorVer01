/*
 * AudioManager에 합쳤음
 */

using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance; // 싱글턴 인스턴스
    public AudioSource backgroundAudioSource; // 배경음 전용 오디오 소스
    public AudioClip backgroundMusic; // 배경음악 클립

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않음
            backgroundAudioSource = gameObject.AddComponent<AudioSource>();
            backgroundAudioSource.loop = true; // 음악 반복 재생
            PlayMusic(); // 음악 시작
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스는 파괴
        }
    }

    private void Start()
    {
        // SettingsData 인스턴스가 null이 아닐 경우 초기 볼륨 설정
        if (SettingsData.instance != null)
        {
            SetBackgroundVolume(SettingsData.instance.backgroundVolume); // 초기 볼륨 설정
        }
        else
        {
            Debug.LogError("SettingsData 인스턴스가 null입니다. SettingsData가 먼저 초기화되어야 합니다.");
        }
    }

    public void PlayMusic()
    {
        if (backgroundMusic != null)
        {
            backgroundAudioSource.clip = backgroundMusic;
            backgroundAudioSource.Play();
        }
        else
        {
            Debug.LogError("배경 음악 클립이 할당되지 않았습니다.");
        }
    }

    public void SetBackgroundVolume(float volume)
    {
        backgroundAudioSource.volume = volume; // 배경음 볼륨 설정
        SettingsData.instance.backgroundVolume = volume; // 설정 저장
    }
}
