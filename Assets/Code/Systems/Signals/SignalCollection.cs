using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Collection", menuName = "Signals/Collection", order = 0)]
public class SignalCollection : ScriptableObject
{
    [SerializeField] private List<Signal> _signals;

    public IReadOnlyCollection<Signal> GetSignalsByType(Paranoia paranoia)
    {
        var signals = GetSignals();
        signals.Where(x => x.Paranoia.HasFlag(paranoia));
        return signals;
    }

    public IReadOnlyCollection<Signal> GetSignals()
    {
        var list = new List<Signal>(_signals);
        list.Shuffle();
        return list;
    }
}