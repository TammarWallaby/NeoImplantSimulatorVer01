/*
 * AudioManager에 합쳤음
 */

using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance; // 싱글턴 인스턴스
    public AudioSource effectAudioSource; // 효과음 전용 오디오 소스
    public AudioClip effectAudioMusic; // 효과음 클립

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않음
            effectAudioSource = gameObject.AddComponent<AudioSource>();
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
            SetEffectVolume(SettingsData.instance.effectVolume); // 초기 볼륨 설정
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
            // 현재 재생 중인 효과음이 있다면 중단
            effectAudioSource.Stop();
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayEffect(effectAudioMusic); // T키를 누르면 효과음 재생
        }
    }
}
