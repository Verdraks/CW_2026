using UnityEngine;

public class S_Light : MonoBehaviour
{
    //[Header("Settings")]
    [Header("References")]
    [SerializeField] private Light lightSource;
    [Header("Input")]
    [SerializeField] private RSE_ChangeLightColor rse_ChangeLightColor;
    //[Header("Output")]
    private void OnEnable()
    {
        rse_ChangeLightColor.Action += ChangeLightColor;
    }
    private void OnDisable()
    {
        rse_ChangeLightColor.Action -= ChangeLightColor;
    }

    private void ChangeLightColor(Color color)
    {
        lightSource.color = color;
    }
}