using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    [SerializeField] private Signal _signal;
    [SerializeField] private float _duration;
    [SerializeField] private UnityEvent<bool> _onVisualTask, _onAudioTask;
    //[SerializeField] private 

    private void Awake()
    {
        _onVisualTask.Invoke(false);
        //_onAudioTask.Invoke(false);
    }
    private void OnEnable() => SignalManager.onSignalEmitted += SignalEmitted;
    private void OnDisable() => SignalManager.onSignalEmitted -= SignalEmitted;

    private void SignalEmitted(Signal signal, bool isReal)
    {
        if (signal != _signal) return;

        if (isReal) ExcecuteAll();
        else ExecuteRandom();
        Invoke(nameof(Disable), _duration);
    }

    private void ExcecuteAll() {
        _onVisualTask.Invoke(true);
        _onAudioTask.Invoke(true);

        print($"real signal emitted in <color=green>{name}</color> BothTasks");
    }

    private void ExecuteRandom() {
        bool result = Random.value > 0.5f;
        if(result)
            _onVisualTask.Invoke(true);
        else
            _onAudioTask.Invoke(true);

        string resultTask = result ? "VisualTask" : "AudioTask";
        print($"fake signal emitted in <color=red>{name}</color> {resultTask}");
    }
    
    private void Disable()
    {
        _onVisualTask.Invoke(false);
        //_onAudioTask.Invoke(false);
    }
}