using UnityEngine;


public class S_ClickableObjectTracking : MonoBehaviour, IObjectTracking, IInteractable
{
    [Header("References")]
    [SerializeField] private RSO_Float m_ClickCount;
    
    private int m_ClickCountInternal;

    public void InitializeTracking()
    {
        m_ClickCountInternal = 0;
        m_ClickCount.Set(0f);
    }

    public void UpdateTracking()
    {
        m_ClickCount.Set(m_ClickCountInternal);
    }

    public void ResetTracking()
    {
        m_ClickCountInternal = 0;
        m_ClickCount.Set(0f);
    }

    public void Interact()
    {
        m_ClickCountInternal++;
        UpdateTracking();
    }

}

