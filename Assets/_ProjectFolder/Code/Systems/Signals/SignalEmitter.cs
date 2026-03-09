using UnityEngine.Events;

namespace UnityEngine.Gameplay
{
    [RequireComponent(typeof(PropEmitter))]
    public class SignalEmitter : SignalBehaviour, ISignalEmitter
    {
        [SerializeField] private PropEmitter _audioEmitter;
        [SerializeField] private float _duration;
        [SerializeField] private UnityEvent<bool> _onSignalEmitted;

        public override float Duration => _duration;

        private void Start() => DisableVisualTask();
        private void OnValidate() => _duration = Mathf.Clamp(_duration, 0, float.MaxValue);

        public override void EmitteSignal()
        {
            _onSignalEmitted.Invoke(true);
            Invoke(nameof(DisableVisualTask), Duration);
            EmitteFakeSignal();
        }
        public override void EmitteFakeSignal()
        {
            _audioEmitter?.Play();
        }

        private void DisableVisualTask()
        {
            _onSignalEmitted.Invoke(false);
        }
    }
}