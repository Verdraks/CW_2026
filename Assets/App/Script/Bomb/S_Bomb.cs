using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Bomb : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float bombTimer = 60f;
    [Header("References")]
    [SerializeField] private List<GameObject> correctWires = new List<GameObject>();
    [SerializeField] private List<GameObject> incorrectWires = new List<GameObject>();

    [Header("Input")]
    [SerializeField] private RSE_WireCut onWireCut;
    [SerializeField] private RSE_OnStartBombTimer onStartBombTimer;

    [Header("Output")]
    [SerializeField] private RSE_BasicEvent onBombDefused;
    [SerializeField] private RSE_BasicEvent onBombExplode;

    private int wiresToCut = 0;
    private int wiresCut = 0;
    private Coroutine bombTimerCoroutine = null;
    private void OnEnable()
    {
        onWireCut.Action += VerifyCutWires;
        onStartBombTimer.Action += StartBombTimer;
    }
    private void OnDisable()
    {
        onWireCut.Action -= VerifyCutWires;
        onStartBombTimer.Action -= StartBombTimer;
    }
    private void Awake()
    {
        wiresToCut = correctWires.Count;
        wiresCut = 0;
    }
    private void VerifyCutWires(GameObject wire)
    {
        if (incorrectWires.Contains(wire))
        {
            BombExplode();
        }
        else if (correctWires.Contains(wire))
        {
            if(wire == correctWires[0])
            {
                wiresCut++;
                correctWires.RemoveAt(0);
            }
            else
            {
                BombExplode();
                return;
            }
            if (wiresCut == wiresToCut)
            {
                BombDefused();
            }
        }
        else
        {
            Debug.Log("Wire not recognized.");
        }
    }

    private void BombExplode()
    {
        onBombExplode.Call();
    }

    private void BombDefused()
    {
        onBombDefused.Call();
        StopBombTimer();
    }
    private void StartBombTimer()
    {
        if (bombTimerCoroutine != null)
        {
            StopCoroutine(bombTimerCoroutine);
            bombTimerCoroutine = null;
        }
        bombTimerCoroutine = StartCoroutine(StartBombTimer(bombTimer));
    }
    private void StopBombTimer()
    {
        if (bombTimerCoroutine != null)
        {
            StopCoroutine(bombTimerCoroutine);
            bombTimerCoroutine = null;
        }
    }
    IEnumerator StartBombTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        BombExplode();
    }
}