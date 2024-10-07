/*
 * 일단 안쓸거임
 */

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartMusic : MonoBehaviour
{
    public GameObject sound;
    public AudioSource backmusic;

    void Awake()
    {
        backmusic = sound.GetComponent<AudioSource>(); //음악 저장해둠
        if (backmusic.isPlaying) return; //음악이 재생되고 있다면 패스
        else
        {
            backmusic.Play();
            DontDestroyOnLoad(sound); //음악 계속 재생하게(이후 버튼매니저에서 조작)
        }
    }
}