using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class S_DiegeticButton : MonoBehaviour
{
    public UnityEvent onClick;

    [Header("Feedback")]
    [SerializeField] MMF_Player clickFeedback;
    [SerializeField] MMF_Player releaseFeedback;

    bool isPressed = false;

    void Update()
    {
        if (Mouse.current == null)
            return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(
                Mouse.current.position.ReadValue()
            );

            if (Physics.Raycast(ray, out RaycastHit hit, 5f))
            {
                if (hit.transform == transform)
                {
                    Press();
                }
            }
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Release();
        }
    }

    void Press()
    {
        isPressed = true;
        clickFeedback.PlayFeedbacks();
    }

    void Release()
    {
        if (isPressed)
        {
            isPressed = false;
            releaseFeedback.PlayFeedbacks();
            onClick?.Invoke();
        }
    }
}
