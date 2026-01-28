using System;

[Serializable]
public struct ClickEvent
{
    public float Time;
    public bool IsRelease; // false = press, true = release

    public ClickEvent(float time, bool isRelease)
    {
        Time = time;
        IsRelease = isRelease;
    }
}

