namespace UnityEngine.Gameplay
{
    public abstract class SignalBehaviour : MonoBehaviour, ISignalEmitter
    {
        [SerializeField] protected SignalType _signalType;
        protected SignalController _controller;

        public abstract float Duration { get; }

        protected virtual void Awake() => _controller = SignalController.Instance;
        protected virtual void OnEnable() => _controller.AddListener(_signalType, this);
        protected virtual void OnDisable() => _controller.RemoveListener(_signalType, this);

        public abstract void EmitteFakeSignal();
        public abstract void EmitteSignal();
    }
}