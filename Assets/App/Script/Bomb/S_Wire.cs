using UnityEngine;

public class S_Wire : MonoBehaviour, IInteractable
{
    [Header("Settings")]
    [SerializeField] private GameObject m_OutlineInteractable;
    //[Header("References")]
    //[Header("Input")]
    [Header("Output")]
    [SerializeField] private RSE_WireCut onWireCut;

    public void Hover()
    {
        m_OutlineInteractable.SetActive(true);
    }

    public void Interact()
    {
        onWireCut.Call(gameObject);
        gameObject.SetActive(false);
    }

    public void Unhover()
    {
        m_OutlineInteractable.SetActive(false);
    }
}