using UnityEngine;
using UnityEngine.Events;

public class Timeout : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private UnityEvent<bool> _onChangeState;
    [SerializeField] private UnityEvent _onCompleteTime;

    private bool _isRunning;
    private float _timePassed;

    private void Update()
    {
        if (!_isRunning) return;

        _timePassed += Time.deltaTime;
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
}