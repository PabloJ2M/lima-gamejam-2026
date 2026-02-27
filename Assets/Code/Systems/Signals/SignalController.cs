using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.Gameplay
{
    using Events;

    public class SignalController : SingletonBasic<SignalController>
    {
        [SerializeField] private SO_SignalDatabase _database;
        [SerializeField] private EnemyManager _enemy;

        [Header("Rounds")]
        [SerializeField] private int _roundsCount = 1;
        [SerializeField] private int _signalsPerRound = 1;

        [Header("Round Controller")]
        [SerializeField] private float _speedMultiply = 1f;
        [SerializeField] private float _startDelay, _betweenDelay, _completeDelay;

        [SerializeField] private UnityEvent _onBeginSequence, _onCompleteSequence;
        public Action<Paranoia> onCompleteSequence;

        private Dictionary<SignalType, List<ISignalEmitter>> _emitters = new();
        private WaitForSeconds _waitStartDelay, _waitBetweenDelay, _waitCompleteDelay;

        protected override void Awake()
        {
            base.Awake();
            _waitStartDelay = new(_startDelay);
            _waitBetweenDelay = new(_betweenDelay);
            _waitCompleteDelay = new(_completeDelay);
        }
        public void AddListener(SignalType @object, ISignalEmitter emitter)
        {
            if (!_emitters.ContainsKey(@object)) _emitters.Add(@object, new());
            _emitters[@object].Add(emitter);
        }
        public void RemoveListener(SignalType @object, ISignalEmitter emitter)
        {
            if (!_emitters.ContainsKey(@object)) return;
            _emitters[@object].Remove(emitter);
        }

        public void StartSequence() => StartCoroutine(SignalSequence());
        private IEnumerator SignalSequence()
        {
            yield return _waitStartDelay;
            _onBeginSequence.Invoke();

            //get signal pattern for entity
            var pattern = _database.GetRandomPattern();
            pattern.Shuffle();

            _enemy.PendingParanoia = pattern.ParanoiaType;
            print($"current paranoia {pattern.ParanoiaType}");

            for (int i = 0; i < _roundsCount; i++)
            {
                //get signal list and real signal for comparison
                var real = pattern.GetSignalByIndex(i);
                var signals = pattern.GetRandomTypes(i, _signalsPerRound);

                //display signal emission
                foreach (var signal in signals)
                {
                    var emitter = _emitters[signal][Random.Range(0, _emitters[signal].Count)];
                    if (signal == real) emitter.EmitteSignal();
                    else emitter.EmitteFakeSignal();

                    yield return new WaitForSeconds(emitter.Duration * _speedMultiply);
                    yield return _waitBetweenDelay;
                }
            }

            yield return _waitCompleteDelay;
            _onCompleteSequence.Invoke();
            onCompleteSequence?.Invoke(pattern.ParanoiaType); // <- try to use this event for checking paranoia
        }
    }
}