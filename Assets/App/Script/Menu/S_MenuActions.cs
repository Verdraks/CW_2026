using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class S_MenuActions : MonoBehaviour
{
    [Header("UI")]
    public Image fadeImage;
    public TMP_Text startText;

    [Header("Timings")]
    public float fadeDuration = 2f;
    public float startDelay = 1.5f;

    public void StartGame()
    {
       StartCoroutine(EndSequence());
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator EndSequence()
    {
        fadeImage.gameObject.SetActive(true);
        startText.gameObject.SetActive(true);

        fadeImage.color = new Color(0, 0, 0, 0);
        startText.alpha = 0f;

        yield return Fade(0f, 1f);

        yield return FadeText(startText, 0f, 1f, 1f);

        yield return new WaitForSeconds(startDelay);

        SceneManager.LoadScene("Game");

    }

    IEnumerator FadeText(TMP_Text text, float from, float to, float duration)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            text.alpha = Mathf.Lerp(from, to, t);
            yield return null;
        }

        text.alpha = to;
    }

    IEnumerator Fade(float from, float to)
    {
        float t = 0f;
        Color c = fadeImage.color;

        while (t < 1f)
        {
            t += Time.deltaTime / fadeDuration;
            c.a = Mathf.Lerp(from, to, t);
            fadeImage.color = c;
            yield return null;
        }
    }
}
