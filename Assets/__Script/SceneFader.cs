/*
 * FadeCanvas에 넣음
 * 전환 할 때 검정 화면 페이드 인 아웃 + 특정 이미지 페이드 인 + 아웃
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage; // 페이드용 검은 이미지
    public Image otherImage; // 알파값을 변화시킬 다른 이미지
    public float fadeDuration; // 페이드 효과의 지속 시간
    public float imageDuration; // otherImage 페이드 인/아웃 속도
    public float delayDuration; // 이미지가 완전히 나타난 후 유지되는 시간
    public float fadeTriggerThreshold = 0.5f; // fadeImage 알파값이 0.5일 때 otherImage 페이드 시작

    private void Start()
    {
        StartCoroutine(FadeIn()); // 장면 시작 시 페이드 인
    }

    // 페이드 인 효과 (검은 이미지)
    IEnumerator FadeIn()
    {
        float timer = 0f;
        bool otherImageStarted = false;

        // fadeImage의 알파 값을 1에서 0으로 변경
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alphaValue = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alphaValue);

            // fadeImage 알파값이 특정 임계값에 도달하면 otherImage 페이드 시작
            if (!otherImageStarted && alphaValue <= fadeTriggerThreshold)
            {
                otherImageStarted = true;
                StartCoroutine(FadeOtherImage());
            }

            yield return null;
        }
    }

    // 다른 이미지의 알파값을 0에서 1로, 그리고 다시 0으로 변화시키는 효과
    IEnumerator FadeOtherImage()
    {
        float timer = 0f;

        // otherImage 알파값을 0에서 1로 변경 (등장)
        while (timer < imageDuration)
        {
            timer += Time.deltaTime;
            float alphaValue = Mathf.Lerp(0f, 1f, timer / imageDuration);
            otherImage.color = new Color(otherImage.color.r, otherImage.color.g, otherImage.color.b, alphaValue);
            yield return null;
        }

        // 이미지가 일정 시간 동안 완전히 나타난 상태로 유지
        yield return new WaitForSeconds(delayDuration);

        timer = 0f;

        // otherImage 알파값을 1에서 0으로 변경 (사라짐)
        while (timer < imageDuration)
        {
            timer += Time.deltaTime;
            float alphaValue = Mathf.Lerp(1f, 0f, timer / imageDuration);
            otherImage.color = new Color(otherImage.color.r, otherImage.color.g, otherImage.color.b, alphaValue);
            yield return null;
        }
    }

    // 씬 전환 시 페이드 아웃을 적용
    public IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        float timer = 0f;

        // fadeImage 알파 값을 0에서 1로 변경
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
