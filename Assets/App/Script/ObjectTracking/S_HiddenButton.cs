using UnityEngine;
using UnityEngine.Events;

public class S_HiddenButton : MonoBehaviour, IInteractable
{
    [Header("References")]
    //Put feedback here
    [SerializeField] private RSO_Float m_ClickCount;

    public UnityEvent action;

    private int m_ClickCountInternal;

    private void Awake()
    {

    }

    public void Interact()
    {
        action.Invoke();
    }
}
