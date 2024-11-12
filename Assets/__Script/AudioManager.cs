/*
 * AudioManager 오브젝트에 넣을거임(빈 오브젝트)
 * 배경음, 효과음 지정
 * DontDestroyOnLoad() 있음
 * 특정 상황 메서드에 AudioManager.instance.PlayEffect(사용할 배열); 쓰면 효과음 들림
 */

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource effectAudioSource;
    public AudioSource backgroundAudioSource;
    public AudioClip[] effectAudioClips; // 여러 효과음 클립을 배열로 관리
    public AudioClip backSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            effectAudioSource = gameObject.AddComponent<AudioSource>();
            backgroundAudioSource = gameObject.AddComponent<AudioSource>();

            backgroundAudioSource.clip = backSound;
            backgroundAudioSource.loop = true;

            if (!backgroundAudioSource.isPlaying)
            {
                backgroundAudioSource.Play();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (SettingsData.instance != null)
        {
            SetEffectVolume(SettingsData.instance.effectVolume);
            SetBackgroundVolume(SettingsData.instance.backgroundVolume);
        }
    }
    private void Update()
    {
        // R 버튼을 눌렀을 때 효과음 재생
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayEffect(0); // 효과음 배열의 첫 번째 효과음 재생 (인덱스를 원하는 값으로 설정)
        }

        // T 버튼을 눌렀을 때 효과음 재생
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            PlayEffect(1); // 효과음 배열의 두 번째 효과음 재생 (인덱스를 원하는 값으로 설정)
        }
    }

    public void PlayEffect(int clipIndex)
    {
        if (effectAudioClips != null && clipIndex >= 0 && clipIndex < effectAudioClips.Length)
        {
            effectAudioSource.Stop();
            effectAudioSource.clip = effectAudioClips[clipIndex];
            effectAudioSource.Play();
        }
    }

    public void SetEffectVolume(float volume)
    {
        effectAudioSource.volume = volume;
        SettingsData.instance.effectVolume = volume;
    }

    public void SetBackgroundVolume(float volume)
    {
        backgroundAudioSource.volume = volume;
        SettingsData.instance.backgroundVolume = volume;
    }

    // 특정 상황에서 효과음을 재생하는 메서드 예시
    public void TriggerSoundEffectOnEvent(int effectIndex)
    {
        // 특정 조건에 따라 인덱스를 전달해 적절한 효과음 재생
        if (SomeGameConditionIsMet())
        {
            //AudioManager.instance.PlayEffect(사용할 효과음 배열); // (다른 스크립트에도 가능) 특정 조건 매서드에 이런식으로 적으면 됨
            PlayEffect(effectIndex); // 조건이 충족되면 효과음 재생
        }
    }

    private bool SomeGameConditionIsMet()
    {
        // 특정 게임 조건을 체크하는 로직 구현
        return true; // 조건이 맞으면 true 반환
    }
}
