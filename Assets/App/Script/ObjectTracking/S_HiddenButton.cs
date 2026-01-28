using UnityEngine;
using UnityEngine.Events;

public class S_HiddenButton : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private GameObject m_OutlineInteractable;
    [Space]
    //Put feedback here
    [SerializeField] private RSO_Float m_ClickCount;

    public UnityEvent action;

    private int m_ClickCountInternal;

    private void Awake()
    {
        m_OutlineInteractable?.SetActive(false);
    }

    public void Hover()
    {
        m_OutlineInteractable?.SetActive(true);
    }

    public void Unhover()
    {
        m_OutlineInteractable?.SetActive(false);
    }

    public void Interact()
    {
        action.Invoke();
    }
}
