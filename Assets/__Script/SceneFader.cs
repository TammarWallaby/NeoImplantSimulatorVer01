/*
 * 
 * 
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage; // 페이드용 검은 이미지
    public float fadeDuration; // 페이드 효과의 지속 시간

    private void Start()
    {
        StartCoroutine(FadeIn()); // 장면 시작 시 페이드 인
    }

    // 페이드 인 효과
    IEnumerator FadeIn()
    {
        float timer = 0f;

        // 이미지의 알파 값을 1에서 0으로 변경
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alphaValue = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alphaValue);
            yield return null;
        }
    }

    // 씬 전환 시 페이드 아웃을 적용
    public IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        float timer = 0f;

        // 이미지의 알파 값을 0에서 1로 변경
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alphaValue = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alphaValue);
            yield return null;
        }

        // 페이드 아웃이 완료되면 씬 전환
        SceneManager.LoadScene(sceneName);
    }
}