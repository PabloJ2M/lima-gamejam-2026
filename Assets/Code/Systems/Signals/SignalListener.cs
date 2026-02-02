using UnityEngine;
using UnityEngine.Events;

[SelectionBase]
public class SignalListener : MonoBehaviour
{
    [SerializeField] private Signal _signal;
    [SerializeField] private float _duration;
    [SerializeField] private UnityEvent<bool> _onVisualTask, _onAudioTask;

    private void Awake() => _onVisualTask.Invoke(false);
    private void OnEnable() => SignalManager.onSignalEmitted += SignalEmitted;
    private void OnDisable() => SignalManager.onSignalEmitted -= SignalEmitted;

    private void SignalEmitted(Signal signal, bool isReal)
    {
        if (signal != _signal) return;

        if (isReal) ExcecuteAll();
        else ExecuteRandom();
        Invoke(nameof(Disable), _duration);
    }

    private void ExcecuteAll()
    {
        _onVisualTask.Invoke(true);
        _onAudioTask.Invoke(true);

        print($"real signal emitted in <color=green>{name}</color> BothTasks");
    }
    private void ExecuteRandom()
    {
        _onAudioTask.Invoke(true);

        print($"fake signal emitted in <color=red>{name}</color>");
    }
    
    private void Disable()
    {
        _onVisualTask.Invoke(false);
    }
}