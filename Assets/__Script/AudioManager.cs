/*
 * 배경음, 효과음 지정
 * AudioManager 오브젝트에 넣을거임(빈 오브젝트)
 * DontDestroyOnLoad() 있음
 */

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // 싱글턴 인스턴스 , 설정 필수
    public AudioSource effectAudioSource; // 효과음 전용 오디오 소스 , 설정 필수
    public AudioSource backgroundAudioSource; // 배경음 전용 오디오 소스 , 설정 필수
    public AudioClip effectAudioClip; // 효과음 클립 , 설정 필수
    public GameObject sound; // 배경음악을 재생할 오브젝트 , 설정 필수

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않음
            effectAudioSource = gameObject.AddComponent<AudioSource>(); // 효과음용 AudioSource 추가
            backgroundAudioSource = sound.GetComponent<AudioSource>(); // 배경음 전용 AudioSource 참조

            if (!backgroundAudioSource.isPlaying)
            {
                backgroundAudioSource.Play(); // 배경음 재생
                DontDestroyOnLoad(sound); // 배경음이 계속 재생되게 설정
            }
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 파괴
        }
    }

    private void Start()
    {
        // SettingsData 인스턴스가 null이 아닐 경우 초기 볼륨 설정
        if (SettingsData.instance != null)
        {
            SetEffectVolume(SettingsData.instance.effectVolume); // 초기 효과음 볼륨 설정
            SetBackgroundVolume(SettingsData.instance.backgroundVolume); // 초기 배경음 볼륨 설정
        }
        else
        {
            Debug.LogError("SettingsData 인스턴스가 null입니다. SettingsData가 먼저 초기화되어야 합니다.");
        }
    }

    public void PlayEffect(AudioClip clip)
    {
        if (clip != null)
        {
            effectAudioSource.Stop(); // 현재 재생 중인 효과음 중단
            effectAudioSource.clip = clip;
            effectAudioSource.Play(); // 새로운 효과음 재생
        }
        else
        {
            Debug.LogError("효과음 클립이 할당되지 않았습니다.");
        }
    }

    public void SetEffectVolume(float volume)
    {
        effectAudioSource.volume = volume; // 효과음 볼륨 설정
        SettingsData.instance.effectVolume = volume; // 설정 저장
    }

    public void SetBackgroundVolume(float volume)
    {
        backgroundAudioSource.volume = volume; // 배경음 볼륨 설정
        SettingsData.instance.backgroundVolume = volume; // 설정 저장
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayEffect(effectAudioClip); // T키를 누르면 효과음 재생
        }
    }
}
