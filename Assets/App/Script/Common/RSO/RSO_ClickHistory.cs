using UnityEngine;
using MVsToolkit.Wrappers;

[CreateAssetMenu(fileName = "RSO_ClickHistory", menuName = "RSO/Common/ClickHistory")]
public class RSO_ClickHistory : RuntimeScriptableObject<CircularBuffer<ClickEvent>>
{
    private readonly int m_MaxSize = 20;

    private void OnEnable()
    {
        Setup();
    }

    public void Setup()
    {
        Set(new CircularBuffer<ClickEvent>(m_MaxSize));
    }

    private void OnValidate()
    {
        Setup();
    }

}

