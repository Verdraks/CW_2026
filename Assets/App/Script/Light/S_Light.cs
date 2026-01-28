using UnityEngine;

public class S_Light : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Color successColor;
    [SerializeField] private Color failureColor;
    [Header("References")]
    [SerializeField] private Light lightSource;
    [Header("Input")]
    [SerializeField] private RSO_Bool rso_LightSuccess;
    [SerializeField] private RSE_BasicEvent rse_ChangeLightColor;
    //[Header("Output")]
    private void OnEnable()
    {
        rse_ChangeLightColor.Action += ChangeLightColor;
    }
    private void OnDisable()
    {
        rse_ChangeLightColor.Action -= ChangeLightColor;
    }
    private void Start()
    {
        ChangeLightColor();
    }
    private void ChangeLightColor()
    {
        if (rso_LightSuccess == null) return;

        if (rso_LightSuccess.Get())
        {
            lightSource.color = successColor;
        }
        else
        {
            lightSource.color = failureColor;
        }
    }
}