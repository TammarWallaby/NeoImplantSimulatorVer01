/* 
 * SettingData 오브젝트에 넣을거임 (빈 오브젝트)
 * 소리, 감도 기본값 + 값 유지 저장 할 때 쓸거임
 * DontDestroyOnLoad() 있음
 */

using UnityEngine;

public class SettingsData : MonoBehaviour
{
    public static SettingsData instance;
    public float backgroundVolume = 1f; // 배경음 볼륨 (0~1)
    public float effectVolume = 1f; // 효과음 볼륨 (0~1)
    public float mouseSensitivity = 120f; // 기본 마우스 감도 (120)

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
