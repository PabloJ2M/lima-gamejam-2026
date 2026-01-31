using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    [SerializeField] private Signal _signal;
    [SerializeField] private UnityEvent _onVisualTask, _onAudioTask;

    private void OnEnable() => SignalManager.onSignalEmitted += SignalEmitted;
    private void OnDisable() => SignalManager.onSignalEmitted -= SignalEmitted;

    private void SignalEmitted(Signal signal, bool isReal)
    {
        if (signal != _signal) return;

        if (isReal) ExcecuteAll();
        else ExecuteRandom();
    }

    private void ExcecuteAll()
    {
        print($"real signal emitted in {name}");

        _onVisualTask.Invoke();
        _onAudioTask.Invoke();
    }
    private void ExecuteRandom()
    {
        print($"fake signal emitted in {name}");

        if (Random.value > 0.5f) _onVisualTask.Invoke();
        else _onAudioTask.Invoke();
    }
}