using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class CircularBuffer<T>
{
    private T[] m_Buffer;
    private int m_Head;

    public int Capacity => m_Buffer?.Length ?? 0;
    public int Count { get; private set; }

    public CircularBuffer(int capacity)
    {
        if (capacity < 1) capacity = 1;
        m_Buffer = new T[capacity];
        m_Head = 0;
        Count = 0;
    }

    public void Add(T item)
    {
        if (m_Buffer == null || m_Buffer.Length == 0) return;
        m_Buffer[m_Head] = item;
        m_Head = (m_Head + 1) % m_Buffer.Length;
        if (Count < m_Buffer.Length) Count++;
    }

    public void Clear()
    {
        if (m_Buffer != null) System.Array.Clear(m_Buffer, 0, m_Buffer.Length);
        m_Head = 0;
        Count = 0;
    }

    // Retourne les éléments dans l'ordre chronologique (le plus ancien en premier)
    public List<T> ToList()
    {
        var result = new List<T>(Count);
        if (Count == 0) return result;
        int start = (m_Head - Count + m_Buffer.Length) % m_Buffer.Length;
        for (int i = 0; i < Count; i++)
        {
            int idx = (start + i) % m_Buffer.Length;
            result.Add(m_Buffer[idx]);
        }
        return result;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count) throw new System.IndexOutOfRangeException();
            int idx = (m_Head - Count + index + m_Buffer.Length) % m_Buffer.Length;
            return m_Buffer[idx];
        }
    }
}

