using System;
using UnityEngine;

public enum E_ClickElementType
{
    Clicks,
    Hold
}

[Serializable]
public class ClickPatternElement
{
    [Tooltip("Type d'élément : Clicks = groupe de clics, Hold = appui maintenu")]
    public E_ClickElementType Type = E_ClickElementType.Clicks;

    [Header("Clicks Group Settings")]
    [Tooltip("Nombre de clics rapides consécutifs (utilisé si Type == Clicks)")]
    public int ClickCount = 1;
    
    [Tooltip("Délai maximum entre les clics rapides (en secondes)")]
    public float MaxTimeBetweenClicks = 0.3f;

    [Tooltip("Délai minimum entre les clics rapides (en secondes). 0 = pas de minimum.")]
    public float MinTimeBetweenClicks = 0f;
    
    [Tooltip("Délai minimum de pause après ce groupe de clics (en secondes). 0 = pas de pause requise.")]
    public float MinPauseAfter;

    [Header("Hold Settings")]
    [Tooltip("Durée minimale de l'appui pour considérer un hold (en secondes). Utilisé si Type == Hold et si HoldExpectedDuration <= 0")]
    public float MinHoldDuration = 0.5f;

    [Tooltip("Durée maximale de l'appui pour considérer un hold (en secondes). 0 = pas de maximum; utilisé si HoldExpectedDuration <= 0")]
    public float MaxHoldDuration = 0f;

    [Tooltip("Durée attendue du hold (en secondes). Si > 0, la validation utilisera HoldExpectedDuration ± HoldDurationTolerance au lieu de Min/Max.")]
    public float HoldExpectedDuration = 0f;

    [Tooltip("Tolérance (en secondes) appliquée à HoldExpectedDuration: la durée sera acceptée si elle est dans [expected - tolerance, expected + tolerance]")]
    public float HoldDurationTolerance = 0.0f;
}
