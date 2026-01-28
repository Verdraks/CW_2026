using UnityEngine;

public class S_Wire : MonoBehaviour, IInteractable
{
    //[Header("Settings")]
    //[Header("References")]
    //[Header("Input")]
    [Header("Output")]
    [SerializeField] private RSE_WireCut onWireCut;

    public void Hover()
    {
    }

    public void Interact()
    {
        onWireCut.Call(gameObject);
        gameObject.SetActive(false);
    }

    public void Unhover()
    {
    }
}