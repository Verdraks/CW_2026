using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class S_TutorialPages : MonoBehaviour
{
    [Header("Pages")]
    public Transform[] pages;
    public Transform targetPosition;
    public float moveDuration = 1f;

    [Header("Events")]
    public UnityEvent onAllPagesShown;

    private int currentPageIndex = -1;
    private bool isAnimating = false;

    public void ShowNextPage()
    {
        if (isAnimating)
            return;

        currentPageIndex++;

        if (currentPageIndex >= pages.Length)
        {
            onAllPagesShown?.Invoke();
            return;
        }

        MovePage(pages[currentPageIndex]);
    }

    void MovePage(Transform page)
    {
        isAnimating = true;

        page.DOKill();

        Sequence seq = DOTween.Sequence();

        seq.Append(
            page.DOMove(targetPosition.position, moveDuration)
                .SetEase(Ease.InOutCubic)
        );

        seq.Join(
            page.DORotateQuaternion(targetPosition.rotation, moveDuration)
                .SetEase(Ease.InOutCubic)
        );

        seq.OnComplete(() =>
        {
            isAnimating = false;
        });
    }
}
