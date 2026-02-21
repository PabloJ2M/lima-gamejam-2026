using UnityEngine;
using UnityEngine.Events;

public class Timeout : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private UnityEvent<bool> _onChangeState;
    [SerializeField] private UnityEvent<float> _onTimeUpdate;
    [SerializeField] private UnityEvent _onCompleteTime;

    private bool _isRunning;
    private float _timePassed;

    private void Start() => _onChangeState.Invoke(false);
    private void Update()
    {
        if (!_isRunning) return;

        _timePassed += Time.deltaTime;
        _onTimeUpdate.Invoke(1f - (_timePassed / _time));

        if (_timePassed < _time) return;

        _isRunning = false;
        _onCompleteTime.Invoke();
        _onChangeState.Invoke(false);
    }

    public void StartTimeout()
    {
        _timePassed = 0f;
        _isRunning = true;
        _onChangeState.Invoke(true);
    }
    public void StopTimeout()
    {
        _isRunning = false;
        _onChangeState.Invoke(false);
    }
    public void CompleteTimeout()
    {
        _timePassed = _time;
    }
}