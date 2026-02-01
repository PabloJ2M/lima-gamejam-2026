using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class SignalManager : MonoBehaviour
{
    [SerializeField] private SignalCollection _collection;
    [SerializeField] private int _rounds = 3;
    [SerializeField] private int _signalsPerRound = 3, _repeatRound = 2;

    [SerializeField] private float _signalDelay;
    [SerializeField] private UnityEvent _onStart;
    [SerializeField] private UnityEvent _onCompleteSequence;

    public Paranoia Paranoia { get; private set; }
    public static event Action<Signal, bool> onSignalEmitted;

    public void DisplayRandomParanoia()
    {
        _onStart.Invoke();

        switch (Random.Range(0, 3))
        {
            case 0: Paranoia = Paranoia.Exito; break;
            case 1: Paranoia = Paranoia.Observado; break;
            case 2: Paranoia = Paranoia.Tecnologia; break;
        }

        DisplayParanoia(Paranoia);
    }
    public void DisplayParanoia(Paranoia paranoia)
    {
        StopAllCoroutines();
        StartCoroutine(StartSignalSequence(paranoia));
        print($"started signal sequence for { paranoia.ToString() }");
    }

    private IEnumerator StartSignalSequence(Paranoia paranoia)
    {
        var delay = new WaitForSeconds(_signalDelay);

        var allSignals = _collection.GetSignals();
        var possibleTrueSignals = _collection.GetSignalsByType(paranoia).ToList();
        print(string.Join(",", possibleTrueSignals));

        possibleTrueSignals.InterleaveShuffle();

        for (int i = 0; i < _rounds; i++)
        {
            Signal trueSignal = possibleTrueSignals[i];

            //generate list of signals to display
            List<(Signal signal, bool isTrue)> roundSignals = new();
            roundSignals.Add((trueSignal, true));

            foreach (var signal in allSignals)
            {
                if (roundSignals.Count >= _signalsPerRound) break;
                if (signal == trueSignal) continue;
                roundSignals.Add((signal, false));
            }

            for (int j = 0; j < _repeatRound; j++)
            {
                roundSignals.Shuffle();

                foreach (var entry in roundSignals)
                {
                    yield return delay;
                    onSignalEmitted?.Invoke(entry.signal, entry.isTrue);
                }
            }

            print("-----------complete round-----------");
            yield return delay;
        }

        print("--------complete sequence---------");
        _onCompleteSequence.Invoke();
    }
}
