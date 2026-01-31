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

    private int _currentRound;

    [SerializeField] private float _signalDelay;
    [SerializeField] private UnityEvent _onCompleteSequence;

    public static event Action<Signal, bool> onSignalEmitted;

    private void Start() => DisplayRandomParanoia();

    public void DisplayRandomParanoia()
    {
        DisplayParanoia((Paranoia)Random.Range(0, 3));
    }
    public void DisplayParanoia(Paranoia paranoia)
    {
        StopAllCoroutines();
        StartCoroutine(StartSignalSequence(paranoia));
        print($"started signal sequence for { paranoia }");
    }

    private IEnumerator StartSignalSequence(Paranoia paranoia)
    {
        var delay = new WaitForSeconds(_signalDelay);

        var allSignals = _collection.GetSignals();
        var possibleTrueSignals = _collection.GetSignalsByType(paranoia).ToList();
        possibleTrueSignals.Shuffle();

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

            yield return delay;
        }

        _onCompleteSequence.Invoke();
    }
}
