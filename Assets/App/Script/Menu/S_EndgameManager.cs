using MoreMountains.Feedbacks;
using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class S_EndgameManager : MonoBehaviour
{
    [Header("UI")]
    public Image fadeImage;
    public TMP_Text endText;
    public TMP_Text statText;
    [SerializeField] MMF_Player textTypingFeedback;

    [Header("Data")]
    public RSO_Int betrayCount;
    public RSO_Bool isMachineDestroyed;

    [Header("Timings")]
    public float fadeDuration = 2f;
    public float charDelay = 0.04f;
    public float statDelay = 1.5f;

    [Header("Ending Conditions")]
    public int betrayThreshold;

    [TextArea] public string ending00;
    [TextArea] public string ending01;
    [TextArea] public string ending10;
    [TextArea] public string ending11;

    void Start()
    {
        StartCoroutine(EndSequence());
    }

    IEnumerator EndSequence()
    {
        fadeImage.gameObject.SetActive(true);
        endText.gameObject.SetActive(true);
        statText.gameObject.SetActive(true);

        fadeImage.color = new Color(0, 0, 0, 0);
        statText.alpha = 0f;
        endText.text = "";

        yield return Fade(0f, 1f);

        ShowStats();

        yield return FadeText(statText, 0f, 1f, 1f);

        yield return new WaitForSeconds(statDelay);

        string endingText = GetEndingText();
        yield return TypeText(endingText);
    }

    string GetEndingText()
    {
        bool highKills = betrayCount.Get() >= betrayThreshold;

        int index =
            (highKills ? 2 : 0) +
            (isMachineDestroyed ? 1 : 0);

        return index switch
        {
            0 => ending00,
            1 => ending01,
            2 => ending10,
            3 => ending11,
            _ => ending00
        };
    }

    void ShowStats()
    {
        statText.alpha = 1f;

        statText.text =
            $"YOU BETRAYED YOUR EXPERT : {betrayCount.Get()} TIMES";
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

    IEnumerator TypeText(string fullText)
    {
        endText.text = "";
        foreach (char c in fullText)
        {
            endText.text += c;
            textTypingFeedback?.PlayFeedbacks();
            yield return new WaitForSeconds(charDelay);
        }
    }
}
