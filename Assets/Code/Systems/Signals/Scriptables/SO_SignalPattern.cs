using System;
using System.Linq;
using System.Collections.Generic;

namespace UnityEngine.Gameplay
{
    [CreateAssetMenu(fileName = "signal pattern", menuName = "system/signals/pattern")]
    public class SO_SignalPattern : ScriptableObject
    {
        [SerializeField] private Paranoia _paranoia;
        [SerializeField] private List<SignalType> _signals = new(4);

        public Paranoia ParanoiaType => _paranoia;

        public void Shuffle() => _signals.Shuffle();
        public SignalType GetSignalByIndex(int index) => _signals[index];

        public SignalType[] GetRandomTypes(int index, int totalAmount)
        {
            if (totalAmount <= 0) return Array.Empty<SignalType>();

            SignalType[] allEnums = Enum.GetValues(typeof(SignalType)) as SignalType[];
            List<SignalType> result = new() { GetSignalByIndex(index) };

            List<SignalType> pool = new(allEnums);
            pool.Remove(result[0]);

            for (int i = 0; i < pool.Count; i++)
            {
                int rnd = Random.Range(i, pool.Count);
                (pool[i], pool[rnd]) = (pool[rnd], pool[i]);
            }

            int remaining = Mathf.Min(totalAmount - 1, pool.Count);
            result.AddRange(pool.Take(remaining));

            for (int i = 0; i < result.Count; i++)
            {
                int rnd = Random.Range(i, result.Count);
                (result[i], result[rnd]) = (result[rnd], result[i]);
            }

            return result.ToArray();
        }
    }
}